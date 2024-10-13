using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Entries.DTOs;
using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Entries.Data;
using MongoDB.Driver;
using Entries.Entities;
using Microsoft.AspNetCore.Http;
namespace Entries.Controllers
{
    [Authorize]
    [Route("api/entries")]
    public class AuthController : ControllerBase
    {
        private readonly IBinaryChecker _service;
        private readonly IMongoCollection<Entry> _entry;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string currentUser;
        public AuthController(IBinaryChecker service, MongoDbContext _context, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _entry = _context.Database?.GetCollection<Entry>("entries")!;
            _httpContextAccessor = httpContextAccessor;
            currentUser = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;


        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllEntries()
        {
            var filter = Builders<Entry>.Filter.Eq(x => x.UserId, currentUser);
            var entriesList = await _entry.Find(filter).SortByDescending(x => x.CheckedDate).ToListAsync();
            var response = new ApiResponse<List<Entry>>
            {
                IsSuccess = true,
                StatusCode = 200,
                StackTrace = null,
                Response = entriesList,
                Message = "List of your entries fetched succesfully"

            };
            return Ok(response);
        }


        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckString([FromBody] BinaryCheckRequestDTO request)
        {
            var input = request.BinaryString;


            var validation = _service.ValidateBinaryString(input);

            var newEntry = new Entry();
            newEntry.String = input;
            newEntry.UserId = currentUser;
            newEntry.CheckedDate = DateTime.Now;
            newEntry.Good = validation.valid ? true : false;

            try
            {

                await _entry.InsertOneAsync(newEntry);
                if (validation.valid)
                {

                    var response = new ApiResponse<bool>
                    {
                        IsSuccess = true,
                        Message = "Valide string",
                        StackTrace = null,
                        Response = true,
                        StatusCode = 200,
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = validation.error,
                        StackTrace = null,
                        Response = false,
                        StatusCode = 400,
                    };
                    return BadRequest(response);
                }

            }
            catch (MongoWriteException mwx)
            {

                return BadRequest(mwx);

            }
            catch (System.Exception)
            {

                return Problem();

            }


        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteEntry([FromBody] DeleteEntryRequestDTO request)
        {

            var filter = Builders<Entry>.Filter.Eq(x => x.Id, request.Id);

            var entry = await _entry.Find(filter).FirstOrDefaultAsync();
            if (entry == null)
            {
                return NotFound("Entry does not exist");
            }
            if (entry!.UserId != currentUser)
            {
                return Unauthorized("Unauthorized action");
            }
            await _entry.DeleteOneAsync(filter);
            return Ok(request);
        }
    }
}
