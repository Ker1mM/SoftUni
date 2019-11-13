using Microsoft.AspNetCore.Identity;
using PANDA.Data;
using PANDA.Domain;
using PANDA.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PANDA.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<PandaUser> manager;

        public UserServices(UserManager<PandaUser> manager)
        {
            this.manager = manager;
        }

        public List<PandaUser> GetAllUsers()
        {
            var users = this.manager.Users.ToList();

            return users;
        }
    }
}
