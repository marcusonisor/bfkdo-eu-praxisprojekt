using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spire.Barcode;
using WebAPI.Identity;
using System.Drawing.Imaging;
using System.Drawing;
using Database.Tables;
using Database;
using Microsoft.EntityFrameworkCore;

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
        /// Erstellung eines neuen Tests.
        /// </summary>
        /// <returns>Ob die Erstellung des neuen Tests erfolgreich war.</returns>
        /// <response code="200">Erstellung war erfolgreich.</response>
        /// <response code="400">Erstellter Test ist ungültig.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpPost]
        [Route("api/admin/createtest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CreateTest(ModelTest dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Bearbeitung der Daten eines Testteilnehmers.
        /// </summary>
        /// <param name="dto">Die neuen Daten des Testteilnehmers</param>
        /// <returns>Ob die Abgaben erfolgreich war.</returns>
        /// <response code="200">Bearbeitung erfolgreich.</response>
        /// <response code="400">Übermittelten Daten sind ungültig.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpPut]
        [Route("api/admin/updateparticipant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult SubmitEvaluation(ModelParticipant dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export-PDF der Testdaten.
        /// </summary>
        /// <returns>Die Export-PDF.</returns>
        /// <response code="200">Export erfolgreich.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/admin/exportdata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult ExportData()
        {
            throw new NotImplementedException();
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
                BarcodeSettings settings = new BarcodeSettings();
                settings.Data2D = sybosId+"\n"+testPersonPassword;
                settings.Data = settings.Data2D = sybosId+"\n"+testPersonPassword;
                settings.Type = BarCodeType.QRCode;
                settings.ShowText=false;
                settings.QRCodeDataMode = QRCodeDataMode.Auto;
                settings.X = 1.0f;
                settings.QRCodeECL = QRCodeECL.H;

                BarCodeGenerator generator = new BarCodeGenerator(settings);
                Image image = generator.GenerateImage();

                return Task.FromResult<ActionResult>(Ok(new QRCodeModel()
                {
                    SybosId = sybosId,
                    QRCodeBytes=ConvertImageToBase64(image)

                }));
            }
            else
            {
                return Task.FromResult<ActionResult>(BadRequest("The specified SybosID does not exist or it does not have a password!"));
            }

        }

        /// <summary>
        ///     Konvertierung des QR-Code in Base64-String.
        /// </summary>
        /// <param name="image">Bild</param>
        /// <returns>QR-Code als Base64-String</returns>
        private string ConvertImageToBase64(Image image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Png);
            byte[] imageBytes = memoryStream.ToArray();

            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
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