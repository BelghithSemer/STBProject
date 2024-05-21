using gestionCredits.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.models;


namespace projet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly GestionCreditContext _context;

        public LoginController(GestionCreditContext context)
        {
            _context = context;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(); 
        }

        // POST: Login/Login
        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Logins.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Identifiant ou mot de passe incorrect.");
                    return BadRequest(ModelState); // Retourner un BadRequest avec les erreurs de validation
                }
            }
            return BadRequest(ModelState); // Retourner un BadRequest avec les erreurs de validation
        }

        [HttpGet]
        [Route("Success")]
        public IActionResult Success()
        {
            return Ok(); // Ou une autre réponse appropriée pour une API
        }

        [HttpGet]
        [Route("Fail")]
        public IActionResult Fail()
        {
            return Ok(); // Ou une autre réponse appropriée pour une API
        }
    }
}
