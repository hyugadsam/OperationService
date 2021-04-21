using BdConectionCore.BDModels;
using Microsoft.EntityFrameworkCore;
using System;


namespace BdConectionCore.BDConection
{
    public class OperationsContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<TeamLogs> TeamLogs { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<GetTeamsUsersResult> SpGetTeamsUsers { get; set; }

        private string _conection;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetTeamsUsersResult>().HasNoKey();
            //modelBuilder.Entity<Users>().Property(r => r.Password).IsRequired(false).ValueGeneratedNever();
        }

        public OperationsContext(string con)
        {
            _conection = con;
        }

    }
}
