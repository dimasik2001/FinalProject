using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlxAPI.Enums;

namespace OlxAPI.Models.PostModels.Parameters
{
    public class SortQueryParameters
    {
        public SortDirectionEnum? SortDirection { get; set; }
        public SortItemEnum? SortItem { get; set; }
    }
}
