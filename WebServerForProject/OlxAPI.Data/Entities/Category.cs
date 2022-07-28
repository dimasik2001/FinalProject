using System;
using System.Collections.Generic;
using System.Text;

namespace OlxAPI.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AdsCategories> AdsCategories { get; set; }
    }
}
