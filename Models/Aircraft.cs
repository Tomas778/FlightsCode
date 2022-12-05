using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCode.Models
{
    public class Aircraft
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Identifier { get; private set; }
      //  [ForeignKey("Id")]
        public int ModelId { get; set; }
      //  [ForeignKey("Id")]
        public int CompanyId { get; set; }
        public string TailNumber { get; set; }
        public Model Model { get; set; }
        public Company Company { get; set; }

        public Aircraft()
        {

        }

        public Aircraft(int identifier, int modelId, int companyId, string tailNumber)
        {
            //SqNr = seqNr;
            Identifier = identifier;
            ModelId = modelId;
            CompanyId = companyId;
            TailNumber = tailNumber;
        }
    }
}
