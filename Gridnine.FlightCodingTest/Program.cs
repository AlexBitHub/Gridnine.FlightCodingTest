using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gridnine.FlightCodingTest.RuleFiltration;

namespace Gridnine.FlightCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FlightBuilder flbuild = new FlightBuilder();
            var flights = flbuild.GetFlights();

            Console.WriteLine("1. Вылет до текущего момента времени");
            //Создаем экземпляр класса правила
            RuleDepartureUntilTime firstRule = new RuleDepartureUntilTime();

            // Передаем в статический метод фильтрации список полетов и правило фильтрации
            var firstFlights = FlightsFiltration.FindFlights(flights, firstRule.Filtration);

            //Выводим на экран найденные полеты
            FlightsFiltration.PrintFlights(firstFlights);

            Console.WriteLine("2. Имеются сегменты с датой прилёта раньше даты вылета");
            RuleArrivalBeforeDeparture secondRule = new RuleArrivalBeforeDeparture();
            var secondFlights = FlightsFiltration.FindFlights(flights, secondRule.Filtration);
            FlightsFiltration.PrintFlights(secondFlights);

            Console.WriteLine("3. Общее время, проведённое на земле превышает два часа ");
            RuleOnTheGroundMoreThenTime thirdRule = new RuleOnTheGroundMoreThenTime();
            var thirdFlights = FlightsFiltration.FindFlights(flights, thirdRule.Filtration);
            FlightsFiltration.PrintFlights(thirdFlights);

            //можно продолжить фильтрацию
            FlightsFiltration.ProcessFiltration(flights);

        }
    }
}
