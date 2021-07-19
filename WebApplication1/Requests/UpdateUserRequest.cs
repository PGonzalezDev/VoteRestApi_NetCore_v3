﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Service.DTOs;

namespace WebApplication
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string NewPass { get; set; }
        public string OldPass { get; set; }

        public UpdateUserDto ToDto(Guid id)
        {
            return new UpdateUserDto()
            {
                Id = id,
                Name = this.Name,
                Mail = this.Mail,
                NewPass = this.NewPass,
                OldPass = this.OldPass
            };
        }
    }
}