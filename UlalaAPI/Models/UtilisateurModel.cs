﻿using System.ComponentModel.DataAnnotations;

namespace UlalaAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        public int Active { get; set; }
        public string ActivationToken { get; set; }
    }
}