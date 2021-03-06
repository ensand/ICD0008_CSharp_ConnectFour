﻿using GameEngine;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext // Microsoft.EntityFrameworkCore
    {
        public DbSet<Game> Games { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:/Users/Enola/RiderProjects/ConnectFour/DAL/connectFourDb.db");
        }
    }
}