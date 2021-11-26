using System;
using System.Collections.Generic;

namespace OlxAPI.Data.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime ChangeDate { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<AdsCategories> AdsCategories { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}