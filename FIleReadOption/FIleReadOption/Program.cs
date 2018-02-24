using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIleReadOption
{
    class Program
    {
        static void Main(string[] args)
        {
            int Counter = 1;

            List<flightdata> flightdata = new List<flightdata>();
            foreach (string path in Directory.EnumerateFiles(@"C:\SupriyaCoditilityTextFiles", "*.txt"))
            {
                int startLine = 1;
                string readText = File.ReadAllText(path);

                var fileLines = File.ReadAllLines(path)
                            .Skip((startLine))
                            .ToList();

                if (path.Contains("provider" + Counter + ".txt"))
                {
                    foreach (string line in fileLines)
                    {
                        string[] temp = new string[fileLines.Count];
                        if (line.Contains(','))
                        {
                            temp = line.Split(',');
                        }
                        else if (line.Contains('|'))
                        {
                            temp = line.Split('|');
                        }


                        flightdata ft = new flightdata();
                        ft.origin = temp[0].Trim();
                        ft.Departure_Time = temp[1].Trim();
                        //ft.Departure_Time = Convert.ToDateTime(temp[1].Trim());
                        //ft.Departure_Time = Convert.ToDateTime(temp[1].Trim(), System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
                        ft.Destination = temp[2].Trim();
                        ft.Destination_Time = temp[3].Trim();
                        ft.price = Convert.ToDecimal(temp[4].Trim().Substring(1));
                        flightdata.Add(ft);
                    }
                    Counter++;
                }
            }

            Console.WriteLine(" $searchFlights -origin");
            var originnew = Console.ReadLine();
            Console.WriteLine(" $searchFlights -Destination -d ");
            var Destinationew = Console.ReadLine();
            
            if ((flightdata.Count(x => x.origin == originnew.ToUpper()) == 0) || (flightdata.Count(u => u.Destination == Destinationew.ToUpper()) == 0))
            {
                Console.WriteLine(" No Flights Found for " + originnew + " --> " + Destinationew);
            }
            else
            {
                foreach (var item in flightdata.OrderByDescending(x => x.price).OrderBy(x => x.Departure_Time).Where(x => x.origin == originnew.ToUpper() && x.Destination == Destinationew.ToUpper()).Distinct())
                {
                    Console.WriteLine(item.origin + " -->" + item.Destination + " (" + item.Departure_Time + " -->" + item.Destination_Time + ") " + " -  $" + item.price);
                }
            }

            Console.ReadKey();
        }

    }
    public class flightdata
    {
        public string origin { get; set; }
        public string Departure_Time { get; set; }
        public string Destination { get; set; }
        public string Destination_Time { get; set; }
        public decimal price { get; set; }

    }
}
