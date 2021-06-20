using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gridnine.FlightCodingTest.RuleFiltration;

namespace Gridnine.FlightCodingTest
{
    public static class FlightsFiltration
    {
        /// <summary>
        /// Filters flights acording to a given rule
        /// </summary>
        /// <param name="flights"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static IList<Flight> FindFlights(IList<Flight> flights, Func<Flight, bool> rule)
        {
            return flights.Where(flight => rule(flight)).ToList();
        }

        public static void ProcessFiltration(IList<Flight> flights)
        {
            var Rules = RulesFilter.Rules; // get available rules
            IList<Flight> filteredFlights = flights; // get list of flights
            while (true)
            {
                Console.WriteLine("What kind of filtration rule do you wanna use?");
                //Display list filtering rules
                for (int i = 0; i < Rules.Count; i++)
                {
                    Console.WriteLine($"{i+1} - {Rules[i].NameRule}");
                }
                var classRule = Rules[Convert.ToInt32(Console.ReadLine()) - 1]; // choose a rule

                classRule.EnterParameters(); // indicate parameters
                filteredFlights = FindFlights(filteredFlights, classRule.Filtration); // get found flights

                PrintFlights(filteredFlights); // display found flights

                Console.WriteLine("Continue filtering found flights (1), cancel all filters (2) or exit(3)");
                switch (Console.ReadLine())
                {
                    case "1":
                        break;
                    case "2":
                        filteredFlights = flights;
                        break;
                    case "3":
                        return;
                    default:
                        return;
                }
            }
        }

        /// <summary>
        /// Display a list of found flights
        /// </summary>
        /// <param name="flights"></param>
        public static void PrintFlights(IList<Flight> flights)
        {
            foreach (var flight in flights)
            {
                foreach (var segment in flight.Segments)
                {
                    Console.Write($"{segment.DepartureDate}, {segment.ArrivalDate} ");
                }
                Console.WriteLine();
            }
        }
    }
}
