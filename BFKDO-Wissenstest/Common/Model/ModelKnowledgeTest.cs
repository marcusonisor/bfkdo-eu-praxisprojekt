using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    ///     Model für Wissenstest.
    /// </summary>
    public class ModelKnowledgeTest
    {
        /// <summary>
        ///     Id des Wissenstest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Testjahr des Wissenstest.
        /// </summary>
        public string Designation { get; set; } = string.Empty;
    }
}
