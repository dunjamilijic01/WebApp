using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Meni")]
    public class Meni
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DayOfWeek Dan { get; set; }

        [Required]
        public String Naziv{get;set;}
        

        [Required]
        [JsonIgnore]
        public Jelo JeloNaMeniju { get; set; }

        public Kantina Kantina { get;set; }
    }
}