namespace Common.Model
{
    /// <summary>
    /// DTO eines Tests.
    /// </summary>
    public class ModelTest
    {
        /// <summary>
        /// Konstruktor des DTOs.
        /// </summary>
        /// <param name="designation"></param>
        /// <param name="evaluatorPassword"></param>
        public ModelTest(string designation, string evaluatorPassword)
        {
            Designation = designation;
            EvaluatorPassword = evaluatorPassword;
        }

        /// <summary>
        /// Die Bezeichnung.
        /// </summary>
        public string Designation { get; }

        /// <summary>
        /// Das Passwort.
        /// </summary>
        public string EvaluatorPassword { get; }

        // TODO: List of ModelTableRegistration
    }
}