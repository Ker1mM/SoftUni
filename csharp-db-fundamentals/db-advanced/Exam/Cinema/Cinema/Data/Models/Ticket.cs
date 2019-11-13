using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        //•	Id – integer, Primary Key
        //•	Price – decimal (non-negative, minimum value: 0.01) (required)
        //•	CustomerId – integer, foreign key(required)
        //•	Customer – the customer’s ticket 
        //•	ProjectionId – integer, foreign key(required)
        //•	Projection – the projection’s ticket


        public int Id { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public int ProjectionId { get; set; }

        [ForeignKey(nameof(ProjectionId))]
        public Projection Projection { get; set; }
    }
}