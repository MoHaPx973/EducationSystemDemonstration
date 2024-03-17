﻿// <auto-generated />
using System;
using EducationSystem.Adapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EducationSystem.Api.Migrations
{
    [DbContext(typeof(EducationDbContext))]
    [Migration("20231022190103_add_user")]
    partial class add_user
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("EducationSystem.Domain.Models.Assessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Point")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeacherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("EducationSystem.Domain.Models.Curriculum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearFormation")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Curriculums");
                });

            modelBuilder.Entity("EducationSystem.Domain.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("EducationSystem.Domain.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("EducationSystem.Domain.Models.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurriculumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Letter")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearFormation")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("EducationSystem.Domain.Models.SchoolItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EducationSystem.Domain.Relationships.ItemInCurriculum", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurriculumId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId", "CurriculumId");

                    b.ToTable("RelationItemsInCurriculums");
                });

            modelBuilder.Entity("EducationSystem.Domain.Relationships.StudentInClass", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId", "SchoolClassId");

                    b.ToTable("RelationStudentsInClasses");
                });
#pragma warning restore 612, 618
        }
    }
}
