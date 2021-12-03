using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stonks.Db.Models;

namespace Stonks.Db
{
    public class FantasyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public readonly string ConnectionName = "FantasyDb";
        public string ConnectionString { get; private set; }

        public DbSet<FantasyTeam> FantasyTeams { get; set; }
        public DbSet<FantasyTeamMember> FantasyTeamMembers { get; set; }
        public DbSet<InvestReviewSource> InvestReviewSources { get; set; }
        public DbSet<InvestReviewPost> InvestReviewPosts { get; set; }

        public FantasyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString(ConnectionName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<InvestReviewPost>()
                .Property(e => e.Tags)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}