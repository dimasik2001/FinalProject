using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlxAPI.Models.ViewModels
{
    public class AdViewModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<string> Images { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
