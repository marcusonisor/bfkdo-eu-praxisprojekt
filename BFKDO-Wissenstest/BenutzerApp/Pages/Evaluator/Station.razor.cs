using BenutzerApp.Services;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Pages.Evaluator
{
    public partial class Station
    {
        [Parameter]
        public int? Id { get; set; }

        /// <summary>
        /// Stationen Service.
        /// </summary>
        [Inject]
        public EvaluatorService EvaluatorService { get; set; } = null!;

        /// <summary>
        ///     Model für die View.
        /// </summary>
        public TestStationModel Stationen { get; set; } = new();

        /// <summary>
        ///     Authentifizierungstatusservice.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        public List<ModelEvaluatorGrade> _data = new();

        public bool _passButtonDisabled = false;
        public bool _failButtonDisbaled = false;

        protected override async Task OnInitializedAsync()
        {
            var knowledgeTestId = AuthenticationStateService.GetContextId();

            if (Id.HasValue)
            {
                var response = await EvaluatorService.GetStationData(knowledgeTestId, Id.Value);
                _data = response.Result;
            }
        }

        public async Task Grade(int gradeId, EnumEvaluation evaluation)
        {
            var response = await EvaluatorService.SubmitEvaluation(new ModelEvaluation(gradeId, evaluation));
            await OnInitializedAsync();
        }
    }
}
