using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DroneServiceApplication
{
    internal class Drone
    {
        // 6.1	Create a separate class file to hold the data items of the Drone. Use separate getter and setter methods, ensure the attributes are private and the accessor methods are public.
        // Add a display method that returns a string for Client Name and Service Cost.
        // Add suitable code to the Client Name and Service Problem accessor methods so the data is formatted as Title case or Sentence case. Save the class as “Drone.cs”.

        private string clientName;
        private string droneModel;
        private string serviceProblem;
        private double serviceCost;
        private int serviceTag;

        public string displayDrone()
        {
            return $"{clientName} {serviceCost}";
        }
        public string ClientName
        {
            get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(clientName.ToLower()); }
            set { clientName = value; }
        }

        public string DroneModel
        {
            get { return droneModel; }
            set { droneModel = value; }
        }

        public string ServiceProblem
        {
            get { return char.ToUpper(serviceProblem[0]) + serviceProblem.Substring(1).ToLower(); }
            set { serviceProblem = value; }
        }

        public double ServiceCost
        {
            get { return serviceCost; }
            set { serviceCost = value; }
        }

        public int ServiceTag
        {
            get { return serviceTag; }
            set { serviceTag = value; }
        }

    }
}
