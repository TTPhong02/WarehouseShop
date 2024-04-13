﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.DTOs
{
    public class UserDTO 
    {
        public Guid UsersId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid CartsId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
