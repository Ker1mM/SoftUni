using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var softUniContext = new SoftUniContext())
            {
                string result = RemoveTown(softUniContext);
                Console.WriteLine(result);
            }
        }

        public static string RemoveTown(SoftUniContext context)
        {
            int townId = context.Towns
                .FirstOrDefault(x => x.Name == "Seattle")
                .TownId;

            context.Employees
                .Where(x => x.Address.Town.TownId == townId)
                .ToList()
                .ForEach(x => x.AddressId = null);

            var addresses = context.Addresses
                .Where(x => x.Town.TownId == townId);

            context.Addresses.RemoveRange(addresses);
            string result = $"{addresses.Count()} addresses in Seattle were deleted";
            var town = context.Towns.Find(townId);
            context.Towns.Remove(town);

            context.SaveChanges();

            return result;
        }
    }
}
