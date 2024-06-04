using System.ComponentModel.DataAnnotations;

namespace Jem_id.webapi.Models
{
    public class Plant
    {
        [Key]
        [MaxLength(13)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naam { get; set; }
        [Required]
        public int Potmaat { get; set; }
        [Required]
        public int Pothoogte { get; set; }
        public string? Kleur { get; set; }
        [Required]
        public string Productgroep { get; set; }
    }
}
