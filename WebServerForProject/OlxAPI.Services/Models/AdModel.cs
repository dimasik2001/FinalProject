using System;
using System.Collections.Generic;
using System.Text;

namespace OlxAPI.Services.Models
{
    public class AdModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime ChangeDate { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
