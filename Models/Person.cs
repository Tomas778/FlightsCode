using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightsCode.DBconfig
{
    public class Person
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SqNr { get; set; }
        public int Sdsds { get; set; }
        public string NAME { get; set; }

        public Person()
        {
        }

        public Person(int seqNr, int sdsds, string name)
        {
            SqNr = seqNr;
            Sdsds = sdsds;
            NAME = name;
        }

    }
}

