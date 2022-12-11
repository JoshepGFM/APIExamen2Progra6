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
    public class UsersController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public UsersController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/GetUsersData?id=1012
        [HttpGet("GetUsersData")]
        public ActionResult<IEnumerable<User>> GetUsersData(int id)
        {
            var query = (from u in _context.Users
                         join r in _context.UserRoles on u.UserRoleId equals r.UserRoleId
                         join c in _context.Countries on u.CountryId equals c.CountryId
                         join s in _context.UserStatuses on u.UserStatusId equals s.UserStatusId
                         where u.UserId == id
                         select new
                         {
                             id = u.UserId,
                             usuario = u.UserName,
                             nombre = u.FirstName,
                             Apellido = u.LastName,
                             telefono = u.PhoneNumber,
                             recuento = u.StrikeCount,
                             correoRespaldo = u.BackUpEmail,
                             dTrabajo = u.JobDescription,
                             estadoId = u.UserStatusId,
                             rolId = u.UserRoleId,
                             paisId = u.CountryId

                         }).ToList();

            List<User> list = new List<User>();

            foreach (var item in query)
            {
                User NewItem = new User();

                NewItem.UserId = item.id;
                NewItem.UserName = item.usuario;
                NewItem.FirstName = item.nombre;
                NewItem.LastName = item.Apellido;
                NewItem.PhoneNumber = item.telefono;
                NewItem.StrikeCount = item.recuento;
                NewItem.BackUpEmail = item.correoRespaldo;
                NewItem.JobDescription = item.dTrabajo;
                NewItem.UserStatusId = item.estadoId;
                NewItem.UserRoleId = item.rolId;
                NewItem.CountryId = item.paisId;

                list.Add(NewItem);

            }

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
