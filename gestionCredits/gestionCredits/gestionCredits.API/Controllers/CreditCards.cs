using gestionCredits.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projet.models;

namespace gestionCredits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly IRepository<CreditCards> repository;
        private readonly IUnitOfWork unitOfWork;

        public CreditCardsController(IUnitOfWork unit)
        {
            unitOfWork = unit;
            repository = unitOfWork.GetRepository<CreditCards>();
        }

        [HttpGet]
        public ActionResult<IList<CreditCards>> Index(string accountNumber)
        {
            var cards = repository.GetAll();
            var filteredCards = cards.Where(t => t.AccountNumber == accountNumber).ToList();

            if (filteredCards.Count == 0)
            {
                return NotFound();
            }

            return Ok(filteredCards);
        }



        [HttpGet("{id}")]
        public ActionResult<CreditCards> Get(int id)
        {
            var card = repository.Get(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, CreditCards card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            repository.Update(card);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<CreditCards> Create(CreditCards creditCard)
        {
            repository.Add(creditCard);
            unitOfWork.Save();

            return CreatedAtAction(nameof(Get), new { id = creditCard.Id }, creditCard);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var card = repository.Get(id);
            if (card == null)
            {
                return NotFound();
            }

            repository.Delete(card);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpPut("ToggleStatus/{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var card = repository.Get(id);
            if (card == null)
            {
                return NotFound();
            }
            card.Statut = card.Statut == "Activé" ? "Désactivé" : "Activé";

            repository.Update(card);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAll(IList<int> ids)
        {
            foreach (var id in ids)
            {
                var card = repository.Get(id);
                if (card == null)
                {
                    return NotFound();
                }
                repository.Delete(card);
            }
            unitOfWork.Save();

            return NoContent();
        }
    }
}
