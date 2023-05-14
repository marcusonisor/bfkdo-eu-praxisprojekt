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
    ///     Datenbank-Modell der Testperson.
    /// </summary>
    [Table("Tbl_Testperson")]
    public class TableTestperson
    {
        /// <summary>
        ///     Id der Testperson
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Sybos Id.
        /// </summary>
        public int SybosId { get; set; }

        /// <summary>
        ///     Passwort.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        ///     Vorname.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        ///     Nachname.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        ///     Dienststelle.
        /// </summary>
        public string FireDepartmentBranch { get; set; } = string.Empty;

        /// <summary>
        ///     Registrierungen.
        /// </summary>
        public virtual ICollection<TableRegistration> Registrations { get; set; } = new List<TableRegistration>();
    }
}
