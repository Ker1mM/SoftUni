using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    [Table("Seats")]
    public class Seat
    {
        //•	Id – integer, Primary Key
        //•	HallId – integer, foreign key(required)
        //•	Hall – the seat’s hall

        public int Id { get; set; }

        public int HallId { get; set; }

        [ForeignKey(nameof(HallId))]
        public Hall Hall { get; set; }
    }
}