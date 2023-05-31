﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(BfkdoDbContext))]
    partial class BfkdoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Tables.TableAdministrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Administrator");
                });

            modelBuilder.Entity("Database.Tables.TableEvaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Evaluation")
                        .HasColumnType("int");

                    b.Property<int?>("EvaluationCriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("EvaluationState")
                        .HasColumnType("int");

                    b.Property<int?>("RegistrationId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationCriteriaId");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Tbl_Evaluation");
                });

            modelBuilder.Entity("Database.Tables.TableEvaluationCriteria", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Key"));

                    b.Property<string>("CriteriaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KnowledgeLevelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("KnowledgeSectionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.HasIndex("KnowledgeLevelId");

                    b.HasIndex("KnowledgeSectionId");

                    b.ToTable("Tbl_EvaluationCriteria");
                });

            modelBuilder.Entity("Database.Tables.TableKnowledgeLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tbl_KnowledgeLevel");
                });

            modelBuilder.Entity("Database.Tables.TableKnowledgeSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tbl_KnowledgeSection");
                });

            modelBuilder.Entity("Database.Tables.TableKnowledgeTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EvaluatorPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Designation")
                        .IsUnique();

                    b.ToTable("Tbl_KnowledgeTest");
                });

            modelBuilder.Entity("Database.Tables.TableRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KnowledgeLevelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("KnowledgeTestId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("TestpersonId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KnowledgeLevelId");

                    b.HasIndex("KnowledgeTestId");

                    b.HasIndex("TestpersonId");

                    b.ToTable("Tbl_Registration");
                });

            modelBuilder.Entity("Database.Tables.TableTestperson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FireDepartmentBranch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SybosId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Testperson");
                });

            modelBuilder.Entity("Database.Tables.TableEvaluation", b =>
                {
                    b.HasOne("Database.Tables.TableEvaluationCriteria", "EvaluationCriteria")
                        .WithMany()
                        .HasForeignKey("EvaluationCriteriaId");

                    b.HasOne("Database.Tables.TableRegistration", "Registration")
                        .WithMany("Evaluations")
                        .HasForeignKey("RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EvaluationCriteria");

                    b.Navigation("Registration");
                });

            modelBuilder.Entity("Database.Tables.TableEvaluationCriteria", b =>
                {
                    b.HasOne("Database.Tables.TableKnowledgeLevel", "KnowledgeLevel")
                        .WithMany("EvaluationCriterias")
                        .HasForeignKey("KnowledgeLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Tables.TableKnowledgeSection", "KnowledgeSection")
                        .WithMany()
                        .HasForeignKey("KnowledgeSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KnowledgeLevel");

                    b.Navigation("KnowledgeSection");
                });

            modelBuilder.Entity("Database.Tables.TableRegistration", b =>
                {
                    b.HasOne("Database.Tables.TableKnowledgeLevel", "KnowledgeLevel")
                        .WithMany("Registrations")
                        .HasForeignKey("KnowledgeLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Tables.TableKnowledgeTest", "KnowledgeTest")
                        .WithMany("Registrations")
                        .HasForeignKey("KnowledgeTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Tables.TableTestperson", "Testperson")
                        .WithMany("Registrations")
                        .HasForeignKey("TestpersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KnowledgeLevel");

                    b.Navigation("KnowledgeTest");

                    b.Navigation("Testperson");
                });

            modelBuilder.Entity("Database.Tables.TableKnowledgeLevel", b =>
                {
                    b.Navigation("EvaluationCriterias");

                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("Database.Tables.TableKnowledgeTest", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("Database.Tables.TableRegistration", b =>
                {
                    b.Navigation("Evaluations");
                });

            modelBuilder.Entity("Database.Tables.TableTestperson", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
