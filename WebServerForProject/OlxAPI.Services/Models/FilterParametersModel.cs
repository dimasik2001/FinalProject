using System;
using System.Collections.Generic;
using System.Text;
using OlxAPI.Enums;

namespace OlxAPI.Services.Models
{
    public class FilterParametersModel
    {
        public FilterItemEnum? FilterItem { get; set; }
        public string ItemId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string KeyWords { get; set; }
    }
}
