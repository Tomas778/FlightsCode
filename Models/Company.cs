using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1
namespace FlightsCode.Models
{
    public class Company
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Aircraft> Aircrafts { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public Company()
        {

        }
        public Company(string name, Country country)
        {
            Name = name;
            Country = country;
        }
    }
}
