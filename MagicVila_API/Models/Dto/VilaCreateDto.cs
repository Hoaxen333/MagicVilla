using System.ComponentModel.DataAnnotations;

namespace MagicVila_API.Models.Dto
{
    public class VilaCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosQuadrados { get; set; }
        public string ImageUrl {  get; set; }
        public string Amenidad { get; set; }
    }
}
