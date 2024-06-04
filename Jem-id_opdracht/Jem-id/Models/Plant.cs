using System.ComponentModel.DataAnnotations;

namespace Jem_id.webapi.Models
{
    public class Plant
    {
        [Key]
        public string Code { get; set; }

        public string Naam { get; set; }
        public int Potmaat { get; set; }
        public int Pothoogte { get; set; }
        public string? Kleur { get; set; }
        public string Productgroep { get; set; }
    }
}
