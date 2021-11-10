using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlxAPI.Enums;

namespace OlxAPI.Models.PostModels.Parameters
{
    public class FilterQueryParameters
    {
        public FilterItemEnum? FilterItem { get; set; }
        public string ItemId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
         public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
