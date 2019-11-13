namespace P8_MilitaryElite.Miscellaneous
{
    public class Mission : IMission
    {
        public string CodeName { get; set; }
        public string Status { get; set; }

        public Mission(string codeName, string status)
        {
            CodeName = codeName;
            Status = status;
        }

        public override string ToString()
        {
            return $"Code Name: {CodeName} State: {Status}";
        }
    }
}
