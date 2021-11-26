using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Models.PostModels
{
    public class UserPostModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string ImageBase64 { get; set; }
    }
}
