using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entities
{
    public abstract class BaseDomainEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset Created_at { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Updated_at { get; set; }= DateTimeOffset.Now;
        public string? Created_by { get; set; }
        public string? Updated_by { get; set; } = null;
        public bool Deleted { get; set; } = false;

    }
}