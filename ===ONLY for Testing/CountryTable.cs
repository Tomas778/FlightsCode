using FlightsCode.DBconfig;
using FlightsCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCode.PrepeareTables
{
    public class CountryTable
    {
        private static void UpdateCountry()
        {
            var ctx = new DataContext();
            ctx.Countries.Find(3).Name = "John Smith";
            ctx.SaveChanges();
            ctx.Dispose();
        }

        private static void DeleteCountry()
        {
            var ctx = new DataContext();
            var item = ctx.Countries.Find(4);
            ctx.Countries.Remove(item);
            ctx.SaveChanges();
            ctx.Dispose();
        }

        private static void DeleteAll()
        {
            var ctx = new DataContext();
            foreach (var item in ctx.Countries)
            {
                ctx.Countries.Remove(item);
            }
            ctx.SaveChanges();
            ctx.Dispose();
        }

        public void AddCountry(Country Item)
        {
            using (var ctx = new DataContext())
            {
                ctx.Countries.Add(Item);
                ctx.SaveChanges();
            }
        }
    }
}
