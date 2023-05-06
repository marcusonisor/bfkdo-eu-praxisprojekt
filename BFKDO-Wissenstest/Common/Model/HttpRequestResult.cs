using Common.Enums;

namespace Common.Model
{
    /// <summary>
    ///     HTTP Request Result.
    /// </summary>
    /// <typeparam name="T">Typ des Rückgabetypens</typeparam>
    public class HttpRequestResult<T>
    {
        /// <summary>
        ///     Status des Requests.
        /// </summary>
        public EnumHttpRequest RequestEnum { get; set; }

        /// <summary>
        ///     Fehlernachricht bei aufgetretenen Fehler.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        ///     Ergebnis des Requests.
        /// </summary>
        public T Result { get; set; } = default!;
    }
}
