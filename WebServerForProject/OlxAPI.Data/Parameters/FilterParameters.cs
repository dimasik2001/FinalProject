using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using OlxAPI.Data.Entities;

namespace OlxAPI.Data.Parameters
{
    public class FilterParameters
    {
        public List<Expression<Func<Ad, bool>>> Predicates { get; set; }
    }
}
