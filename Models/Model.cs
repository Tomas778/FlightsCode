using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCode.Models
{
    public class Model
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }
        public int Identifier { get; private set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public ICollection<Aircraft> Aircrafts { get; set; }

        public Model()
            {

            }

        public Model(int identifier, string number, string description)
        {
           // SqNr = SeqNr;
            Identifier = identifier;
            Number = number;
            Description = description;
        }
    }
}
