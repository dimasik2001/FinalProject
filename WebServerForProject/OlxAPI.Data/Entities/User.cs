using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace OlxAPI.Data.Entities
{
    public class User : IdentityUser
    {
        public IEnumerable<Ad> Ads { get; set; }
    }
}
