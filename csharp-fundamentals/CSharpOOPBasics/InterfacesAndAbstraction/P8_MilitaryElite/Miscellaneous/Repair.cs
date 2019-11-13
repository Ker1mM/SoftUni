namespace P8_MilitaryElite.Miscellaneous
{
    public class Repair : IRepair
    {
        public string PartName { get; set; }
        public int WorkedHours { get; set; }

        public Repair(string partName, int hours)
        {
            PartName = partName;
            WorkedHours = hours;
        }

        public override string ToString()
        {
            return $"Part Name: {PartName} Hours Worked: {WorkedHours}";
        }
    }
}
