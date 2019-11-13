namespace P02_BlackBoxInteger
{
    using System;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            Type type = typeof(BlackBoxInteger);
            var classInstance = Activator.CreateInstance(type, true);

            FieldInfo myNumber = type.GetField("innerValue", BindingFlags.NonPublic | BindingFlags.Instance);

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] args = input.Split("_");
                string method = args[0];
                int value = int.Parse(args[1]);

                MethodInfo invokedMethod = type.GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance);
                invokedMethod.Invoke(classInstance, new object[] { value });

                var result = myNumber.GetValue(classInstance);
                Console.WriteLine(result);
            }
        }
    }
}
