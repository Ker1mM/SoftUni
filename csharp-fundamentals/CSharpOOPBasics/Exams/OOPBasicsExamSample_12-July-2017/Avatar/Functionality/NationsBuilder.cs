using System;
using System.Collections.Generic;
using System.Text;

public class NationsBuilder
{
    private Dictionary<string, Nation> Nations;
    private Queue<string> Wars;

    public NationsBuilder()
    {
        Nations = new Dictionary<string, Nation>();
        Wars = new Queue<string>();
        Nations.Add("AirBender", new Nation());
        Nations.Add("WaterBender", new Nation());
        Nations.Add("EarthBender", new Nation());
        Nations.Add("FireBender", new Nation());
    }

    public void AssignBender(List<string> benderArgs)
    {
        Bender bender = BenderFactory.GetBender(benderArgs);
        string type = bender.GetType().FullName;
        Nations[type].AddBender(bender);
    }
    public void AssignMonument(List<string> monumentArgs)
    {
        Monument monument = MonumentFactory.GetMonument(monumentArgs);
        string type = monument.GetType().FullName;
        type = type.Replace("Monument", "Bender");
        Nations[type].AddMonument(monument);
    }

    public string GetStatus(string nationsType)
    {
        Console.WriteLine($"{nationsType} Nation");
        switch (nationsType)
        {
            case "Air":
                return Nations["AirBender"].ToString();
            case "Water":
                return Nations["WaterBender"].ToString();
            case "Earth":
                return Nations["EarthBender"].ToString();
            case "Fire":
                return Nations["FireBender"].ToString();
            default:
                throw new ArgumentException("Invalid Nation Type");
        }
    }

    public void IssueWar(string nationsType)
    {
        Wars.Enqueue(nationsType);

        double airPower = Nations["AirBender"].Power;
        double waterPower = Nations["WaterBender"].Power;
        double earthPower = Nations["EarthBender"].Power;
        double firePower = Nations["FireBender"].Power;

        double winner = Math.Max(airPower, waterPower);
        winner = Math.Max(winner, earthPower);
        winner = Math.Max(winner, firePower);

        if (winner != airPower)
        {
            Nations["AirBender"].Surrender();
        }

        if (winner != waterPower)
        {
            Nations["WaterBender"].Surrender();
        }

        if (winner != earthPower)
        {
            Nations["EarthBender"].Surrender();
        }

        if (winner != firePower)
        {
            Nations["FireBender"].Surrender();
        }
    }


    public string GetWarsRecord()
    {
        int counter = 1;
        StringBuilder sb = new StringBuilder();
        while (Wars.Count > 1)
        {
            sb.Append($"War {counter} issued by {Wars.Dequeue()}");
            sb.Append(Environment.NewLine);
            counter++;
        }
        sb.Append($"War {counter} issued by {Wars.Dequeue()}");
        return sb.ToString();
    }


}

