namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Linq;

    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            var prisoner = context.Prisoners.FirstOrDefault(x => x.Id == prisonerId);

            var result = string.Empty;
            if (prisoner == null)
            {
                result = "Prisoner not found!";
            }
            else if (prisoner.ReleaseDate == null)
            {
                result = $"Prisoner {prisoner.FullName} is sentenced to life";
            }
            else
            {
                result = $"Prisoner {prisoner.FullName} released";
                prisoner.ReleaseDate = DateTime.Now;
                context.SaveChanges();
            }

            return result;
        }
    }
}
