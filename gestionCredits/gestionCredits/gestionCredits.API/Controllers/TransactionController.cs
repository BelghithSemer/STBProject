using gestionCredits.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.models;
using System.Collections.Generic;

namespace gestionCredits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IRepository<Transaction> repository;
        private readonly IUnitOfWork unitOfWork;

        public TransactionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<Transaction>();
        }

        [HttpGet("account/{accountnumber}")]
        public IList<Transaction> Index(string accountnumber)
        {
            var transactions = repository.GetAll();

            // Filter transactions locally
            var filteredTransactions = transactions.Where(t => t.AccountNumber == accountnumber).ToList();

            /*if (filteredTransactions == null || filteredTransactions.Count == 0)
            {
                return NotFound("Aucune transaction trouvée pour ce numéro de compte.");
            }*/

            return filteredTransactions;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var transaction = repository.Get(id);
            if (transaction == null)
            {
                return NotFound("Transaction not found.");
            }
            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            repository.Add(transaction);
            unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            repository.Update(transaction);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = repository.Get(id);
            if (transaction == null)
            {
                return NotFound();
            }

            repository.Delete(transaction);
            unitOfWork.Save();

            return NoContent();
        }
    }
}
