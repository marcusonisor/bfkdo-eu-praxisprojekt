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
    ///     Datenbankmodell des Administrators.
    /// </summary>
    [Table("Tbl_Administrator")]
    public class TableAdministrator
    {
        /// <summary>
        ///     Id des Administrators.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Email des Administrators.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     Passwort des Administrators.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        ///     Letzter Login.
        /// </summary>
        public DateTime LastLogin { get; set; }
    }
}
