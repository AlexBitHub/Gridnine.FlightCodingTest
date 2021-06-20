using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest.RuleFiltration
{
    /// <summary>
    /// Represents a filtering rule
    /// </summary>
    public interface IRuleFilter
    {
        /// <summary>
        /// Name of rule filtration
        /// </summary>
        string NameRule { get; }

        /// <summary>
        /// Method to change filtering parameters
        /// </summary>
        void EnterParameters();
        
        /// <summary>
        /// Filtration method
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        bool Filtration(Flight flight);
    }
}
