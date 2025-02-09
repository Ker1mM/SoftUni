﻿namespace P5_MordorsCruelPlan.Factories
{
    using P5_MordorsCruelPlan.Factories.Foods;
    public static class FoodFactory
    {
        public static Food GetFood(string name)
        {
            switch (name.ToLower())
            {
                case "apple":
                    return new Apple();
                case "cram":
                    return new Cram();
                case "honeycake":
                    return new HoneyCake();
                case "lembas":
                    return new Lembas();
                case "melon":
                    return new Melon();
                case "mushrooms":
                    return new Mushrooms();
                default:
                    return new Other();
            }
        }
    }
}
