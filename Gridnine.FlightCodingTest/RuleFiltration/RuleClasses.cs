using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest.RuleFiltration
{
    /// <summary>
    /// Contains list of rules
    /// </summary>
    public class RulesFilter
    {
        public static List<IRuleFilter> Rules = new List<IRuleFilter>() {
                                                        new RuleDepartureUntilTime(),
                                                        new RuleOnTheGroundMoreThanTime(),
                                                        new RuleArrivalBeforeDeparture()};
    }

    /// <summary>
    /// Rule excludes flights with earlier departure time
    /// </summary>
    public class RuleDepartureUntilTime : IRuleFilter
    {

        public DateTime untilTime;
        public string NameRule => "Departure until time";

        public void EnterParameters()
        {
            Console.Write("Indicate the date before which the departure took place, Enter - current date (format dd-mm-yyyy hh:mm): ");
            var param = Console.ReadLine();
            if (param == "")
                untilTime = DateTime.Now;
            else
            {
                while (!DateTime.TryParse(Console.ReadLine(), out untilTime))
                {
                    Console.WriteLine("Indicate correct date");
                }
            }
        }
        public bool Filtration(Flight flight)
        {
            return flight.Segments[0].DepartureDate >= untilTime;
        }
    }

    /// <summary>
    /// Rule excludes flights with time on the ground more than indicates
    /// </summary>
    public class RuleOnTheGroundMoreThanTime : IRuleFilter
    {
        public double moreThenTime;
        public string NameRule => "Time on the ground";
        public bool Filtration(Flight flight)
        {
            TimeSpan timeOnTheGround = new TimeSpan();
            for (int i = 1; i < flight.Segments.Count; i++)
            {
                timeOnTheGround += flight.Segments[i].DepartureDate - flight.Segments[i - 1].ArrivalDate;
            }
            return TimeSpan.FromHours(moreThenTime) > timeOnTheGround;
        }

        public void EnterParameters()
        {
            Console.Write("How many hours spent on the ground: ");
            while(!double.TryParse(Console.ReadLine(), out moreThenTime))
            {
                Console.WriteLine("Indicate correct time");
            }
        }
    }

    /// <summary>
    /// Rule excludes flights with arrival time before departure time
    /// </summary>
    public class RuleArrivalBeforeDeparture : IRuleFilter
    {
        public string NameRule => "Arrival before departure";
        public bool Filtration(Flight flight)
        {
            return !flight.Segments.Any(x => x.ArrivalDate < x.DepartureDate);
        }

        public void EnterParameters()
        {
            Console.WriteLine("No parameters");
        }
    }
}
