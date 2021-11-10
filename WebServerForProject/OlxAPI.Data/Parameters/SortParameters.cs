using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using OlxAPI.Data.Entities;

namespace OlxAPI.Data.Parameters
{
    public class SortParameters
    {
        public bool IsAscending { get; set; }
        public Expression<Func<Ad, object>> SortFunc { get; set; }
    }
}
