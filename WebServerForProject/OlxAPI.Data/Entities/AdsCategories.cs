using System;
using System.Collections.Generic;
using System.Text;

namespace OlxAPI.Data.Entities
{
    public class AdsCategories
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        public int CategoryId { get; set; }
        public Ad Ad { get; set; }
        public Category Category { get; set; }
    }
}
