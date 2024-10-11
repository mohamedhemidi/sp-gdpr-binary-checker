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
            var entriesList = await _entry.Find(filter).ToListAsync();
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

            if (validation)
            {

                var newEntry = new Entry
                {
                    String = input,
                    UserId = currentUser,
                    Good = true
                };
                await _entry.InsertOneAsync(newEntry);
            }


            var response = new ApiResponse<bool>
            {
                IsSuccess = true,
                Message = "Valide String",
                StackTrace = null,
                Response = validation,
                StatusCode = 200,
            };
            return Ok(response);
        }
    }
}
