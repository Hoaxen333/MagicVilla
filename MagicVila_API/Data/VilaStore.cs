using MagicVila_API.Models.Dto;

namespace MagicVila_API.Data
{
    public static class VilaStore
    {
        public static List<VilaDto> vilaList = new List<VilaDto> { 
            new VilaDto{ Id = 1, Name = "Vista a la Piscina",Ocupantes = 345,MetrosQuadrados = 500},
            new VilaDto{ Id = 2, Name = "Vista a la Playa",Ocupantes = 543,MetrosQuadrados = 900}
        };
    }
}
