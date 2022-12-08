using FlightsCode.DBconfig;
using FlightsCode.Models;
using FlightsCode.PrepeareTables;
using FlightsCode.Services;
using System;
using System.Collections;
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
            const string dbfile = "URI=file:MySqliteDB.db"; //Debug folder
            const string dbfilestring = "MySqliteDB.db"; //Predifined DB name

            //===================Create DB and Tables if not exists
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), dbfilestring)))
            {
                //Do Nothing
            }
            else
            {
                SQLiteConnection connection = new SQLiteConnection(dbfile);
                connection.Open(); //Open connection with DB

                //Building SQL query string for New Tables

                string coutries_tbl = @"CREATE TABLE Country (Id integer primary key autoincrement, 
                                        Code text, Name text, Continent text, BelongsToEU bool);";

                SQLiteCommand command1 = new SQLiteCommand(coutries_tbl, connection);

                string companies_tbl = "CREATE TABLE Company (Id integer primary key autoincrement, " +
                                       "Name text, CountryId integer, " +
                                       "FOREIGN KEY(CountryId) REFERENCES Country(Id));";

                SQLiteCommand command2 = new SQLiteCommand(companies_tbl, connection);

                string models_tbl = "CREATE TABLE Model (Id integer primary key autoincrement, " +
                                    "Number text, Description text);";

                SQLiteCommand command3 = new SQLiteCommand(models_tbl, connection);

                string aircrafts_tbl = "CREATE TABLE Aircraft (Id integer primary key autoincrement, " +
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

            //Create test Objects
            //Everything is done by Primary Key and foreign Key relationship in DB

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
            //================Done with New Objects


            List<Aircraft> EUAircrafts = new List<Aircraft>();
            List<Aircraft> NonEUAirfcrafts = new List<Aircraft>();

            //Get lists(EU / NonEU) from DataContext 
            using (var ctx = new DataContext())
            {
                //Get relevant Objects from Tables into Context
                ctx.Companies.ToList();
                ctx.Countries.ToList();
                ctx.Models.ToList();

                //Returns EU Aircrafts
                EUAircrafts = ctx.Aircrafts.Where(x => x.Company.Country.BelongsToEU).ToList();

                //Returns Non EU Aircrafts
                NonEUAirfcrafts = ctx.Aircrafts.Where(x => !x.Company.Country.BelongsToEU).ToList();
            }

            //Use same Aircraft lists from above to send report vie e-mail
            SendEmailReport emailToCustomer = new SendEmailReport();
            emailToCustomer.EmailSend(EUAircrafts); //Send EU Aircrafts
            emailToCustomer.EmailSend(NonEUAirfcrafts); //Send Non EU Aircrafts

            //Use same Aircraft lists from above to generate HTML
            GenerateHTML htmlReport = new GenerateHTML();
            htmlReport.GenerateHTMLraport(EUAircrafts, "C:\\Users\\SYT\\Desktop\\EUoutput.html"); //Generate EU Aircrafts. Change HTML directory
            htmlReport.GenerateHTMLraport(NonEUAirfcrafts, "C:\\Users\\SYT\\Desktop\\NonEUoutput.html"); //Generate Non EUAircrafts. Change HTML directory

            Console.ReadLine(); //Stop
        }
    }
}
