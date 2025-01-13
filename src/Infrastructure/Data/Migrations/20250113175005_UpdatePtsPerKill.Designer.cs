﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QLStats.Infrastructure.Data;

#nullable disable

namespace QLStats.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250113175005_UpdatePtsPerKill")]
    partial class UpdatePtsPerKill
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("QLSTATS")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QLStats.Domain.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aborted")
                        .HasColumnType("boolean");

                    b.Property<int>("CaptureLimit")
                        .HasColumnType("integer");

                    b.Property<string>("ExitMsg")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Factory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FactoryTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstScorer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FragLimit")
                        .HasColumnType("integer");

                    b.Property<int>("GameLength")
                        .HasColumnType("integer");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Infected")
                        .HasColumnType("boolean");

                    b.Property<bool>("Instagib")
                        .HasColumnType("boolean");

                    b.Property<int>("LastLeadChangeTime")
                        .HasColumnType("integer");

                    b.Property<string>("LastScorer")
                        .HasColumnType("text");

                    b.Property<string>("LastTeamscorer")
                        .HasColumnType("text");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MatchGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("MercyLimit")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PlayedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Quadhog")
                        .HasColumnType("boolean");

                    b.Property<bool>("Restarted")
                        .HasColumnType("boolean");

                    b.Property<int>("RoundLimit")
                        .HasColumnType("integer");

                    b.Property<int>("ScoreLimit")
                        .HasColumnType("integer");

                    b.Property<string>("ServerTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TeamBlueScore")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamRedScore")
                        .HasColumnType("integer");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("integer");

                    b.Property<bool>("Training")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Matches", "QLSTATS");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.MatchPlayerStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DamageDealt")
                        .HasColumnType("integer");

                    b.Property<int>("DamageTaken")
                        .HasColumnType("integer");

                    b.Property<int>("Kills")
                        .HasColumnType("integer");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.Property<int>("MedalsTotal")
                        .HasColumnType("integer");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<int>("Suicides")
                        .HasColumnType("integer");

                    b.Property<int?>("Team")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamScore")
                        .HasColumnType("integer");

                    b.Property<bool>("Win")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchPlayerStats", "QLSTATS");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Players", "QLSTATS");
                });

            modelBuilder.Entity("QLStats.Domain.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("DamageForOnePts")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("EndsAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PtsForClanArenaMatchWin")
                        .HasColumnType("integer");

                    b.Property<int>("PtsForClanArenaRoundWin")
                        .HasColumnType("integer");

                    b.Property<decimal>("PtsPerKill")
                        .HasColumnType("numeric");

                    b.Property<int>("PtsPerMedal")
                        .HasColumnType("integer");

                    b.Property<int>("PtsPerSuicide")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("UseGameScore")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Seasons", "QLSTATS");
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
