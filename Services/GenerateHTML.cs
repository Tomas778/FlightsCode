using Aspose.Html;
using FlightsCode.DBconfig;
using FlightsCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCode.Services
{
    public class GenerateHTML
    {
        public void GenerateHTMLraport(List<Aircraft> printListAircrafts, string uploadDirectory)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("<body>");
            html.AppendLine("<table>");
            html.AppendLine("<style> table, th, td {border: 1px solid black;}" +
                                           "th, td {padding: 5px;} </style>");

            html.AppendLine("<tr><th>Model</th><th>TailNumber</th><th>Company</th><th>Country</th></tr>"); // Create table headers

            for (int row = 0; row < printListAircrafts.Count; row++) //Create tables row for every list item
            {
                string trTag = "<tr "; // building tr tag for coloring depends on country EU / NonEU

                if (printListAircrafts[row].Company.Country.BelongsToEU) //Create tables column string
                {
                    trTag += "bgcolor='lightgreen'"; // IF EU country color Green
                }
                else
                {
                    trTag += "bgcolor='lightblue'"; // IF Non EU country color Blue
                }
                trTag += ">";

                html.AppendLine(trTag);

                html.AppendLine($"<td> {printListAircrafts[row].Model.Number} </td>");
                html.AppendLine($"<td> {printListAircrafts[row].TailNumber} </td>");
                html.AppendLine($"<td> {printListAircrafts[row].Company.Name} </td>");
                html.AppendLine($"<td> {printListAircrafts[row].Company.Country.Name} </td>");

                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(uploadDirectory, html.ToString()); // Change file's path!!!
        }
    }
}
