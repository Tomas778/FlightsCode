using FlightsCode.DBconfig;
using FlightsCode.Models;
using FlightsCode.PrepeareTables;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;




namespace FlightsCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string dbfile = "URI=file:MySqliteDB.db";
            const string dbfilestring = "MySqliteDB.db";

            //===Create DB if not exists
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), dbfilestring)))
            {
                //Do Nothing
            }
            else
            {
                SQLiteConnection connection = new SQLiteConnection(dbfile);
                connection.Open();
               // string tbl = "create table Person (SqNr integer primary key, Sdsds integer, NAME text);";
               // SQLiteCommand command = new SQLiteCommand(tbl, connection);
                string coutries_tbl = "create table Country (CountryId integer primary key autoincrement, " +
                    "Identifier integer, Code text, Name text, Continent text, BelongsToEU bool);";
                SQLiteCommand command1 = new SQLiteCommand(coutries_tbl, connection);
                string companies_tbl = "create table Company (CompanyId integer primary key autoincrement, " +
                    "Identifier integer, Name text, CountryTblId integer, " +
                    "FOREIGN KEY(CountryTblId) REFERENCES Country(CountryId));";
                SQLiteCommand command2 = new SQLiteCommand(companies_tbl, connection);
                string models_tbl = "create table Model (ModelId integer primary key autoincrement, " +
                    "Identifier integer, Number text, Description text);";
                SQLiteCommand command3 = new SQLiteCommand(models_tbl, connection);
                string aircrafts_tbl = "create table Aircraft (AircraftId integer primary key autoincrement, " +
                    "Identifier integer, " +
                    "ModelTblId integer, " +
                    "CompanyTblId integer, " +
                    "TailNumber text, " +
                    "FOREIGN KEY(ModelTblId) REFERENCES Model(ModelId), " +
                    "FOREIGN KEY(CompanyTblId) REFERENCES Company(CompanyId));";
                SQLiteCommand command4 = new SQLiteCommand(aircrafts_tbl, connection);
               // command.ExecuteNonQuery();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                connection.Close();
            }
            //===End DB


            using (var ctx = new DataContext())
            {

                //var salis = new Country(5, "DE", "Germany", "Europe", true);
                //var firma = ctx.Companies.Find(1);
                //firma.Country.Add(salis);
                //ctx.SaveChanges();

                ctx.Countries.Add(new Country(5, "DE", "Germany", "Europe", true));
                ctx.Countries.Add(new Country(1, "LT", "Lithuania", "Europe", true));
                ctx.Countries.Add(new Country(3, "PL", "Poland", "Europe", true));
                ctx.Countries.Add(new Country(7, "AE", "United Arab Emirates", "Asia", false));
                ctx.Countries.Add(new Country(6, "AM", "Armenia", "Asia", false));
                ctx.SaveChanges();

                //var test = new Company() { Identifier = 10, Name = "Neee Bill" };
                ctx.Companies.Add(new Company(15, "BELAVIA", 1));
                //ctx.Countries.Where(x => x.Code == "LT").FirstOrDefault()
                //   .Companies.Add(test);
              //  ctx.Companies.Add(test);

                // ctx.Companies.Add(new Company(15, "BELAVIA", 1));
                //ctx.Companies.Add(new Company(16, "GLOBAL AIRPARTS", 5));
                //ctx.Companies.Add(new Company(17, "STARWARES.", 3));
                //ctx.Companies.Add(new Company(18, "RANGER AIR", 7));
                //ctx.Companies.Add(new Company(19, "CASP-CO", 6));
                //ctx.Models.Add(new Model(3, "B737-300", "BOEING 737-300"));
                //ctx.Models.Add(new Model(4, "B737-400", "BOEING 737-400"));
                //ctx.Models.Add(new Model(5, "B737-500", "BOEING 737-500"));
                //ctx.Aircrafts.Add(new Aircraft(100, 3, 17, "LY-SPH"));
                //ctx.Aircrafts.Add(new Aircraft(101, 3, 16, "TC-KLA"));
                //ctx.Aircrafts.Add(new Aircraft(102, 4, 15, "EI-FBH"));
                //ctx.Aircrafts.Add(new Aircraft(103, 5, 19, "LY-SPC"));
                //ctx.Aircrafts.Add(new Aircraft(104, 4, 15, "UR-WRK"));
                //ctx.Aircrafts.Add(new Aircraft(105, 3, 18, "VP-BHX"));
                ctx.SaveChanges();
            }

          //  List<Aircraft> test = GetBelongsToEU();
            
            Console.ReadLine();
        }


        //private static void UpdatePerson()
        //{
        //    var ctx = new DataContext();
        //    ctx.People.Find(3).NAME = "John Smith";
        //    ctx.SaveChanges();
        //    ctx.Dispose();
        //}

        //private static void DeletePerson()
        //{
        //    //var ctx = new DataContext();
        //    //var person = ctx.People.Find(4);
        //    //ctx.People.Remove(person);
        //    //ctx.SaveChanges();
        //    //ctx.Dispose();

        //    //Delete all
        //    var ctx = new DataContext();
        //    foreach (var item in ctx.Countries)
        //    {
        //        ctx.Countries.Remove(item);
        //    }
        //    ctx.SaveChanges();
        //    ctx.Dispose();
        //}

        //private static void AddPerson()
        //{
        //    //Person p = new Person() { ID = 10, NAME = "Neee Bill" };
        //    //using (var ctx = new DataContext())
        //    //{
        //    //    ctx.People.Add(p);
        //    //    ctx.SaveChanges();
        //    //}
        //    Country p = new Country() { Id = 5, Code = "LV", Name = "Lithd", Continent = "Europe", BelongsToEU = true };
        //    using (var ctx = new DataContext())
        //    {
        //        ctx.Countries.Add(p);
        //        ctx.SaveChanges();
        //    }
        //}

        public static List<Aircraft> GetBelongsToEU()
        {
            List <Aircraft> EUAircrafts = new List<Aircraft>();
            DataContext ctx = new DataContext();
            try
            {
                foreach (var Aircraft in ctx.Aircrafts)
                {

                    var firma = ctx.Companies
                        .Where(b => b.Identifier == Aircraft.CompanyTblId)
                        .FirstOrDefault();

                    var salis = ctx.Countries
                        .Where(b => b.Identifier == firma.CountryTblId)
                        .FirstOrDefault();

                    var modelis = ctx.Models
                        .Where(b => b.Identifier == Aircraft.ModelTblId)
                        .FirstOrDefault();

                    if (salis.BelongsToEU)

                    {
                        EUAircrafts.Add(Aircraft);

                        Console.WriteLine(modelis.Description + " / " 
                            + Aircraft.TailNumber + " / " 
                            + firma.Name + " / " 
                            + salis.Name);
                    }
                }
                return EUAircrafts;
            }
            catch (Exception ex)
            {
                return null;
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                ctx.Dispose();
            }
        }
    }
}
