using Database.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    ///     Datenbank-Kontext für das Projekt.
    /// </summary>
    public partial class BfkdoDbContext : DbContext
    {
        /// <summary>
        ///     Konstruktor der Datenbankanbindung.
        /// </summary>
        /// <param name="options"></param>
        public BfkdoDbContext(DbContextOptions<BfkdoDbContext> options) : base(options)
        {
        }

        /// <summary>
        ///     Administrator-Tabelle.
        /// </summary>
        public virtual DbSet<TableAdministrator> TableAdministrators { get; set; }

        /// <summary>
        ///     Wissenstest-Tabelle.
        /// </summary>
        public virtual DbSet<TableKnowledgeTest> TableKnowledgeTests { get; set; }

        /// <summary>
        ///     Registrierungs-Tabelle.
        /// </summary>
        public virtual DbSet<TableRegistration> TableRegistrations { get; set; }

        /// <summary>
        ///     Testpersonen-Tabelle.
        /// </summary>
        public virtual DbSet<TableTestperson> TableTestpersons { get; set; }

        /// <summary>
        ///     Bewertungs-Tabelle.
        /// </summary>
        public virtual DbSet<TableEvaluation> TableEvaluations { get; set; }

        /// <summary>
        ///     Bewertungskriterien-Tabelle.
        /// </summary>
        public virtual DbSet<TableEvaluationCriteria> TableEvaluationCriterias { get; set; }

        /// <summary>
        ///     Wissenstest-Stufen-Tabelle.
        /// </summary>
        public virtual DbSet<TableKnowledgeLevel> TableKnowledgeLevels { get; set; }

        /// <summary>
        ///     Wissenstest-Kapitel-Tabelle.
        /// </summary>
        public virtual DbSet<TableKnowledgeSection> TableKnowledgeSections { get; set; }


        /// <summary>
        ///     Erstellen der ersten Befüllung der Datenbank.
        /// </summary>
        /// <returns></returns>
        public async Task CreateInitialFillUp()
        {
            if (Database.EnsureDeleted())
            {
                Database.EnsureCreated();
            }

            for (int i = 1; i < 7; i++)
            {
                TableKnowledgeLevels.Add(new TableKnowledgeLevel()
                {
                    Description = $"Stufe {i}"
                });
            }

            foreach (var section in _testSections)
            {
                TableKnowledgeSections.Add(new TableKnowledgeSection()
                {
                    Description = section.Item1
                });
            };

            await SaveChangesAsync();

            foreach (var level in TableKnowledgeLevels.ToList())
            {
                var neededsections = _testSections.Where(t => t.Item2.Select(t => $"Stufe {t}").Contains(level.Description)).ToList();
                foreach (var section in neededsections)
                {
                    var tablesection = TableKnowledgeSections.FirstOrDefault(t => t.Description == section.Item1);
                    if (tablesection != null!)
                    {
                        TableEvaluationCriterias.Add(new()
                        {
                            KnowledgeLevel = level,
                            KnowledgeSection = tablesection
                        });
                    }
                }
            }

            await SaveChangesAsync();
        }
        #region Constants
        private readonly List<(string, int[])> _testSections = new()
        {
            ("Knoten",new[]{1,2,3,4,5,6}),
            ("Organisation und Verhaltensregeln",new[]{1,2,3,4,5}),
            ("Bekleidung, Fahrzeuge und Geräte",new[]{1,2,3,4,5,6}),
            ("Formalexerzieren",new[]{2,3,4}),
            ("Dienstgrade",new[]{2,3,4}),
            ("Unfallverhütung und Erste Hilfe",new[]{4,5,6}),
            ("Atem- und Körperschutz",new[]{5}),
            ("der technische Einsatz",new[]{6}),
            ("die taktische Einheit im Einsatz",new[]{6})
        };
        #endregion
    }
}
