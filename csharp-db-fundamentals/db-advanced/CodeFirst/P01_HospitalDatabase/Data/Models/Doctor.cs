using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Doctors")]
    public class Doctor
    {
        public Doctor()
        {
            Visitations = new HashSet<Visitation>();
        }

        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
    }
}
