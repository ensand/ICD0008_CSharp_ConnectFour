﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("Domain.GameState", b =>
                {
                    b.Property<int>("GameStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Board")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PlayerOneMove")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SaveGameId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameStateId");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("Domain.SaveGame", b =>
                {
                    b.Property<int>("SaveGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Board")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PlayerOneMove")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SaveCreationDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("SaveGameName")
                        .HasColumnType("TEXT");

                    b.HasKey("SaveGameId");

                    b.ToTable("SaveGames");
                });
#pragma warning restore 612, 618
        }
    }
}
