using System;
using System.Linq;
using System.Text;

namespace P9_CollectionHierarchy
{
    public class StartUp
    {
        static void Main()
        {
            string[] elementsToAdd = Console.ReadLine().Split();
            int removeOperationCount = int.Parse(Console.ReadLine());

            var addCollection = new AddCollection();
            var addRemoveCollection = new AddRemoveCollection();
            var myList = new MyList();

            StringBuilder sb = new StringBuilder();
            elementsToAdd.ToList().ForEach(x => sb.Append(addCollection.Add(x) + " "));
            Console.WriteLine(sb.ToString().Trim());
            sb.Clear();

            elementsToAdd.ToList().ForEach(x => sb.Append(addRemoveCollection.Add(x) + " "));
            Console.WriteLine(sb.ToString().Trim());
            sb.Clear();

            elementsToAdd.ToList().ForEach(x => sb.Append(myList.Add(x) + " "));
            Console.WriteLine(sb.ToString().Trim());
            sb.Clear();

            var sb2 = new StringBuilder();
            for (int i = 0; i < removeOperationCount; i++)
            {
                sb.Append(addRemoveCollection.Remove() + " ");
                sb2.Append(myList.Remove() + " ");
            }

            Console.WriteLine(sb.ToString().Trim());
            Console.WriteLine(sb2.ToString().Trim());
        }
    }
}
