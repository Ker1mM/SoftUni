using System;
using System.Collections.Generic;

public class TyreFactory
{
    public static Tyre GetTyre(string[] args)
    {
        string type = args[0];
        double hardness = double.Parse(args[1]);

        if (type == "Ultrasoft")
        {
            double grip = double.Parse(args[2]);
            return new UltrasoftTyre(hardness, grip);
        }
        else if (type == "Hard")
        {
            return new HardTyre(hardness);
        }

        throw new ArgumentException("Invalid Tyre Type!");

    }
}
