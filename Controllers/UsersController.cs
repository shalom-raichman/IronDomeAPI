using IronDomeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IronDomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private string GenerateToken(string userIP)
        {
            // token handler can create token
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretKey = "12345l'trqa ;haiu;bfskjbgiHGOIEQJGPO[GUHE   poiuhghvpiufdh;kjhrp[gouhpajngd;iueh[oiug   ;g  liuhy9=uhgf;b   lkjwnbg;ouhrpy7unrlkgfh;riugf;rikfh;orgfiu678"; //TODO: remove this from code
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            // token descriptor describe HOW to create the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // things to include in the token
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                    new Claim(ClaimTypes.Name, "Shubulu"),
                    }
                ),
                // expiration time of the token
                Expires = DateTime.UtcNow.AddMinutes(1),
                // the secret key of the token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            // creating the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // converting the token to string
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginObject loginObject)
        {
            if (loginObject.UserName == "admin" &&
                loginObject.Password == "123456")
            {

                // getting the user (requester) IP
                string userIP = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                return StatusCode(200
                    , new { token = GenerateToken(userIP) }
                    );
            }
            return StatusCode(StatusCodes.Status401Unauthorized,
                    new { error = "invalid credentials" });
        }
    }
}
