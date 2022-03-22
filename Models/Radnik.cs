using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Radnik")]
    public class Radnik
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        [MaxLength(13)]
        [RegularExpression("(0[1-9]|[12][0-9]|3[01])(0[1-9]|1[012])[0-9]{9}")]
        public string JMBG { get; set; }

       [JsonIgnore]
        public List<Porudzbina> Porudzbine {get; set; }

        public Kantina kantina {get; set;} 

    }

}