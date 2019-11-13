using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FDMC.Data;
using FDMC.Domain;

namespace FDMC.Services
{
    public class CatsService : ICatsService
    {
        private readonly FdmcDBContext context;

        public CatsService(FdmcDBContext context)
        {
            this.context = context;
        }

        public void AddCat(string name, int age, string breed, string imageURL)
        {
            var cat = new Cat
            {
                Name = name,
                Age = age,
                Breed = breed,
                ImageURL = imageURL
            };

            this.context.Cats.Add(cat);

            this.context.SaveChanges();
        }

        public List<Cat> GetAllCats()
        {
            var result = this.context.Cats.ToList();

            return result;
        }

        public Cat GetCatById(int id)
        {
            var cat = this.context.Cats.FirstOrDefault(x => x.Id == id);

            return cat;
        }
    }
}
