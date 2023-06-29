﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using R6T.Model.Context.R6Tracker;

#nullable disable

namespace R6T.Model.Migrations
{
    [DbContext(typeof(R6TrackerEntities))]
    [Migration("20230629225319_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("R6T.Model.Context.R6Tracker.GameStat", b =>
                {
                    b.Property<Guid>("GameStatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BlindKills")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Deaths")
                        .HasColumnType("int");

                    b.Property<string>("HeadshotPercent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Headshots")
                        .HasColumnType("int");

                    b.Property<decimal?>("KD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("KillPerMatch")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("KillPerMin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Kills")
                        .HasColumnType("int");

                    b.Property<int?>("Losses")
                        .HasColumnType("int");

                    b.Property<int?>("MMR")
                        .HasColumnType("int");

                    b.Property<int?>("MatchTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("MatchesPlayed")
                        .HasColumnType("int");

                    b.Property<int?>("MeleeKills")
                        .HasColumnType("int");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PlayerLevel")
                        .HasColumnType("int");

                    b.Property<string>("RankUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimePlayed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalXp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinPercent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Wins")
                        .HasColumnType("int");

                    b.HasKey("GameStatId");

                    b.HasIndex("MatchTypeId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameStats");
                });

            modelBuilder.Entity("R6T.Model.Context.R6Tracker.MatchType", b =>
                {
                    b.Property<int>("MatchTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchTypeId"));

                    b.Property<string>("MatchTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MatchTypeId");

                    b.ToTable("MatchTypes");
                });

            modelBuilder.Entity("R6T.Model.Context.R6Tracker.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LatestAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RankUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("R6T.Model.Context.R6Tracker.GameStat", b =>
                {
                    b.HasOne("R6T.Model.Context.R6Tracker.MatchType", "MatchType")
                        .WithMany("GameStats")
                        .HasForeignKey("MatchTypeId");

                    b.HasOne("R6T.Model.Context.R6Tracker.Player", "Player")
                        .WithMany("GameStats")
                        .HasForeignKey("PlayerId");

                    b.Navigation("MatchType");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("R6T.Model.Context.R6Tracker.MatchType", b =>
                {
                    b.Navigation("GameStats");
                });

            modelBuilder.Entity("R6T.Model.Context.R6Tracker.Player", b =>
                {
                    b.Navigation("GameStats");
                });
#pragma warning restore 612, 618
        }
    }
}