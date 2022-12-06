using FlightsCode.DBconfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCode.Models
{
    public class Country
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public bool BelongsToEU { get; set; }
        public ICollection<Company> Companies { get; set; }

        public Country() { }

        public Country(string code, string name, string continent, bool belongsToEU) 
        {
            Code = code;
            Name = name;
            Continent = continent;
            BelongsToEU = belongsToEU;
        }
    }
}
