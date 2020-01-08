﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("GameEngine.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("BoardHeight")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BoardString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BoardWidth")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PlayerOneMove")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SaveCreationDateTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SaveName")
                        .HasColumnType("TEXT");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
