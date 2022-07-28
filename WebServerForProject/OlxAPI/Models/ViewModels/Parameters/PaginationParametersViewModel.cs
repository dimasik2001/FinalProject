using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Models.ViewModels.Parameters
{
    public class PaginationParametersViewModel
    {
        public class PaginationParametersModel
        {
            public int? Page { get; set; }
            public int? PageSize { get; set; }
            public int? Total { get; set; }
        }
    }
}
