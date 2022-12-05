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
        public int AircraftId { get; set; }
        public int Identifier { get; private set; }
        [ForeignKey("Model")]
        public int ModelTblId { get; set; }
        
        [ForeignKey("Company")]
        public int CompanyTblId { get; set; }
     

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
            ModelTblId = modelId;
            CompanyTblId = companyId;
            TailNumber = tailNumber;
        }
    }
}
