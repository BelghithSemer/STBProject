using gestionCredits.data;
using Microsoft.AspNetCore.Mvc;
using projet.models;
using System.Linq;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldController : ControllerBase
    {
        IRepository<Sold> repository;
        protected IUnitOfWork unitOfWork;

        public SoldController(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
            repository = unitOfWork.GetRepository<Sold>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetTotalBalance(string accountNumber)
        {
            var sales = repository.GetAll()
                        .Where(s => s.AccountNumber == accountNumber) 
                        .Select(s => new { s.AccountName, s.Balance }) 
                        .ToList();

            if (!sales.Any())
            {
                return NotFound();
            }

            return Ok(sales);
        }

        [HttpPost]
        public void create(Sold sold)
        {
            repository.Add(sold);
            unitOfWork.Save();
           /* return CreatedAtAction(nameof(GetTotalBalance), new { accountNumber = sold.AccountNumber }, sold);*/
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Sold sold)
        {
            if (id != sold.Id)
            {
                return BadRequest();
            }

            repository.Update(sold);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sold = repository.Get(id);
            if (sold == null)
            {
                return NotFound();
            }

            repository.Delete(sold);
            unitOfWork.Save();

            return NoContent();
        }
    }
}
