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
    public class Model
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public ICollection<Aircraft> Aircrafts { get; set; }

        public Model()
        {

        }

        public Model(string number, string description)
        {
            Number = number;
            Description = description;
        }
    }
}
