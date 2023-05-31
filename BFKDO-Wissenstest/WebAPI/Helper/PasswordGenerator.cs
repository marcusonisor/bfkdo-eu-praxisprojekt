namespace WebAPI.Helper
{
    /// <summary>
    /// Statischer Passwortgenerator.
    /// </summary>
    public static class PasswordGenerator
    {
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly int length = 6;
        private static readonly Random rnd = new();

        /// <summary>
        /// Generiert ein alphanumerisches Passwort.
        /// </summary>
        /// <returns>Ein alphanumerisches Passwort</returns>
        public static string GeneratePassword()
        {
            string password = string.Empty;

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                password += chars[index];
            }

            return password;
        }
    }
}