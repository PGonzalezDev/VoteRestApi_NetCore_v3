using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Pass { get; set; }
        public int UserRoleValue { get; set; }
        public bool IsAdmin
        {
            get { return (UserRoleValue == (int)UserRol.Admin); }
        }
    }
}
