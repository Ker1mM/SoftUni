namespace AnimalCentre.Models.Contracts
{
    public interface IAnimal
    {
        string Name { get; }
        int ProcedureTime { get; }
        string Owner { get; set; }
        bool IsAdopt { get; set; }
        bool IsChipped { get; set; }
        bool IsVaccinated { get; set; }
        int Happiness { get; set; }
        int Energy { get; set; }

        void SetProcedureTime(int procedureTime);
    }
}
