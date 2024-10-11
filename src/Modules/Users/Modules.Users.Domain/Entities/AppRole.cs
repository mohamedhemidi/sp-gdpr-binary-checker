using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Modules.Users.Domain.Entities
{
    [CollectionName("roles")]
    public class AppRole : MongoIdentityRole<Guid>
    {

    }
}