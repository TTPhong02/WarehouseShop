﻿using BE_WAREHOUSE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Users
    {
        public Guid UsersId { get; set; }
        public string  Password { get; set; }
        public string Email { get; set; }
        public Guid? AddressId { get; set; }
        public string? Avatar { get; set; }
        public string? Fullname { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public AccountStatus? Status { get; set; }
        public string? Role { get; set; }
        public string? NewPassword { get; set; }
        public string? CurrentPassword { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
