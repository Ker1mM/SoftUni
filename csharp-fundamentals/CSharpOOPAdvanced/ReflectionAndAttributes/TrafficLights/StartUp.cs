using System;
using System.Linq;

namespace TrafficLights
{
    public class StartUp
    {
        static void Main()
        {
            string[] args = Console.ReadLine().Split();
            int count = int.Parse(Console.ReadLine());

            var lights = GetLights(args);
            for (int i = 0; i < count; i++)
            {
                Signal(lights);
                Console.WriteLine(string.Join(" ", lights.ToList()));
            }

        }

        private static void Signal(TrafficLight[] lights)
        {
            foreach (var light in lights)
            {
                light.ChangeColor();
            }
        }

        private static TrafficLight[] GetLights(string[] inputArgs)
        {
            var result = new TrafficLight[inputArgs.Length];

            for (int i = 0; i < inputArgs.Length; i++)
            {
                LightColor current = (LightColor)Enum.Parse(typeof(LightColor), inputArgs[i]);
                result[i] = new TrafficLight(current);
            }

            return result;
        }
    }
}
