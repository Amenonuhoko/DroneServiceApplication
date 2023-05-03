using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Mauriza Arianne
// DroneServiceApp
// 26/04/2023

namespace DroneServiceApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 6.2	Create a global List<T> of type Drone called “FinishedList”. 
        // 6.3	Create a global Queue<T> of type Drone called “RegularService”.
        // 6.4	Create a global Queue<T> of type Drone called “ExpressService”.
        List<Drone> FinishedList = new List<Drone>();
        Queue<Drone> RegularService = new Queue<Drone>();
        Queue<Drone> ExpressService = new Queue<Drone>();

        // 6.5	Create a button method called “AddNewItem” that will add a new service item to a Queue<> based on the priority.
        // Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost.
        // Use a numeric up/down control for the Service Tag.
        // The new service item will be added to the appropriate Queue based on the Priority radio button.
        public void AddNewItem()
        {
            Drone newDrone = new Drone();
            newDrone.setClientName(txtClientName.Text);
            newDrone.setDroneModel(txtDroneModel.Text);
            newDrone.setServiceProblem(txtServiceProblem.Text);
            newDrone.setServiceCost(double.Parse(txtServiceTag.Text));
            newDrone.setServiceTag(int.Parse(txtServiceTag.Text));
            if (GetServicePriority() == 1)
            {
                RegularService.Enqueue(newDrone);
            } else if (GetServicePriority() == 2)
            {
                // 6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
                newDrone.setServiceCost(double.Parse(txtServiceTag.Text) * 1.15);
                ExpressService.Enqueue(newDrone);
            }
            DisplayRegularService();

        }
        // 6.8	Create a custom method that will display all the elements in the RegularService queue.
        // The display must use a List View and with appropriate column headers.
        public void DisplayRegularService()
        {
            ObservableCollection<Drone> collection = new ObservableCollection<Drone>();
            foreach (Drone drone in RegularService)
            {
                collection.Add(drone);
            }
            lvRegular.ItemsSource = collection;

        }
        // 6.9	Create a custom method that will display all the elements in the ExpressService queue.
        // The display must use a List View and with appropriate column headers.
        public void DisplayExpressService()
        {

        }
        // 6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group.
        // This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        public int GetServicePriority()
        {
            int priority = 0;
            if (rbRegular.IsChecked == true)
            {
                priority = 1;
            } else if (rbExpress.IsChecked == true)
            {
                priority = 2;
            }
            return priority;
        }

        private void btnServiceTagInc_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtServiceTag.Text, out int tag))
            {
                if (tag < 900) txtServiceTag.Text = (tag + 10).ToString();
            }
        }
        private void btnServiceTagDec_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtServiceTag.Text, out int tag))
            {
                if (tag > 100) txtServiceTag.Text = (tag - 10).ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewItem();
        }
    }
}
