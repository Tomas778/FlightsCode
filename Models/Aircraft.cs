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
    public class Aircraft
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TailNumber { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public Aircraft()
        {

        }

        public Aircraft(string tailNumber, Model model, Company company)
        {
            TailNumber = tailNumber;
            Model = model;
            Company = company;
        }

        public void task2()
        {
            Console.Write("LABAS");
            Console.ReadKey();
        }


    }
}
