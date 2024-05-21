using gestionCredits.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.models;



namespace projet.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CreditApplicationsController(GestionCreditContext context) : ControllerBase
    {
        private readonly GestionCreditContext _context = context;

        // GET: api/CreditApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditApplication>>> GetCreditApplications()
        { 
            return await _context.CreditApplications.ToListAsync();
        }

        // POST: api/CreditApplications
        [HttpPost]
        public async Task<ActionResult<CreditApplication>> PostCreditApplication(CreditApplication creditApplication)
        {
            _context.CreditApplications.Add(creditApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditApplication", new { id = creditApplication.Id }, creditApplication);
        }

        // Les autres méthodes CRUD sont à implémenter ici
    }
}