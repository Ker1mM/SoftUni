using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetClinics.Models
{
    public class Clinic : IEnumerable<int>
    {
        private int roomCount;
        private Pet[] rooms;
        public string Name { get; private set; }

        public int RoomCount
        {
            get { return roomCount; }
            private set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentException();
                }
                roomCount = value;
            }
        }

        public Clinic(string name, int roomCount)
        {
            this.Name = name;
            this.RoomCount = roomCount;
            this.rooms = new Pet[this.RoomCount];
        }

        public bool HasEmptyRooms()
        {
            return this.GetNextEmptyRoom() != -1;
        }

        private int GetNextEmptyRoom()
        {
            int result = -1;
            int middleRoom = this.roomCount / 2;
            for (int i = 0; i <= middleRoom; i++)
            {
                if (rooms[middleRoom - i] == null)
                {
                    result = middleRoom - i;
                    break;
                }
                else if (rooms[middleRoom + i] == null)
                {
                    result = middleRoom + i;
                    break;
                }
            }

            return result;
        }

        public bool Add(Pet pet)
        {
            int nextEmpty = this.GetNextEmptyRoom();

            if (nextEmpty != -1)
            {
                this.rooms[nextEmpty] = pet;
            }

            return nextEmpty != -1;
        }

        public bool Release()
        {
            bool result = false;
            int middleRoom = this.roomCount / 2;
            for (int i = 0; i <= middleRoom; i++)
            {
                if (rooms[middleRoom + i] != null)
                {
                    result = true;
                    this.rooms[middleRoom + i] = null;
                    break;
                }
                else if (rooms[middleRoom - i] != null)
                {
                    result = true;
                    this.rooms[middleRoom - i] = null;
                    break;
                }
            }

            return result;
        }

        public string RoomInfo(int roomNumber)
        {
            string result = "Room empty";
            if (rooms[roomNumber] != null)
            {
                result = rooms[roomNumber].ToString();
            }

            return result;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < this.rooms.Length; i++)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
