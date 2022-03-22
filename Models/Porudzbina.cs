using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Porudzbina")]
    public class Porudzbina
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public bool PreuzetaPorudzbina { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [JsonIgnore]
        public Radnik PorudzbinaZa { get; set; }
        
        [Required]
        public Jelo JeloPorudzbine  { get; set; }

    }
}