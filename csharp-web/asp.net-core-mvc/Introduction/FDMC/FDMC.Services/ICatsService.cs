using FDMC.Domain;
using System;
using System.Collections.Generic;

namespace FDMC.Services
{
    public interface ICatsService
    {
        List<Cat> GetAllCats();

        void AddCat(string name, int age, string breed, string imageURL);

        Cat GetCatById(int id);
    }
}
