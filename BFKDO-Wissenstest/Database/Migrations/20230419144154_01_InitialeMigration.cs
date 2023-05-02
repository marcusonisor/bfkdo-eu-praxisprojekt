using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class _01_InitialeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Administrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_KnowledgeLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_KnowledgeLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_KnowledgeSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_KnowledgeSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_KnowledgeTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluatorPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_KnowledgeTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Testperson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FireDepartmentBranch = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Testperson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_EvaluationCriteria",
                columns: table => new
                {
                    Key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnowledgeLevelId = table.Column<int>(type: "int", nullable: false),
                    KnowledgeSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EvaluationCriteria", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Tbl_EvaluationCriteria_Tbl_KnowledgeLevel_KnowledgeLevelId",
                        column: x => x.KnowledgeLevelId,
                        principalTable: "Tbl_KnowledgeLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_EvaluationCriteria_Tbl_KnowledgeSection_KnowledgeSectionId",
                        column: x => x.KnowledgeSectionId,
                        principalTable: "Tbl_KnowledgeSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Registration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnowledgeTestId = table.Column<int>(type: "int", nullable: false),
                    TestpersonId = table.Column<int>(type: "int", nullable: false),
                    KnowledgeLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Registration_Tbl_KnowledgeLevel_KnowledgeLevelId",
                        column: x => x.KnowledgeLevelId,
                        principalTable: "Tbl_KnowledgeLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Registration_Tbl_KnowledgeTest_KnowledgeTestId",
                        column: x => x.KnowledgeTestId,
                        principalTable: "Tbl_KnowledgeTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Registration_Tbl_Testperson_TestpersonId",
                        column: x => x.TestpersonId,
                        principalTable: "Tbl_Testperson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Evaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    EvaluationCriteriaId = table.Column<int>(type: "int", nullable: true),
                    Evaluation = table.Column<int>(type: "int", nullable: false),
                    EvaluationState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Evaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Evaluation_Tbl_EvaluationCriteria_EvaluationCriteriaId",
                        column: x => x.EvaluationCriteriaId,
                        principalTable: "Tbl_EvaluationCriteria",
                        principalColumn: "Key");
                    table.ForeignKey(
                        name: "FK_Tbl_Evaluation_Tbl_Registration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Tbl_Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Evaluation_EvaluationCriteriaId",
                table: "Tbl_Evaluation",
                column: "EvaluationCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Evaluation_RegistrationId",
                table: "Tbl_Evaluation",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_EvaluationCriteria_KnowledgeLevelId",
                table: "Tbl_EvaluationCriteria",
                column: "KnowledgeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_EvaluationCriteria_KnowledgeSectionId",
                table: "Tbl_EvaluationCriteria",
                column: "KnowledgeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Registration_KnowledgeLevelId",
                table: "Tbl_Registration",
                column: "KnowledgeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Registration_KnowledgeTestId",
                table: "Tbl_Registration",
                column: "KnowledgeTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Registration_TestpersonId",
                table: "Tbl_Registration",
                column: "TestpersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Administrator");

            migrationBuilder.DropTable(
                name: "Tbl_Evaluation");

            migrationBuilder.DropTable(
                name: "Tbl_EvaluationCriteria");

            migrationBuilder.DropTable(
                name: "Tbl_Registration");

            migrationBuilder.DropTable(
                name: "Tbl_KnowledgeSection");

            migrationBuilder.DropTable(
                name: "Tbl_KnowledgeLevel");

            migrationBuilder.DropTable(
                name: "Tbl_KnowledgeTest");

            migrationBuilder.DropTable(
                name: "Tbl_Testperson");
        }
    }
}
