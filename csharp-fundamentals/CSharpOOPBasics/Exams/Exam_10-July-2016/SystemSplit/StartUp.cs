using System;
public class StartUp
{
    static void Main(string[] args)
    {
        var theSystem = new TheSystem();
        string input;
        while ((input = Console.ReadLine()) != "System Split")
        {
            theSystem.ExecuteCommand(input);
        }

        theSystem.End();
    }
}
