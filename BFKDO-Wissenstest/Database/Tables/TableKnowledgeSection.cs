using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Tables
{
    /// <summary>
    ///     Datenbank-Modell des Wissenskapitel.
    /// </summary>
    [Table("Tbl_KnowledgeSection")]
    public class TableKnowledgeSection
    {
        /// <summary>
        ///     Id des Wissenskapitel.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Beschreibung des Wissenskapitel.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
