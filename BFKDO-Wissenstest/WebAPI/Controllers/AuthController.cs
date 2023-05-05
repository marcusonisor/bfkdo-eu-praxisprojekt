namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Common.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Database;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using WebAPI.Identity;

    /// <summary>
    /// Controller zuständig für die Authentifizierung aller Benuter.
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BfkdoDbContext _databaseContext;

        public AuthController(IConfiguration config, BfkdoDbContext databaseContext)
        {
            _configuration = config;
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Login des Admins.
        /// </summary>
        /// <param name="dto">Die Logindaten des Admins.</param>
        /// <returns>Ob der Login eines Admins erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">E-Mail oder Passwort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthAdmin(ModelAdminAuthData dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login der Testbewerter.
        /// </summary>
        /// <param name="dto">Das Zugangspasswort des Testbewerters.</param>
        /// <returns>Ob der Login eines Testbewerters erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">Zugangspasswort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/evaluator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthEvaluator([FromBody] ModelEvaluatorAuthData authData)
        {
            string token = GenerateEvaluatorToken();
            return Ok(new { token = token });
        }

        /// <summary>
        /// Login der Testteilnehmer.
        /// </summary>
        /// <param name="dto">Die Logindaten des Testteilnehmers.</param>
        /// <returns>Ob der Login eines Teilnehmers erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">SybosId oder Passwort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/participant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthParticipant(ModelParticipantAuthData dto)
        {
            throw new NotImplementedException();
        }

        private string GenerateEvaluatorToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[] {
                         new Claim(Identities.EvaluatorClaimName, "true"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
                         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    _configuration["Jwt:Issuer"],
            //    _configuration["Jwt:Audience"],
            //    claims,
            //    expires: DateTime.UtcNow.AddHours(1),
            //    signingCredentials: signIn);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = signIn
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }
    }
}