using System;

namespace VotesRestApi.Core.Models
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
