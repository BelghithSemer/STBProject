using gestionCredits.API.Services;
using gestionCredits.data;
using gestionCredits.entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projet.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace gestionCredits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class AuthController: ControllerBase
    {
        public static usr user = new usr();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        IUserRepo _userRepo;
        protected IUnitOfWork unitOfWork;

        public AuthController(IConfiguration configuration, IUserService userService, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _userService = userService;
            this.unitOfWork = unitOfWork;
            _userRepo = this.unitOfWork.GetUserRepository();

        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<usr>> Register(Login request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }
        [HttpPost("SignUp")]
        public User SignUp([FromBody]SignUpRequest u)
        {
            CreatePasswordHash(u.password, out byte[] passwordHash, out byte[] passwordSalt);
            User us = new User();
            //user.Username = request.Username;
            us.password = u.password;
            us.email = u.email;
            us.adsress = u.adsress;
            us.AccountNumber = u.AccountNumber;
            us.agency = u.agency;
            us.CIN = u.CIN;
            us.Date = u.Date;
            us.Name = u.Name;
            us.LastName = u.LastName;
            us.role = u.role;
            us.PhoneNumber = u.PhoneNumber;
            us.IdRef = "test";

            us.PasswordHash = passwordHash;
            us.PasswordSalt = passwordSalt;
            _userRepo.Add(us);
            unitOfWork.Save();
            return us;
            
        }


        /*[HttpPost("login")]
        public async Task<ActionResult<string>> Login(Login request)
        {
            User user = _userRepo.GetByEmail(request.Email);
            if (user.email == null )
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }*/
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login request)
        {
            User user = _userRepo.GetByEmail(request.Email);
            if (user.email == null )
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(user);
        }
        /*[HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }*/

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "this is my custom Secret key for authentication for authentication"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
