using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagement.Models;
using ZooManagement.Data;

namespace ZooManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnimaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Animais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimais()
        {
            return await _context.Animais
                .Include(a => a.AnimalCuidados)
                    .ThenInclude(ac => ac.Cuidado)
                .ToListAsync();
        }

        // GET: api/Animais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animais
                .Include(a => a.AnimalCuidados)
                    .ThenInclude(ac => ac.Cuidado)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // POST: api/Animais
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Animais.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        // PUT: api/Animais/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // DELETE: api/Animais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Remover relações de cuidados
            var relacoes = _context.AnimalCuidados.Where(ac => ac.AnimalId == id);
            _context.AnimalCuidados.RemoveRange(relacoes);

            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Animais/5/AddCuidado/2
        [HttpPost("{animalId}/AddCuidado/{cuidadoId}")]
        public async Task<IActionResult> AddCuidadoToAnimal(int animalId, int cuidadoId)
        {
            var animal = await _context.Animais.FindAsync(animalId);
            var cuidado = await _context.Cuidados.FindAsync(cuidadoId);

            if (animal == null || cuidado == null)
            {
                return NotFound();
            }

            // Verificar se a relação já existe
            var relacaoExistente = await _context.AnimalCuidados
                .AnyAsync(ac => ac.AnimalId == animalId && ac.CuidadoId == cuidadoId);

            if (relacaoExistente)
            {
                return BadRequest("Este cuidado já está associado a este animal.");
            }

            _context.AnimalCuidados.Add(new AnimalCuidado
            {
                AnimalId = animalId,
                CuidadoId = cuidadoId
            });

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Animais/5/RemoveCuidado/2
        [HttpDelete("{animalId}/RemoveCuidado/{cuidadoId}")]
        public async Task<IActionResult> RemoveCuidadoFromAnimal(int animalId, int cuidadoId)
        {
            var relacao = await _context.AnimalCuidados
                .FirstOrDefaultAsync(ac => ac.AnimalId == animalId && ac.CuidadoId == cuidadoId);

            if (relacao == null)
            {
                return NotFound();
            }

            _context.AnimalCuidados.Remove(relacao);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return _context.Animais.Any(e => e.Id == id);
        }
    }
}