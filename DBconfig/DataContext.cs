using FlightsCode.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Diagnostics;


namespace FlightsCode.DBconfig
{
    public class DataContext:DbContext
    {

        public DataContext() : base(new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder()
            {
                DataSource = "MySqliteDB.db",
                ForeignKeys = true
            }.ConnectionString
        }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);


            //Releations, fluentAPI...
            modelBuilder.Entity<Country>()
                           .HasMany(c => c.Companies);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Aircrafts)
                .WithRequired(c => c.Company)
                .HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<Model>()
                .HasMany(m => m.Aircrafts)
                .WithRequired(m => m.Model)
                .HasForeignKey(m => m.ModelId);

            modelBuilder.Entity<Aircraft>()
                .HasRequired(c => c.Company)
                .WithMany(m => m.Aircrafts)
                .HasForeignKey(m => m.CompanyId);

            modelBuilder.Entity<Aircraft>()
                .HasRequired(c => c.Model)
                .WithMany(m => m.Aircrafts)
                .HasForeignKey(m => m.ModelId);

        }
        //one dataset prop for each table:
        //public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }

        public List<Aircraft> GetBelongsToEU()
        {
            List<Aircraft> EUAircrafts = new List<Aircraft>();
            DataContext ctx = new DataContext();

            foreach (Aircraft Aircraft in Aircrafts)
                {
                    if (Aircraft.Company.Country.BelongsToEU)
                    {
                    EUAircrafts.Add(Aircraft);

                    Console.WriteLine(Aircraft.Model.Number + " / "
                            + Aircraft.TailNumber + " / "
                            + Aircraft.Company.Name + " / "
                            + Aircraft.Company.Country.Name);
                    }
                }
            return EUAircrafts;
        }
        public List<Aircraft> GetNonEUAircrafts()
        {
            List<Aircraft> NonEUAircrafts = new List<Aircraft>();
            DataContext ctx = new DataContext();

            foreach (Aircraft Aircraft in Aircrafts)
            {
                if (!Aircraft.Company.Country.BelongsToEU)
                {
                    NonEUAircrafts.Add(Aircraft);

                    Console.WriteLine(Aircraft.Model.Number + " / "
                            + Aircraft.TailNumber + " / "
                            + Aircraft.Company.Name + " / "
                            + Aircraft.Company.Country.Name);
                }
            }
            return NonEUAircrafts;
        }
    }
}
