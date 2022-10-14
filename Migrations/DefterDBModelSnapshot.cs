﻿// <auto-generated />
using System;
using KelimeDefteri.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KelimeDefteri.Migrations
{
    [DbContext(typeof(DefterDB))]
    partial class DefterDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KelimeDefteri.Models.Definition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("WordID")
                        .HasColumnType("bigint");

                    b.Property<string>("definition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("definitionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("WordID");

                    b.ToTable("Definitions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            WordID = 1L,
                            definition = "Anlamlı ses veya ses birliği, söz, sözcük, lügat",
                            definitionType = "isim"
                        },
                        new
                        {
                            Id = 2L,
                            WordID = 2L,
                            definition = "Söz, konuşma",
                            definitionType = "isim"
                        },
                        new
                        {
                            Id = 3L,
                            WordID = 2L,
                            definition = "Söylev",
                            definitionType = "isim"
                        },
                        new
                        {
                            Id = 4L,
                            WordID = 3L,
                            definition = "Yiğitlik, kahramanlık, cesaret",
                            definitionType = "isim, eskimiş"
                        },
                        new
                        {
                            Id = 5L,
                            WordID = 3L,
                            definition = "Dinleyenleri etkilemek veya heyecanlandırmak amacıyla yapılan abartılı anlatım",
                            definitionType = "isim, eskimiş"
                        },
                        new
                        {
                            Id = 6L,
                            WordID = 4L,
                            definition = "Çok konuşan, herkese laf yetiştiren kimse, dil ebesi, söz ebesi",
                            definitionType = "isim, mecaz"
                        });
                });

            modelBuilder.Entity("KelimeDefteri.Models.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Records");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            date = new DateTime(2022, 10, 14, 12, 6, 59, 465, DateTimeKind.Local).AddTicks(8443)
                        });
                });

            modelBuilder.Entity("KelimeDefteri.Models.Word", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.ToTable("Words");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Kelime",
                            RecordId = 1
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Nutuk",
                            RecordId = 1
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Hamaset",
                            RecordId = 1
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Lafebesi",
                            RecordId = 1
                        });
                });

            modelBuilder.Entity("KelimeDefteri.Models.Definition", b =>
                {
                    b.HasOne("KelimeDefteri.Models.Word", "Word")
                        .WithMany("Definitions")
                        .HasForeignKey("WordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("KelimeDefteri.Models.Word", b =>
                {
                    b.HasOne("KelimeDefteri.Models.Record", "Record")
                        .WithMany("Words")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Record");
                });

            modelBuilder.Entity("KelimeDefteri.Models.Record", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("KelimeDefteri.Models.Word", b =>
                {
                    b.Navigation("Definitions");
                });
#pragma warning restore 612, 618
        }
    }
}
