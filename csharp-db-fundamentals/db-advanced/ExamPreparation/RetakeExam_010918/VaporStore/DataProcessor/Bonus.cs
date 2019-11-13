namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;
    using Data;

    public static class Bonus
    {
        public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
        {
            var result = string.Empty;

            var user = context.Users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                result = $"User {username} not found";
            }
            else if (context.Users.Any(u => u.Email == newEmail))
            {
                result = $"Email {newEmail} is already taken";
            }
            else
            {
                user.Email = newEmail;
                context.SaveChanges();
                result = $"Changed {username}'s email successfully";
            }

            return result;
        }
    }
}
