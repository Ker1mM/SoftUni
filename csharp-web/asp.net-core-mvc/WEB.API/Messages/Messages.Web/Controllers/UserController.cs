using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Messages.Web.Data;
using Messages.Web.Domain;
using Messages.Web.Jwt;
using Messages.Web.Service;
using Messages.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Messages.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MessagesDbContext db;
        private readonly JwtSettings jwtSettings;

        public UserController(MessagesDbContext db)
        {
            this.db = db;
            this.jwtSettings = new JwtSettings();
        }

        [HttpPost(Name = "register")]
        [Route("register")]
        public async Task<ActionResult> Register(CreateUserModel userModel)
        {
            if(this.db.Users.Any(x => x.Username == userModel.Username))
            {
                return this.BadRequest();
            }

            var user = new User
            {
                Username = userModel.Username,
                Password = userModel.Password
            };

            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPost(Name = "login")]
        [Route("login")]
        public ActionResult<string> Login(CreateUserModel userModel)
        {
            var user = this.GetUserByUsernameAndPassword(userModel.Username, userModel.Password);

            if (user == null)
            {
                return null;
            }

            return this.Authenticate(userModel.Username, userModel.Password);
        }

        [HttpPost(Name = "Username")]
        [Route("username")]
        public ActionResult<User> GetUsernameById(UserViewModel model)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == model.Id);

            return user;
        }
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Password == password && x.Username == username);

            return user;
        }

        public string Authenticate(string username, string password)
        {
            var user = this.GetUserByUsernameAndPassword(username, password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}