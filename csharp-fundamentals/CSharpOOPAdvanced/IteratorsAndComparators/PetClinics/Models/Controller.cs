using System;
using System.Collections.Generic;
using System.Text;

namespace PetClinics.Models
{
    public class Controller
    {
        private List<Clinic> clinicPool;
        private List<Pet> petPool;

        public Controller()
        {
            this.clinicPool = new List<Clinic>();
            this.petPool = new List<Pet>();
        }

        public void Create(string[] args)
        {
            if (args[0] == "Pet")
            {
                var pet = new Pet(args[1], int.Parse(args[2]), args[3]);
                petPool.Add(pet);
            }
            else if (args[0] == "Clinic")
            {
                var clinic = new Clinic(args[1], int.Parse(args[2]));
                clinicPool.Add(clinic);
            }
        }

        public bool Add(string[] args)
        {
            int clinicIndex = this.clinicPool.FindIndex(x => x.Name == args[1]);
            int petIndex = this.petPool.FindIndex(x => x.Name == args[0]);


            if (clinicIndex == -1 || petIndex == -1)
            {
                throw new ArgumentException();
            }
            var currentClinic = clinicPool[clinicIndex];
            var currentPet = petPool[petIndex];

            bool result = currentClinic.HasEmptyRooms();
            if (result)
            {
                currentClinic.Add(currentPet);
            }

            return result;
        }

        public bool HasEmptyRooms(string[] args)
        {
            int clinicIndex = this.clinicPool.FindIndex(x => x.Name == args[0]);

            if (clinicIndex == -1)
            {
                throw new ArgumentException();
            }

            bool result = this.clinicPool[clinicIndex].HasEmptyRooms();

            return result;
        }

        public bool Release(string[] args)
        {
            int clinicIndex = this.clinicPool.FindIndex(x => x.Name == args[0]);

            if (clinicIndex == -1)
            {
                throw new ArgumentException();
            }

            bool result = this.clinicPool[clinicIndex].Release();

            return result;
        }

        public string Print(string[] args)
        {
            int clinicIndex = this.clinicPool.FindIndex(x => x.Name == args[0]);
            if (clinicIndex == -1)
            {
                throw new ArgumentException();
            }

            StringBuilder sb = new StringBuilder();
            if (args.Length == 2)
            {
                sb.AppendLine(this.clinicPool[clinicIndex].RoomInfo(int.Parse(args[1])-1));
            }
            else if (args.Length == 1)
            {
                foreach (var room in clinicPool[clinicIndex])
                {
                    sb.AppendLine(clinicPool[clinicIndex].RoomInfo(room));
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return sb.ToString().TrimEnd();
        }

    }
}
