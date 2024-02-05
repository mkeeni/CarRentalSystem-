using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Exceptions
{
    public class LeaseNotFoundException: ApplicationException
    {
        LeaseNotFoundException() { }

        public LeaseNotFoundException(string message) : base(message) { }
    }
}
