namespace WebAPI.Controllers
{
    using Common.Model;
    using Database;
    using Database.Tables;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using WebAPI.Identity;

    /// <summary>
    /// Controller zuständig für die Authentifizierung aller Benuter.
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BfkdoDbContext _databaseContext;

        /// <summary>
        /// Konstruktor des Controllers.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="databaseContext"></param>
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
        public ActionResult AuthAdmin([FromBody] ModelAdminAuthData dto)
        {
            string specifiedPassword = string.Empty;

            try
            {
                TableAdministrator admin = _databaseContext.TableAdministrators
                    .Where(entity => entity.Email == dto.Email)
                    .Single();

                using (SHA256 sha256 = SHA256.Create())
                {
                    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

                    foreach (byte b in hash)
                        specifiedPassword += b.ToString("x2");
                }

                if (specifiedPassword != admin.Password)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(new { Token = GenerateToken(Identities.AdminClaimName) });
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Login der Testbewerter.
        /// </summary>
        /// <param name="authData">Das Zugangspasswort des Testbewerters.</param>
        /// <returns>Ob der Login eines Testbewerters erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">Zugangspasswort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/evaluator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthEvaluator([FromBody] ModelEvaluatorAuthData authData)
        {
            try
            {
                TableKnowledgeTest test = _databaseContext.TableKnowledgeTests
                    .Where(entity => entity.EvaluatorPassword == authData.Password)
                    .Single();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(new { Token = GenerateToken(Identities.EvaluatorClaimName) });
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
        public ActionResult AuthParticipant([FromBody] ModelParticipantAuthData dto)
        {
            try
            {
                TableTestperson participant = _databaseContext.TableTestpersons
                    .Where(entity => entity.SybosId == dto.SybosId)
                    .Single();

                if (dto.Password != participant.Password)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(new { Token = GenerateToken(Identities.AdminClaimName) });
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        private string GenerateToken(string claimNaime)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[] {
                         new Claim(claimNaime, "true"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString())
                         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]!));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = _configuration["JWTSettings:Issuer"],
                Audience = _configuration["JWTSettings:Audience"],
                SigningCredentials = signIn
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }
    }
}