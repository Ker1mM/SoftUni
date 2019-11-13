using PANDA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PANDA.App.ViewModels
{
    public class RecipientViewModel
    {
        public RecipientViewModel()
        {
            this.Users = new List<PandaUser>();
        }
        public List<PandaUser> Users { get; set; }
    }
}
