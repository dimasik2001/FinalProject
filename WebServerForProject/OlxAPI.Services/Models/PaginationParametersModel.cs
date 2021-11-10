using System;
using System.Collections.Generic;
using System.Text;

namespace OlxAPI.Services.Models
{
    public class PaginationParametersModel
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
