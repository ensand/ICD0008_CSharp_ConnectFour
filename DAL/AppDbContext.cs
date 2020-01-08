using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext // Microsoft.EntityFrameworkCore
    {
        public DbSet<SaveGame> SaveGames { get; set; }
        public DbSet<GameState> GameStates { get; set; }
        
        public AppDbContext() { }
        public AppDbContext(DbContextOptions options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:/Users/Enola/RiderProjects/ConnectFour/ConsoleApplication/bin/Debug/netcoreapp3.0/saves/connectFourDb.db");
        }
    }
}