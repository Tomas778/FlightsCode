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
        public int Id { get; private set; }
        public int Identifier { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public bool BelongsToEU { get; set; }
        public ICollection<Company> Companies { get; set; }

        public Country()
        {

        }

        public Country( int identifier, string countryCode, string countryName, string continent, bool belongsToEU)
        {
           // Id = id;
            Identifier = identifier;
            Code = countryCode;
            Name = countryName;
            Continent = continent;
            BelongsToEU = belongsToEU;
        }
    }
}
