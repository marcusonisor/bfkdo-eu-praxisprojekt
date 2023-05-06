namespace Common.Enums
{
    /// <summary>
    ///     HTTP Request Enum.
    /// </summary>
    public enum EnumHttpRequest
    {
        /// <summary>
        ///     Erfolgreich.
        /// </summary>
        Success,
        /// <summary>
        ///     Nicht zugelassen.
        /// </summary>
        Unauthorized,
        /// <summary>
        ///     Authentifiziert aber nicht richtige Rolle.
        /// </summary>
        Forbidden,
        /// <summary>
        ///     Unbekannter Error.
        /// </summary>
        UnknownError
    }
}