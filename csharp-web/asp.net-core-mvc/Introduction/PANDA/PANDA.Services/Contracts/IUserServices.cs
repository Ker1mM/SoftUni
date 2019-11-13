using PANDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PANDA.Services.Contracts
{
    public interface IUserServices
    {
        List<PandaUser> GetAllUsers();
    }
}
