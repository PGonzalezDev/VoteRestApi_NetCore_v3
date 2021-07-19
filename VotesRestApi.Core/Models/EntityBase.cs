using System;
using System.Collections.Generic;
using System.Text;

namespace VotesRestApi.Core.Models
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
