using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppConfiguration1
{
    public class DeveloperSettings
    {
        public DeveloperSettings()
        {
            Address = new Address();
        }

        public string Name { get; set; }
        public string Domain { get; set; }
        public string Location { get; set; }
        public Address Address { get; set; }

    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}
