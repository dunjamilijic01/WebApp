using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Jelo")]
    public class Jelo
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string NazivJela { get; set; }

        [MaxLength(50)]
        [Required]
        public string Restoran { get; set; }

        [Required]
        [Range(100,500)]
        public int Cena { get; set; }   

        
        [JsonIgnore]
        public List<Porudzbina> JeloUPorudzbinama { get; set; }

        [JsonIgnore]
        public List<Meni> ListaMenija { get; set; }

    }
}