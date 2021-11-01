using System;
using System.Collections.Generic;
using System.Text;

namespace OlxAPI.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<AdsCategories> AdsCategories { get; set; }
    }
}
