using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVila_API.Models
{
    public class Vila
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosQuadrados { get; set; }
        public string ImageUrl  { get; set; }
        public string Amenidad { get; set; }
        public DateTime DateOfFoundation { get; set; }
        public DateTime DataDeActualization { get; set; }
    }
}
