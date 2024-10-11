
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Modules.Users.Domain.Entities
{
    [CollectionName("users")]
    public class AppUser : MongoIdentityUser<Guid>
    {

        public string FullName { get; set; } = string.Empty;
    }
}
