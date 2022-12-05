using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCode.Models
{
    public class Company
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public int Identifier { get; set; }
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryTblId { get; set; }

        public ICollection<Aircraft> Aircrafts { get; set; }
        public Country Country { get; set; }

        public Company()
        {

        }
        public Company(int identifier, string name, int countryId)
        {
            //SqNr = seqNr;
            Identifier = identifier;
            Name = name;
            CountryTblId = countryId;
        }
    }
}
