using gestionCredits.data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projet.models;
using System.Linq;
using projet.models;
using user = projet.models.User;


namespace gestionCredits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IRepository<user> repository;
        protected IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
            repository = unitOfWork.GetRepository<user>();
        }

        [HttpGet("account/{accountnumber}")]
        public user Index(string accountnumber)
        {
            var users = repository.GetAll();
            foreach (user user in users)
            {
                if (user.AccountNumber == accountnumber)
                {
                    return user;
                }
            }

            return null;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = repository.Get(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }
        [HttpGet]
        public IList<User> GetAll(int id)
        {
            return repository.GetAll().ToList();
        }
        // POST: api/User
        [HttpPost]
        public ActionResult Create(user user)
        {
            repository.Add(user);
            unitOfWork.Save();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] user user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            repository.Update(user);
            unitOfWork.Save();

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            repository.Delete(user);
            unitOfWork.Save();

            return NoContent();
        }
    }
}

