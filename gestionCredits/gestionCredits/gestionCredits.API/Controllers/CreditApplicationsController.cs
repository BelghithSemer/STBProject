using gestionCredits.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.models;
using Microsoft.Net.Http.Headers;
using gestionCredits.API.Helper;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.StaticFiles;


namespace gestionCredits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditApplicationsController : ControllerBase
    {
        IRepository<CreditApplication> repository;
        protected IUnitOfWork unitOfWork;
        EmailSettings emailSettings;
        public CreditApplicationsController(IUnitOfWork unit, IOptions<EmailSettings> options)
        {
            this.unitOfWork = unit;
            repository = unitOfWork.GetRepository<CreditApplication>();
            this.emailSettings = options.Value;


        }
        [HttpGet("GetAll")]
        public IList<CreditApplication> GetAll()
        {
            var creditApplications = repository.GetAll();


            return creditApplications;
        }
        [HttpGet("AccountNumber/{accountNumber}")]
        public IList<CreditApplication> Index(string accountNumber)
        {
            var creditApplications = repository.GetAll();
            var filteredcreditApplications = creditApplications.Where(t => t.AccountNumber == accountNumber).ToList();

            return filteredcreditApplications;
        }
        // GET: api/CreditApplications/5
        [HttpGet("{id}")]
        public ActionResult<CreditApplication> GetCreditApplication(int id)
        {
            var creditApplication = repository.Get(id);

            if (creditApplication == null)
            {
                return NotFound();
            }

            return creditApplication;
        }

        /*[HttpPost("uploadfiles/{id}")]
        public async void UploadFiles(int id, [FromBody] IFormFileCollection files)
        {
            CreditApplication credit = repository.Get(id);

            foreach (var file in files)
            {

                var folderName = Path.Combine("Resources", "Attachments");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                Directory.CreateDirectory(pathToSave);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                credit.files.Append(file);
            }
            repository.Update(credit);
            unitOfWork.Save();
        } */

        [HttpPost("adddemande")]
        public async Task<IActionResult> PostCreditApplication([FromForm] CreditApplication credit)
        {
            if (credit.files != null && credit.files.Count > 0)
            {
                var uploadedFiles = new List<string>();

                foreach (var file in credit.files)
                {
                    var folderName = Path.Combine("Resources", "Attachments");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                    var fullPath = Path.Combine(pathToSave, fileName.ToString());

                    Directory.CreateDirectory(pathToSave);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploadedFiles.Add(fullPath);
                }

                credit.FilePaths = uploadedFiles;
            }

            repository.Add(credit);
            unitOfWork.Save();

            return Ok(credit);
        }

        [HttpGet("filename")]
        public IActionResult GetFile(string fullFilePath)
        {
            // Extract just the filename from the full path
            var filename = Path.GetFileName(fullFilePath);

            // Define the base directory where your files are stored
            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Attachments");

            // Combine the base directory with the filename
            var path = Path.Combine(baseDirectory, filename);

            // Check if the file exists
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            // Determine the content type
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            // Read the file bytes and return the file
            var fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, contentType, filename);
        }
        // PUT: api/CreditApplications/5
        [HttpPut("{id}")]
        public CreditApplication PutCreditApplication(int id, [FromBody] CreditApplication credit)
        {
            if (id != credit.Id)
            {
                return null;
            }

            repository.Update(credit);
            unitOfWork.Save();

            return credit;
        }

        // DELETE: api/CreditApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditApplication(int id)
        {
            var creditApplication = repository.Get(id);
            if (creditApplication == null)
            {
                return NotFound();
            }

            repository.Delete(creditApplication);
            unitOfWork.Save();

            return NoContent();
        }

        // DELETE: api/CreditApplications
        [HttpDelete]
        public async Task<IActionResult> DeleteCreditApplications([FromBody] IList<CreditApplication> creditApplications)
        {
            foreach (var creditApplication in creditApplications)
            {
                repository.Delete(creditApplication);
            }
            unitOfWork.Save();

            return NoContent();
        }


        [HttpPost("SendMail")]
        public async Task SendEmail([FromBody]MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse("samerbelghith2017@gmail.com");
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("samerbelghith2017@gmail.com", "msjhmwbmdmgbfckg");
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }catch (Exception ex)
            {
                throw;
            }

        }
        
    }
}
