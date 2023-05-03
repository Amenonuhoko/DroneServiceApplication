using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public void displayDrone()
        {

        }

        public string getClientName() { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(clientName.ToLower()); }
        public string getDroneModel() { return droneModel; }
        public string getServiceProblem() { return char.ToUpper(serviceProblem[0]) + serviceProblem.Substring(1).ToLower(); }
        public string getServiceCost() { return serviceCost.ToString(); }
        public int getServiceTag() { return serviceTag; }

        public void setClientName(string input) { clientName = input; }
        public void setDroneModel(string input) { droneModel = input; }
        public void setServiceProblem(string input) { serviceProblem = input; }
        public void setServiceCost(double input) { serviceCost = input; }
        public void setServiceTag(int input) { serviceTag = input; }
    }
}
