using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int StoreId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }
    }
}
