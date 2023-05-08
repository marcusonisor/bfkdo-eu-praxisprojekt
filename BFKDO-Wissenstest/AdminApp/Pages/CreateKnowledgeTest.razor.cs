using Microsoft.AspNetCore.Components;
using AdminApp.Services;

namespace AdminApp.Pages
{
    public partial class CreateKnowledgeTest
    {
        /// <summary>
        ///     Kommunikationsservice.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;
    }
}