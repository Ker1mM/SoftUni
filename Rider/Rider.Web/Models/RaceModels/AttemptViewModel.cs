using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Models.RaceModels
{
    public class AttemptViewModel
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime AttemptedOn { get; set; }

        public string PlayerUsername { get; set; }

        public string TrackName { get; set; }

    }
}
