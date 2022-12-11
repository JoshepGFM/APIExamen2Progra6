using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIExamen2.Models;

namespace APIExamen2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralsController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public GeneralsController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Generals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<General>>> GetGenerals()
        {
            return await _context.Generals.ToListAsync();
        }

        // GET: api/Generals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<General>> GetGeneral(int id)
        {
            var general = await _context.Generals.FindAsync(id);

            if (general == null)
            {
                return NotFound();
            }

            return general;
        }

        // PUT: api/Generals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneral(int id, General general)
        {
            if (id != general.Idconfig)
            {
                return BadRequest();
            }

            _context.Entry(general).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralExists(id))
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

        // POST: api/Generals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<General>> PostGeneral(General general)
        {
            _context.Generals.Add(general);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeneral", new { id = general.Idconfig }, general);
        }

        // DELETE: api/Generals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneral(int id)
        {
            var general = await _context.Generals.FindAsync(id);
            if (general == null)
            {
                return NotFound();
            }

            _context.Generals.Remove(general);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneralExists(int id)
        {
            return _context.Generals.Any(e => e.Idconfig == id);
        }
    }
}
