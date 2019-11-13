using Messages.Web.Data;
using Messages.Web.Domain;
using Messages.Web.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Web.Service
{
    public class UserService
    {
        private readonly MessagesDbContext context;
        private readonly JwtSettings jwtSettings;

        public UserService(MessagesDbContext context)
        {
            this.context = context;
            this.jwtSettings = new JwtSettings();
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

        public async Task<User> RegisterAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var user = this.context.Users.FirstOrDefault(x => x.Password == password && x.Username == username);

            return user;
        }
    }
}
