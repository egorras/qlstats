﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLStats.Infrastructure.Data;

#nullable disable

namespace QLStats.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QLStats.Domain.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aborted")
                        .HasColumnType("bit");

                    b.Property<int>("CaptureLimit")
                        .HasColumnType("int");

                    b.Property<string>("ExitMsg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Factory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactoryTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstScorer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FragLimit")
                        .HasColumnType("int");

                    b.Property<int>("GameLength")
                        .HasColumnType("int");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Infected")
                        .HasColumnType("bit");

                    b.Property<bool>("Instagib")
                        .HasColumnType("bit");

                    b.Property<int>("LastLeadChangeTime")
                        .HasColumnType("int");

                    b.Property<string>("LastScorer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastTeamscorer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MatchGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MercyLimit")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlayedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Quadhog")
                        .HasColumnType("bit");

                    b.Property<bool>("Restarted")
                        .HasColumnType("bit");

                    b.Property<int>("RoundLimit")
                        .HasColumnType("int");

                    b.Property<int>("ScoreLimit")
                        .HasColumnType("int");

                    b.Property<string>("ServerTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamBlueScore")
                        .HasColumnType("int");

                    b.Property<int?>("TeamRedScore")
                        .HasColumnType("int");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("int");

                    b.Property<bool>("Training")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.MatchPlayerStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DamageDealt")
                        .HasColumnType("int");

                    b.Property<int>("DamageTaken")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("MedalsTotal")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int?>("Team")
                        .HasColumnType("int");

                    b.Property<int?>("TeamScore")
                        .HasColumnType("int");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchPlayerStats");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("DamageForOnePts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndsAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PtsForClanArenaMatchWin")
                        .HasColumnType("int");

                    b.Property<int>("PtsForClanArenaRoundWin")
                        .HasColumnType("int");

                    b.Property<int>("PtsPerMedal")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("UseGameScore")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.MatchPlayerStats", b =>
                {
                    b.HasOne("QLStats.Domain.Entities.Match", "Match")
                        .WithMany("PlayerStats")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QLStats.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.Match", b =>
                {
                    b.Navigation("PlayerStats");
                });
#pragma warning restore 612, 618
        }
    }
}
