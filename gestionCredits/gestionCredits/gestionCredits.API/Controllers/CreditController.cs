using gestionCredits.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using projet.models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gestionCredits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        IRepository<Credit> repository;
        IRepository<CreditApplication> carepository;
        protected IUnitOfWork unitOfWork;

        public CreditController(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
            repository = unitOfWork.GetRepository<Credit>();
            carepository = unitOfWork.GetRepository<CreditApplication>();
        }
        private decimal CalculateTotalDue(Credit credit)
        {
            return credit.Balance / credit.NumberOfMonthsToRepay;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var credit = repository.Get(id);
            if (credit == null)
            {
                return NotFound("credit not found.");
            }
            return Ok(credit);
        }

        [HttpGet("account/{accountnumber}")]
        public IList<Credit> Index(string accountnumber)
        {
            var credits = repository.GetAll();
            var filteredcredits = credits.Where(t => t.AccountNumber == accountnumber).ToList();

            return filteredcredits;
        }

        [HttpPost]
        public ActionResult Create(Credit credit)
        {
            credit.TotalDue = CalculateTotalDue(credit);
            //credit.capacity = CalculateCapacity(credit.AccountNumber);
            repository.Add(credit);
            unitOfWork.Save();
            CalculateTotalDue(credit);

            return CreatedAtAction(nameof(Get), new { id = credit.Id }, credit);


        }

        [HttpPut("{id}")]
        public IActionResult UpdateCredit(int id, [FromBody] Credit credit)
        {
            if (id != credit.Id)
            {
                return BadRequest();
            }

            repository.Update(credit);
            unitOfWork.Save();

            CalculateTotalDue(credit);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCredit(int id)
        {
            var credit = repository.Get(id);
            if (credit == null)
            {
                return NotFound();
            }

            repository.Delete(credit);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteCredits([FromBody] IList<Credit> credits)
        {
            foreach (var credit in credits)
            {
                repository.Delete(credit);
            }
            unitOfWork.Save();

            return NoContent();
        }

        private decimal CalculateCapacity(string accountNumber)
        {
            try
            {
                var creditApplications = carepository.GetAll().Where(ca => ca.AccountNumber == accountNumber);
                var credits = repository.GetAll().Where(c => c.AccountNumber == accountNumber);

                decimal totalDue = credits.Sum(c => c.TotalDue);
                decimal salarity = creditApplications.FirstOrDefault()?.salarity ?? 0m; 

                if (salarity <= 0 || totalDue <= 0)
                {
                    Console.WriteLine($"Salarity or Total Due is zero or negative for account number: {accountNumber}");
                    return -1; 
                }

                decimal capacity = (salarity * 0.4m) - totalDue;

                return capacity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -3;
            }
        }

    }
}