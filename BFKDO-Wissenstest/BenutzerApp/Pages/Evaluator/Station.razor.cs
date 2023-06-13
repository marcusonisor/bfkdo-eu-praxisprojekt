﻿using BenutzerApp.Services;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BenutzerApp.Pages.Evaluator
{
    /// <summary>
    /// Partielle Klasse der Stationsseite.
    /// </summary>
    public partial class Station
    {
        /// <summary>
        /// ID der Station.
        /// </summary>
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

        /// <summary>
        /// Dialogservice.
        /// </summary>
        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        /// <summary>
        ///     Boolean für gestreifte Liste
        /// </summary>
        public bool Striped { get; set; } = true;

        /// <summary>
        /// Der Datensatz fpr die Seite.
        /// </summary>
        public List<ModelEvaluationSet> _data = new();

        /// <summary>
        /// Der Name der Station.
        /// </summary>
        public string _stationName = string.Empty;

        /// <summary>
        /// Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Benotet die ausgewählte Evaluierung.
        /// </summary>
        /// <param name="gradeId"></param>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        public async Task Grade(int gradeId, EnumEvaluation evaluation)
        {
            var response = await EvaluatorService.SubmitEvaluation(new ModelEvaluation(gradeId, evaluation));
            await OnInitializedAsync();
        }

        /// <summary>
        /// Schließt die Evaluierungen der ausgwählten Station ab.
        /// </summary>
        /// <returns></returns>
        public async Task CloseStation()
        {
            var dialog = await DialogService.ShowAsync<CloseStationDialog>();
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var ids = new List<int>();

                foreach (var data in _data)
                {
                    ids.Add(data.GradeId);
                }

                await EvaluatorService.CloseEvaluation(ids);
            }

            await OnInitializedAsync();
        }
    }
}
