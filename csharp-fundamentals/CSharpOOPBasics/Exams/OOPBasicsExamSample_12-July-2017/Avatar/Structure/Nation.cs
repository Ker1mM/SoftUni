using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Nation
{
    public List<Bender> Benders;
    public List<Monument> Monuments;
    public double Power
    {
        get
        {
            double totalPower = GetTotalBenderPower();
            totalPower += (totalPower / 100.0) * GetTotalMonumentPower();

            return totalPower;
        }
    }

    public Nation()
    {
        Benders = new List<Bender>();
        Monuments = new List<Monument>();
    }

    public void AddBender(Bender bender)
    {
        Benders.Add(bender);
    }

    public void AddMonument(Monument monument)
    {
        Monuments.Add(monument);
    }

    private double GetTotalBenderPower()
    {
        return Benders.Sum(x => x.GetPower());
    }

    private int GetTotalMonumentPower()
    {
        return Monuments.Sum(x => x.GetPower());
    }

    public void Surrender()
    {
        Benders.Clear();
        Monuments.Clear();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Benders: ");
        if (Benders.Count == 0)
        {
            sb.Append("None");
        }
        else
        {
            sb.Append(Environment.NewLine);
            sb.Append($"###{string.Join(Environment.NewLine + "###", Benders)}");
        }
        sb.Append(Environment.NewLine);
        sb.Append("Monuments: ");
        if (Monuments.Count == 0)
        {
            sb.Append("None");
        }
        else
        {
            sb.Append(Environment.NewLine);
            sb.Append($"###{string.Join(Environment.NewLine + "###", Monuments)}");
        }
        return sb.ToString();
    }
}
