using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class ProcedureAnimalAid
    {
        //-	ProcedureId – integer, Primary Key
        //-	Procedure – the animal aid’s procedure(required)
        //-	AnimalAidId – integer, Primary Key
        //-	AnimalAid – the procedure’s animal aid(required)


        public int ProcedureId { get; set; }

        [ForeignKey(nameof(ProcedureId))]
        public Procedure Procedure { get; set; }

        public int AnimalAidId { get; set; }

        [ForeignKey(nameof(AnimalAidId))]
        public AnimalAid AnimalAid { get; set; }
    }
}