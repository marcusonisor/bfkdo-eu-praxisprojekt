namespace WebAPI.Identity
{
    /// <summary>
    /// Statische Klasse die alle JWT Identities enthält.
    /// </summary>
    public static class Identities
    {
        /// <summary>
        /// ClaimName des Admins.
        /// </summary>
        public const string AdminClaimName = "admin";

        /// <summary>
        /// PolicyName des Admins.
        /// </summary>
        public const string AdminPolicyName = "Admin";

        /// <summary>
        /// ClaimName des Testbewerters.
        /// </summary>
        public const string EvaluatorClaimName = "evaluator";

        /// <summary>
        /// PolicyName des Testbewerters.
        /// </summary>
        public const string EvaluatorPolicyName = "Evaluator";

        /// <summary>
        /// ClaimName des Testteilnehmers.
        /// </summary>
        public const string ParticipantClaimName = "participant";

        /// <summary>
        /// PolicyName des Testteilnehmers.
        /// </summary>
        public const string ParticipantPolicyName = "Participant";
    }
}