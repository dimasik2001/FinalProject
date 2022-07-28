﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Models.PostModels.Parameters
{
    public class PaginationQueryParameters
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
