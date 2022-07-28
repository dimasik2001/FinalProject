using System;
using System.Collections.Generic;
using System.Text;
using OlxAPI.Enums;

namespace OlxAPI.Services.Models
{
    public class SortParametersModel
    {
        public SortDirectionEnum? SortDirection { get; set; }
        public SortItemEnum? SortItem { get; set; }
    }
}
