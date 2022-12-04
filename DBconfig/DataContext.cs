using FlightsCode.Models;
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

            //modelBuilder.Entity<Model>()
            //    .HasMany<Aircraft>(g => g.Aircrafts)
            //    .WithRequired(g => g.Model)
            //    .HasForeignKey(g => g.ModelId);

            modelBuilder.Entity<Company>()
                .HasRequired<Country>(s => s.Country)
                .WithMany(g => g.Companies)
                .HasForeignKey<int>(s => s.CountryId);

            modelBuilder.Entity<Aircraft>()
                .HasRequired<Company>(s => s.Company)
                .WithMany(g => g.Aircrafts)
                .HasForeignKey<int>(s => s.CompanyId);

            modelBuilder.Entity<Aircraft>()
                .HasRequired<Model>(s => s.Model)
                .WithMany(g => g.Aircrafts)
                .HasForeignKey<int>(s => s.ModelId);

            //modelBuilder.Entity<Country>()
            //      .HasMany(c => c.Companies)
            //      .WithOne();

        }
        //one dataset prop for each table:
        //public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
    }
}
