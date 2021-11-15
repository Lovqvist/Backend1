using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Backend1_2.Data;
using Backend1_2.Model;
using Backend1_2.Entities;

namespace Dbfirst_ehandel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SqlContext _context;

        public UsersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUser>>> GetUsers()
        {
             
            var users = new List<GetUser>();

            foreach (var user in await _context.Users.ToListAsync())
            users.Add(new GetUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUser>> GetUser(int id)
        {
            var users = await _context.Users.ToListAsync();

            if (users != null)
            {
                foreach (var user in users)
                {
                    if (user.Id == id)
                    {
                        var oneUser = new GetUser();
                        oneUser.Id = user.Id;
                        oneUser.FirstName = user.FirstName;
                        oneUser.LastName = user.LastName;
                        oneUser.Email = user.Email;

                        return oneUser;
                    }
                    else
                    {
                        return NotFound();

                    }

                } 
                
            } return BadRequest();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUser model)
        {
            var checkEmail = model.Email;
            var checkPassword = model.Password;
            var user = await _context.Users.FindAsync(id);

            Regex emailRegex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match emailMatch = emailRegex.Match(checkEmail);

            Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
            Match passwordMatch = passwordRegex.Match(checkPassword);


            if (id != model.Id)
            {
                return BadRequest();
            }

            
            if (user == null)
            {
                return NotFound();
            }

            if(emailMatch.Success && passwordMatch.Success){
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Password = model.Password;

                _context.Entry(user).State = EntityState.Modified;
            }else
            {
                return BadRequest();
            }
            

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
        public async Task<ActionResult<User>> PostUser(CreateUser model)
        {
            var customer = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            if(customer == null)
            {
                var checkEmail = model.Email;
                var checkPassword = model.Password;

                Regex emailRegex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match emailMatch = emailRegex.Match(checkEmail);

                Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
                Match passwordMatch = passwordRegex.Match(checkPassword);

                if (emailMatch.Success && passwordMatch.Success)
                {
                    var user = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetUser", new { id = user.Id }, user);
                } else
                {
                    return BadRequest();
                }
            } else
            {
               
               return BadRequest("A user with the same email already exist");
            }
            
                
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Hej");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
