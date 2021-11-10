using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Models.PostModels
{
    public class AdPostModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<CategoryPostModel> Categories { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
