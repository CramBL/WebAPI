using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using static BCrypt.Net.BCrypt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const int BcryptWorkfactor = 10;

        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public AccountController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // POST: api/ManageUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(UserDto regUser)
        {
            regUser.Email = regUser.Email.ToLower();
            var emailExist = await _context.Users.Where(u =>
            u.Email == regUser.Email).FirstOrDefaultAsync();
            if (emailExist != null)
                return BadRequest(new { errorMessage = "Email already in use" });
            User user = new User()
            {
                Email = regUser.Email,
                FirstName = regUser.FirstName,
                LastName = regUser.LastName
            };
            user.PwHash = HashPassword(regUser.Password, BcryptWorkfactor);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = user.UserId }, regUser);
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto();
            userDto.Email = user.Email;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            return userDto;
        }
        
        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Login(UserDto login)
        {
            login.Email = login.Email.ToLower();
            var user = await _context.Users.Where(u => u.Email == login.Email)
            .FirstOrDefaultAsync();
            if (user != null)
            {
                var validPwd = Verify(login.Password, user.PwHash);
                if (validPwd)
                {
                    var token = new TokenDto();
                    token.JWT = GenerateToken(user);
                    return token;
                }
            }
            ModelState.AddModelError(string.Empty, "Forkert brugernavn eller password");
            return BadRequest(ModelState);
        }
        private string GenerateToken(User user)
        {
            var claims = new Claim[]
            {
                 new Claim("Email", user.Email),
                 new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                 new Claim("UserId", user.UserId.ToString()),
                 new Claim(JwtRegisteredClaimNames.Exp,
                 new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)),
            new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

                //[HttpPost("login"), AllowAnonymous]
        //public async Task<ActionResult<UserDto>> Login(UserDto login)
        //{
        //    login.Email = login.Email.ToLower();
        //    var user = await _context.Users.Where(u =>
        //    u.Email == login.Email).FirstOrDefaultAsync();
        //    if (user != null)
        //    {
        //        var validPwd = Verify(login.Password, user.PwHash);
        //        if (validPwd)
        //        {
        //            return login;
        //        }
        //    }
        //    ModelState.AddModelError(string.Empty, "Forkert brugernavn eller password");
        //    return BadRequest(ModelState);
        //}

        //[HttpPost("login"), AllowAnonymous]
    }

}

