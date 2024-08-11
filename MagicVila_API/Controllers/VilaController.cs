using MagicVila_API.Data;
using MagicVila_API.Models;
using MagicVila_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly ILogger<VilaController> _logger;

        private readonly ApplicationDbContext _db;
        public VilaController(ILogger<VilaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VilaDto>> GetVilas() {
            _logger.LogInformation("Obtendo as Vilas");
            return Ok(_db.vilas.ToList());
        }

        [HttpGet("id:int", Name ="GetVila")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VilaDto> GetVila(int id) {
            if (id == 0) {
                _logger.LogError("Erro aos trazer uma Vila de id 0");
                return BadRequest();
            }
           // var villa = VilaStore.vilaList.FirstOrDefault(v => v.Id == id);
           var villa = _db.vilas.FirstOrDefault(x => x.Id == id);

            if (villa == null) { 
                return NotFound();
            }

            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VilaDto> CreateVila([FromBody] VilaDto vila) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (_db.vilas.FirstOrDefault(v => v.Name.ToLower() == vila.Name.ToLower()) != null) {
                ModelState.AddModelError("O nome ja existe", "Uma vila com esse nome ja existe");
            }
            if (vila == null) {
                return BadRequest(vila);
            }
            if (vila.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Vila modelo = new Vila()
            {
                Name = vila.Name,
                Description = vila.Description,
                Ocupantes = vila.Ocupantes,
                Amenidad = vila.Amenidad,
                ImageUrl = vila.ImageUrl,
                Tarifa = vila.Tarifa,
                MetrosQuadrados = vila.MetrosQuadrados,
            };
            _db.vilas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetVila",new { Id = vila.Id},vila);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVila(int id) {
            if (id==0) { 
                return BadRequest();
            }
            var vila = _db.vilas.FirstOrDefault(v => v.Id==id);
            if (vila == null) { 
                return NotFound();
            }
            _db.vilas.Remove(vila);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateVila(int id, [FromBody] VilaDto vila) {
            if (vila == null || id != vila.Id) {
                return BadRequest();
            }
            //var villa = VilaStore.vilaList.FirstOrDefault(v => v.Id==id);
            //villa.Name = vila.Name;
            //villa.Ocupantes = vila.Ocupantes;
            //villa.MetrosQuadrados = vila.MetrosQuadrados;

            Vila modelo = new()
            {
                Id = vila.Id,
                Name = vila.Name,
                Description = vila.Description,
                Ocupantes = vila.Ocupantes,
                Amenidad = vila.Amenidad,
                ImageUrl = vila.ImageUrl,
                Tarifa = vila.Tarifa,
                MetrosQuadrados = vila.MetrosQuadrados,
            };
            _db.vilas.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult UpdatePartialVila(int id, JsonPatchDocument<VilaDto> patch)
        {
            if (patch == null || id == 0)
            {
                return BadRequest();
            }
            var vila = _db.vilas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VilaDto modeloDto = new()
            {
                Id = vila.Id,
                Name = vila.Name,
                Description = vila.Description,
                Ocupantes = vila.Ocupantes,
                Amenidad = vila.Amenidad,
                ImageUrl = vila.ImageUrl,
                Tarifa = vila.Tarifa,
                MetrosQuadrados = vila.MetrosQuadrados,
            };
            if (vila == null)
            {
                return BadRequest(ModelState);
            }
            patch.ApplyTo(modeloDto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Vila modelo = new()
            {
                Id = vila.Id,
                Name = vila.Name,
                Description = vila.Description,
                Ocupantes = vila.Ocupantes,
                Amenidad = vila.Amenidad,
                ImageUrl = vila.ImageUrl,
                Tarifa = vila.Tarifa,
                MetrosQuadrados = vila.MetrosQuadrados,
            };
           
            _db.vilas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
