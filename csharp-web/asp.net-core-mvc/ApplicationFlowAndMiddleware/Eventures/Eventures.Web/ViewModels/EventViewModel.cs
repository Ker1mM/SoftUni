using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.ViewModels
{
    public class EventViewModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Place { get; set; }
    }
}
