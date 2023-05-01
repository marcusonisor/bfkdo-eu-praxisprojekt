namespace Common.Model
{
    /// <summary>
    /// Enität eines Testteilnehmers.
    /// </summary>
    public class ModelParticipant
    {
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