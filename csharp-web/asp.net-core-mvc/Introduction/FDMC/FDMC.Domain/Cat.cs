using System;
using System.ComponentModel.DataAnnotations;

namespace FDMC.Domain
{
    public class Cat
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 50)]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
