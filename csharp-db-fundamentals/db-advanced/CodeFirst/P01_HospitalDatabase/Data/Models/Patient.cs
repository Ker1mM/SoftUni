using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Patients")]
    public class Patient
    {
        public Patient()
        {
            Prescriptions = new HashSet<PatientMedicament>();
            Diagnoses = new HashSet<Diagnose>();
            Visitations = new HashSet<Visitation>();
        }

        [Key]
        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(80)]
        public string Email { get; set; }

        [Required]
        public bool HasInsurance { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }
        public ICollection<Diagnose> Diagnoses { get; set; }
        public ICollection<Visitation> Visitations { get; set; }
    }
}
