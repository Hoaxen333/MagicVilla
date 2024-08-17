using System.ComponentModel.DataAnnotations;

namespace MagicVila_API.Models.Dto
{
    public class VilaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosQuadrados { get; set; }
        [Required]
        public string ImageUrl {  get; set; }
        [Required]
        public string Amenidad { get; set; }
    }
}
