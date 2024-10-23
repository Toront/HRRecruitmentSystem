﻿// <auto-generated />
using HRRecruitmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRRecruitmentSystem.Migrations
{
    [DbContext(typeof(RecruitmentDbContext))]
    [Migration("20241023225634_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("CandidateVacancy", b =>
                {
                    b.Property<int>("CandidatesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VacanciesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CandidatesId", "VacanciesId");

                    b.HasIndex("VacanciesId");

                    b.ToTable("CandidateVacancy");
                });

            modelBuilder.Entity("HRRecruitmentSystem.Models.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsHired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsTestCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("HRRecruitmentSystem.Models.HR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HrSpecialists");
                });

            modelBuilder.Entity("HRRecruitmentSystem.Models.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("CandidateVacancy", b =>
                {
                    b.HasOne("HRRecruitmentSystem.Models.Candidate", null)
                        .WithMany()
                        .HasForeignKey("CandidatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRRecruitmentSystem.Models.Vacancy", null)
                        .WithMany()
                        .HasForeignKey("VacanciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
