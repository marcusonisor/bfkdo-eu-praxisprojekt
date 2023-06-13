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
        ///     Authentifizierungstatusservice.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        //public TestStationModel Stationen { get; set; } = new();

        public List<ModelEvaluationSet> _data = new();

        public string _stationName = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var knowledgeTestId = AuthenticationStateService.GetContextId();

            if (Id.HasValue)
            {
                var stationName = await EvaluatorService.GetStationName(Id.Value);
                _stationName = stationName.Result.StationName;

                var data = await EvaluatorService.GetStationData(knowledgeTestId, Id.Value);
                _data = data.Result;
            }
        }

        public async Task Grade(int gradeId, EnumEvaluation evaluation)
        {
            var response = await EvaluatorService.SubmitEvaluation(new ModelEvaluation(gradeId, evaluation));
            await OnInitializedAsync();
        }

        public async Task CloseStation()
        {
            var ids = new List<int>();

            foreach (var data in _data)
            {
                ids.Add(data.GradeId);
            }

            await EvaluatorService.CloseEvaluation(ids);
        }
    }
}
