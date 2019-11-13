﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Data.Models
{
    [Table("Halls")]
    public class Hall
    {
        //•	Id – integer, Primary Key
        //•	Name – text with length[3, 20] (required)
        //•	Is4Dx - bool
        //•	Is3D - bool
        //•	Projections - collection of type Projection
        //•	Seats - collection of type Seat

        public Hall()
        {
            this.Projections = new HashSet<Projection>();
            this.Seats = new HashSet<Seat>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        //Might need to make nullable
        public bool Is4Dx { get; set; }

        //Might need to make nullable
        public bool Is3D { get; set; }

        public ICollection<Projection> Projections { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
