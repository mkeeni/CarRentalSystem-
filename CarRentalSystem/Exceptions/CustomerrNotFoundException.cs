﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Exceptions
{
    public class CustomerNotFoundException: ApplicationException
    {
        CustomerNotFoundException() { }
        public CustomerNotFoundException(string message) : base(message) 
        {
            
        }
    }
}
