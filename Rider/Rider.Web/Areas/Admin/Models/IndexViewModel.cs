using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public IEnumerable<PlayerAllViewModel> Players { get; set; }

        public string Search { get; set; }
    }
}
