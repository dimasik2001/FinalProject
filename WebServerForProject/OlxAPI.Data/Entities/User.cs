using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace OlxAPI.Data.Entities
{
    public class User : IdentityUser
    {
        public string ImagePath { get; set; }
        public ICollection<Ad> Ads { get; set; }
        public bool IsBlocked { get; set; }
    }
}
