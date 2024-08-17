using AutoMapper;
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

        private readonly IMapper mapper;
        public VilaController(ILogger<VilaController> logger, ApplicationDbContext db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VilaDto>>> GetVilas() {
            _logger.LogInformation("Obtendo as Vilas");

            IEnumerable<Vila> vilaList = await _db.vilas.ToListAsync();

            return Ok(mapper.Map<IEnumerable<VilaDto>>(vilaList));
        }

        [HttpGet("id:int", Name ="GetVila")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<VilaDto>> GetVila(int id) {
            if (id == 0) {
                _logger.LogError("Erro aos trazer uma Vila de id 0");
                return BadRequest();
            }
           var villa = await _db.vilas.FirstOrDefaultAsync(x => x.Id == id);

            if (villa == null) { 
                return NotFound();
            }

            return Ok(mapper.Map<VilaDto>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VilaDto>> CreateVila([FromBody] VilaCreateDto vila) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (await _db.vilas.FirstOrDefaultAsync(v => v.Name.ToLower() == vila.Name.ToLower()) != null) {
                ModelState.AddModelError("O nome ja existe", "Uma vila com esse nome ja existe");
            }
            if (vila == null) {
                return BadRequest(vila);
            }

            Vila modelo = mapper.Map<Vila>(vila);

            await _db.vilas.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVila",new { Id = modelo.Id},vila);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVila(int id) {
            if (id==0) { 
                return BadRequest();
            }
            var vila = await _db.vilas.FirstOrDefaultAsync(v => v.Id==id);
            if (vila == null) { 
                return NotFound();
            }
            _db.vilas.Remove(vila);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVilaAsync(int id, [FromBody] VilaUpdateDto vila) {
            if (vila == null || id != vila.Id) {
                return BadRequest();
            }

            Vila modelo = mapper.Map<Vila>(vila);
            
            _db.vilas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdatePartialVila(int id, JsonPatchDocument<VilaUpdateDto> patch)
        {
            if (patch == null || id == 0)
            {
                return BadRequest();
            }
            var vila = await _db.vilas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            VilaUpdateDto modeloDto = mapper.Map<VilaUpdateDto>(vila);

            if (vila == null)
            {
                return BadRequest(ModelState);
            }
            patch.ApplyTo(modeloDto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Vila modelo = mapper.Map<Vila>(vila);
           
            _db.vilas.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
