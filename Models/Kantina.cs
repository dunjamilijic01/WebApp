using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models{
    [Table("Kantina")]

    public class Kantina{
       
         [Key]
        public int ID { get; set; }


        [MaxLength(50)]
        [Required]
        public string NazivKantine { get; set; }

        [JsonIgnore]
        public List<Radnik> ListaRadnika { get; set; }

        public List<Meni> ListaMenija { get; set; }
    }
}