﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Models.UpdateModels
{
    public class UserUpdateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
