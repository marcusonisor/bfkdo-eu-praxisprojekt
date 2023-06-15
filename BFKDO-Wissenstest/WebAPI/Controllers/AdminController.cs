using Common.Model;
using Microsoft.AspNetCore.Mvc;
using Database.Tables;
using Database;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using WebAPI.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller zuständig für alle Aktionen eines authentifizierten Admins
    /// </summary>
    [Authorize(Policy = Identities.AdminPolicyName)]
    public class AdminController : ControllerBase
    {
        private readonly BfkdoDbContext _databaseContext;
        /// <summary>
        ///     Konstruktor des Admin-Controllers.
        /// </summary>
        /// <param name="databaseContext"></param>
        public AdminController(BfkdoDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// QR-Code Generierung mit den Anmeldedaten der Testpersonen.
        /// </summary>
        /// <returns>Die Export-PDF.</returns>
        /// <response code="200">Export erfolgreich.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/admin/createqrcode/{sybosId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<ActionResult> CreateQrCode(int sybosId)
        {
            string testPersonPassword = GetPasswordAsync(sybosId).Result;

            if (testPersonPassword!="")
            {
                QRCodeData qrCodeData;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    string credentials = sybosId+"\n"+testPersonPassword;
                    qrCodeData = qrGenerator.CreateQrCode(credentials, QRCodeGenerator.ECCLevel.Q);
                }

                // Image Format
                var imgType = Base64QRCode.ImageType.Png;

                var qrCode = new Base64QRCode(qrCodeData);
                //Base64 Format
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);


                return Task.FromResult<ActionResult>(Ok(qrCodeImageAsBase64));
            }
            else
            {
                return Task.FromResult<ActionResult>(BadRequest("The specified SybosID does not exist or it does not have a password!"));
            }

        }
        /// <summary>
        ///     Passwort zu SybosId aus Datenbank holen.
        /// </summary>
        /// <param name="SybosId">SybosId</param>
        /// <returns>Passwort zu SybosId</returns>
        private async Task<string> GetPasswordAsync(int SybosId)
        {
            try
            {
                TableTestperson participant = await _databaseContext.TableTestpersons.Where(entity => entity.SybosId == SybosId).SingleAsync();
                return participant.Password;
            }
            catch
            {
                return "";
            }
        }

    }
}
