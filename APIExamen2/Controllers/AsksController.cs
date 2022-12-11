using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIExamen2.Models;
using APIExamen2.Models.DTOs;

namespace APIExamen2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsksController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public AsksController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Asks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ask>>> GetAsks()
        {
            return await _context.Asks.ToListAsync();
        }

        // GET: api/Asks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ask>> GetAsk(long id)
        {
            var ask = await _context.Asks.FindAsync(id);

            if (ask == null)
            {
                return NotFound();
            }

            return ask;
        }

        // GET: api/Asks/GetAskList?R=true
        [HttpGet("GetAskList")]
        public ActionResult<IEnumerable<AskDTO>> GetAskList(bool R = true)
        {
            if (R)
            {
            var query = from a in _context.Asks
                        join s in _context.AskStatuses on a.AskStatusId equals s.AskStatusId
                        join u in _context.Users on a.UserId equals u.UserId
                        where a.AskStatusId == 1
                        select new
                        {
                            id = a.AskId,
                            data = a.Date,
                            ask = a.AskDescription,
                            userId = a.UserId,
                            statusId = a.AskStatusId,
                            isStrik = a.IsStrike,
                            image = a.ImageUrl,
                            detail = a.AskDetail,
                            user = u.FirstName + " " + u.LastName,
                            status = s.AskStatus1
                        };


            List<AskDTO> askList = new List<AskDTO>();

            foreach (var item in query)
            {
                askList.Add(
                    new AskDTO
                    {
                        AskId = item.id,
                        Date = item.data,
                        AskDescription = item.ask,
                        UserId = item.userId,
                        AskStatusId = item.statusId,
                        IsStrike = item.isStrik,
                        ImageUrl = item.image,
                        AskDetail = item.detail,
                        User = item.user,
                        Status = item.status
                    }
                    );
            }

            if (askList == null)
            {
                return NotFound();
            }

            return askList;

            }
            else
            {
                var query = from a in _context.Asks
                            join s in _context.AskStatuses on a.AskStatusId equals s.AskStatusId
                            join u in _context.Users on a.UserId equals u.UserId
                            select new
                            {
                                id = a.AskId,
                                data = a.Date,
                                ask = a.AskDescription,
                                userId = a.UserId,
                                statusId = a.AskStatusId,
                                isStrik = a.IsStrike,
                                image = a.ImageUrl,
                                detail = a.AskDetail,
                                user = u.LastName + " " + u.FirstName,
                                status = s.AskStatus1
                            };


                List<AskDTO> askList = new List<AskDTO>();

                foreach (var item in query)
                {
                    askList.Add(
                        new AskDTO
                        {
                            AskId = item.id,
                            Date = item.data,
                            AskDescription = item.ask,
                            UserId = item.userId,
                            AskStatusId = item.statusId,
                            IsStrike = item.isStrik,
                            ImageUrl = item.image,
                            AskDetail = item.detail,
                            User = item.user,
                            Status = item.status
                        }
                        );
                }

                if (askList == null)
                {
                    return NotFound();
                }

                return askList;

            }
            
        }

        // PUT: api/Asks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsk(long id, Ask ask)
        {
            if (id != ask.AskId)
            {
                return BadRequest();
            }

            _context.Entry(ask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AskExists(id))
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

        // POST: api/Asks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ask>> PostAsk(Ask ask)
        {
            _context.Asks.Add(ask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsk", new { id = ask.AskId }, ask);
        }

        // DELETE: api/Asks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsk(long id)
        {
            var ask = await _context.Asks.FindAsync(id);
            if (ask == null)
            {
                return NotFound();
            }

            _context.Asks.Remove(ask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AskExists(long id)
        {
            return _context.Asks.Any(e => e.AskId == id);
        }
    }
}
