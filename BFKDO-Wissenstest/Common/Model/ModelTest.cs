namespace Common.Model
{
    public class ModelTest
    {
        public ModelTest(string designation, string evaluatorPassword)
        {
            Designation = designation;
            EvaluatorPassword = evaluatorPassword;
        }

        public string Designation { get; }

        public string EvaluatorPassword { get; }

        // TODO: List of ModelTableRegistration
    }
}