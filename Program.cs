using FlightsCode.DBconfig;
using FlightsCode.Models;
using FlightsCode.PrepeareTables;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;



//1
namespace FlightsCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string dbfile = "URI=file:MySqliteDB.db";
            const string dbfilestring = "MySqliteDB.db";

            //===================Create DB and Tables if not exists
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), dbfilestring)))
            {
                //Do Nothing
            }
            else
            {
                SQLiteConnection connection = new SQLiteConnection(dbfile);
                connection.Open();
                string coutries_tbl = "create table Country (Id integer primary key autoincrement, " +
                    "Code text, Name text, Continent text, BelongsToEU bool);";
                SQLiteCommand command1 = new SQLiteCommand(coutries_tbl, connection);
                string companies_tbl = "create table Company (Id integer primary key autoincrement, " +
                    "Name text, CountryId integer, " +
                    "FOREIGN KEY(CountryId) REFERENCES Country(Id));";
                SQLiteCommand command2 = new SQLiteCommand(companies_tbl, connection);
                string models_tbl = "create table Model (Id integer primary key autoincrement, " +
                    "Number text, Description text);";
                SQLiteCommand command3 = new SQLiteCommand(models_tbl, connection);
                string aircrafts_tbl = "create table Aircraft (Id integer primary key autoincrement, " +
                    "ModelId integer, " +
                    "CompanyId integer, " +
                    "TailNumber text, " +
                    "FOREIGN KEY(ModelId) REFERENCES Model(Id), " +
                    "FOREIGN KEY(CompanyId) REFERENCES Company(Id));";
                SQLiteCommand command4 = new SQLiteCommand(aircrafts_tbl, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                connection.Close();
            }
            //=====================End SQLite DB with Tables


            using (var ctx = new DataContext())
            {
                //Add new Countries Initial Data without ForeignKey
                ctx.Countries.Add(new Country("DE", "Germany", "Europe", true));
                ctx.Countries.Add(new Country("LT", "Lithuania", "Europe", true));
                ctx.Countries.Add(new Country("PL", "Poland", "Europe", true));
                ctx.Countries.Add(new Country("AE", "United Arab Emirates", "Asia", false));
                ctx.Countries.Add(new Country("AM", "Armenia", "Asia", false));

                //Add new Aircraft Models Initial Data without ForeignKey
                ctx.Models.Add(new Model("737-MAX", "Not the safest aircraft on air"));
                ctx.Models.Add(new Model("737", "safest aircraft on air"));
                ctx.Models.Add(new Model("737-Test", "safest aircraft on air"));

                ctx.SaveChanges();         //Save Countries and Models


                //New Companies with ForeignKey from Countries ID

                ctx.Companies.Add(new Company("UAB Skrendam",
                    ctx.Countries.FirstOrDefault(x => x.Code == "LT")));

                ctx.Companies.Add(new Company("UAB Krentam",
                    ctx.Countries.FirstOrDefault(x => x.Code == "LT")));

                ctx.Companies.Add(new Company("Das Flug",
                    ctx.Countries.FirstOrDefault(x => x.Code == "DE")));

                ctx.Companies.Add(new Company("ArmeniaAir",
                    ctx.Countries.FirstOrDefault(x => x.Code == "AM")));

                ctx.Companies.Add(new Company("LOT",
                    ctx.Countries.FirstOrDefault(x => x.Code == "PL")));

                ctx.Companies.Add(new Company("ArabAirlines",
                    ctx.Countries.FirstOrDefault(x => x.Code == "AE")));

                ctx.SaveChanges();         //Save Companies


                //New Aircrafts with ForeignKeys from Companies ID and Models ID

                ctx.Aircrafts.Add(new Aircraft("LT-SPH",
                    ctx.Models.FirstOrDefault(x => x.Number == "737-MAX"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "UAB Skrendam")));

                ctx.Aircrafts.Add(new Aircraft("LT-TGF",
                    ctx.Models.FirstOrDefault(x => x.Number == "737"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "UAB Skrendam")));

                ctx.Aircrafts.Add(new Aircraft("LT-RGR",
                    ctx.Models.FirstOrDefault(x => x.Number == "737-Test"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "UAB Krentam")));

                ctx.Aircrafts.Add(new Aircraft("PL-TWQ",
                    ctx.Models.FirstOrDefault(x => x.Number == "737-Test"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "LOT")));

                ctx.Aircrafts.Add(new Aircraft("DE-SPJ",
                    ctx.Models.FirstOrDefault(x => x.Number == "737-MAX"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "Das Flug")));

                ctx.Aircrafts.Add(new Aircraft("AM-TGF",
                    ctx.Models.FirstOrDefault(x => x.Number == "737"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "ArmeniaAir")));

                ctx.Aircrafts.Add(new Aircraft("AE-TWB",
                    ctx.Models.FirstOrDefault(x => x.Number == "737-Test"),
                    ctx.Companies.FirstOrDefault(x => x.Name == "ArabAirlines")));

                ctx.SaveChanges();          //Save Aircrafts
                ctx.Dispose();
            }


            //Get\Lists lists
            using (var ctx = new DataContext())
            {
                List<Aircraft> EUAircrafts =                                          //Returns EU Aircrafts
                    ctx.Aircrafts.Where(x => x.Company.Country.BelongsToEU).ToList();

                List<Aircraft> NonEUAirfcrafts =                                      //Returns Non EU Aircrafts
                    ctx.Aircrafts.Where(x => !x.Company.Country.BelongsToEU).ToList();

            }

            Console.ReadLine(); //Stop
        }
    }
}
