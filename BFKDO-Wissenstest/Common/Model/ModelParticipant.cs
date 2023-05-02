namespace Common.Model
{
    /// <summary>
    /// DTO eines Testteilnehmers.
    /// </summary>
    public class ModelParticipant
    {
        /// <summary>
        /// Konstruktor des DTOs.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="fireDepartmentBranch"></param>
        public ModelParticipant(string password, string firstName, string lastName, string fireDepartmentBranch)
        {
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            FireDepartmentBranch = fireDepartmentBranch;
        }

        /// <summary>
        /// Das Passwort.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Der Vorname.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Der Nachname.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Die Dienststelle.
        /// </summary>
        public string FireDepartmentBranch { get; }

        // TODO: List of ModelTableRegistration
    }
}