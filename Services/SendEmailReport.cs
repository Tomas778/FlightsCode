using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FlightsCode.Models;
using FlightsCode.DBconfig;

namespace FlightsCode.Services
{
    internal class SendEmailReport
    {
        public void EmailSend(List<Aircraft> printListAircrafts)
        {
            String emailString = "";

            //Init E-mail connection
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("f1cffbed3491aa", "8c91d590005e91"),
                EnableSsl = true
            };


            using (var ctx = new DataContext())
            {
                //Generate e-mail text
                foreach (Aircraft Aircraft in printListAircrafts)
                {
                    emailString += $"{Aircraft.Model.Number} / " +
                                   $"{Aircraft.TailNumber} / " +
                                   $"{Aircraft.Company.Name} / " +
                                   $"{Aircraft.Company.Country.Name} \n";
                }
            }

            //Send the string
            client.Send("AircraftsReport@gmail.com", "test@gmail.com", "Incoming airplane report", emailString);
        }
    }
}
