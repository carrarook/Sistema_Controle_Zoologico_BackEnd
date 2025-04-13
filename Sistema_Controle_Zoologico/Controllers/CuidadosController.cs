using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Controle_Zoologico.Models;
using Sistema_Controle_Zoologico.Data;

namespace ZooManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuidadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CuidadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cuidados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuidado>>> GetCuidados()
        {
            return await _context.Cuidados
                .Include(c => c.AnimalCuidados)
                    .ThenInclude(ac => ac.Animal)
                .ToListAsync();
        }

        // GET: api/Cuidados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuidado>> GetCuidado(int id)
        {
            var cuidado = await _context.Cuidados
                .Include(c => c.AnimalCuidados)
                    .ThenInclude(ac => ac.Animal)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuidado == null)
            {
                return NotFound();
            }

            return cuidado;
        }

        // POST: api/Cuidados
        [HttpPost]
        public async Task<ActionResult<Cuidado>> PostCuidado(Cuidado cuidado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cuidados.Add(cuidado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuidado), new { id = cuidado.Id }, cuidado);
        }

        // PUT: api/Cuidados/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuidado(int id, Cuidado cuidado)
        {
            if (id != cuidado.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(cuidado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuidadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Cuidados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuidado(int id)
        {
            var cuidado = await _context.Cuidados.FindAsync(id);
            if (cuidado == null)
            {
                return NotFound();
            }

            // Remover relações com animais
            var relacoes = _context.AnimalCuidados.Where(ac => ac.CuidadoId == id);
            _context.AnimalCuidados.RemoveRange(relacoes);

            _context.Cuidados.Remove(cuidado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Adicionar cuidado ao animal
        [HttpPost("{cuidadoId}/animais/{animalId}")]
        public async Task<IActionResult> AdicionarAnimalAoCuidado(int cuidadoId, int animalId)
        {
            var animalCuidado = new AnimalCuidado
            {
                CuidadoId = cuidadoId,
                AnimalId = animalId
            };

            _context.AnimalCuidados.Add(animalCuidado);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //deletar cuidado do animal
        [HttpDelete("{cuidadoId}/animais/{animalId}")]
        public async Task<IActionResult> RemoverAnimalDoCuidado(int cuidadoId, int animalId)
        {
            var relacao = await _context.AnimalCuidados
                .FirstOrDefaultAsync(ac => ac.CuidadoId == cuidadoId && ac.AnimalId == animalId);

            if (relacao == null) return NotFound();

            _context.AnimalCuidados.Remove(relacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // GET: api/Cuidados/5/Animais
        [HttpGet("{id}/Animais")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimaisPorCuidado(int id)
        {
            var cuidado = await _context.Cuidados
                .Include(c => c.AnimalCuidados)
                    .ThenInclude(ac => ac.Animal)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuidado == null)
            {
                return NotFound();
            }

            var animais = cuidado.AnimalCuidados.Select(ac => ac.Animal).ToList();
            return animais;
        }

        private bool CuidadoExists(int id)
        {
            return _context.Cuidados.Any(e => e.Id == id);
        }
    }
}