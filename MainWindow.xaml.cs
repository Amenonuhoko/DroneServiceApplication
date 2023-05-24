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
            // Create new drone object to fill
            if (!string.IsNullOrEmpty(txtClientName.Text) && !string.IsNullOrEmpty(txtDroneModel.Text) && !string.IsNullOrEmpty(txtServiceProblem.Text))
            {
                Drone newDrone = new Drone();
                newDrone.ClientName = txtClientName.Text;
                newDrone.DroneModel = txtDroneModel.Text;
                newDrone.ServiceProblem = txtServiceProblem.Text;
                newDrone.ServiceCost = double.Parse(txtServiceCost.Text);
                newDrone.ServiceTag = int.Parse(txtServiceTag.Text);
                // Check priority and add to appropriate queue
                // Refresh Display after queue
                if (GetServicePriority() == 1)
                {
                    RegularService.Enqueue(newDrone);
                    DisplayRegularService();
                    txtServiceTag.Text = (int.Parse(txtServiceTag.Text) + 10).ToString();
                }
                else if (GetServicePriority() == 2)
                {
                    // 6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
                    newDrone.ServiceCost = Math.Round(double.Parse(txtServiceTag.Text) * 1.15, 2);
                    ExpressService.Enqueue(newDrone);
                    DisplayExpressService();
                    txtServiceTag.Text = (int.Parse(txtServiceTag.Text) + 10).ToString();
                }
                // Clear fields after adding
                ClearFields();
            }           
        }
        // Add logic to button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GetServicePriority() > 0)
            {
                AddNewItem();
            }
        }

        // 6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group.
        // This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        public int GetServicePriority()
        {
            // Create int 
            int priority = 0;
            // Check buttons and set priority
            if (rbRegular.IsChecked == true)
            {
                priority = 1;
            }
            else if (rbExpress.IsChecked == true)
            {
                priority = 2;
            }
            // Return int
            return priority;
        }
        // 6.8	Create a custom method that will display all the elements in the RegularService queue.
        // The display must use a List View and with appropriate column headers.
        public void DisplayRegularService()
        {
            // Create a collection of drones to display
            List<Drone> collection = new List<Drone>();
            // Loop through data and add each to collection
            foreach (Drone drone in RegularService)
            {
                collection.Add(drone);
            }
            // Set item source to collection
            lvRegular.ItemsSource = collection;
        }
        // 6.9	Create a custom method that will display all the elements in the ExpressService queue.
        // The display must use a List View and with appropriate column headers.
        public void DisplayExpressService()
        {
            // Same
            List<Drone> collection = new List<Drone>();
            foreach (Drone drone in ExpressService)
            {
                collection.Add(drone);
            }
            lvExpress.ItemsSource = collection;
        }
        // 6.10	Create a custom keypress method to ensure the Service Cost textbox can only accept a double value with one decimal point.
        private void txtServiceCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if text is a number and if text is not a dot
            if (!int.TryParse(e.Text, out int _) && e.Text != ".")
            {
                // If true then do not allow input
                e.Handled = true;
            }
            // Else if text is a dot and textbox already contains a dot
            else if (e.Text == "." && txtServiceCost?.Text?.Contains(".") == true)
            {
                // Then do not allow input
                e.Handled = true;
            }
            // Else if textbox contains a dot and the length of the text after the dot is greater than 1
            else if (txtServiceCost?.Text?.Contains(".") == true && txtServiceCost.Text.Substring(txtServiceCost.Text.IndexOf('.')).Length > 1)
            {   // Then do not allow input
                e.Handled = true;
            }
        }
        // 6.11	Create a custom method to increment the service tag control,
        // this method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        private void btnServiceTagInc_Click(object sender, RoutedEventArgs e)
        {
            // Parse text as int and increment if less than 900
            if (int.TryParse(txtServiceTag.Text, out int tag))
            {
                if (tag < 900) txtServiceTag.Text = (tag + 10).ToString();
            }
        }
        private void btnServiceTagDec_Click(object sender, RoutedEventArgs e)
        {
            // Parse text as int and decrement if greater than 100
            if (int.TryParse(txtServiceTag.Text, out int tag))
            {
                if (tag > 100) txtServiceTag.Text = (tag - 10).ToString();
            }
        }
        // 6.12	Create a mouse click method for the regular service ListView that will display the Client Name and Service Problem in the related textboxes.
        private void lvRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Display the text if selected
            // Grab index display at at index
            int index = lvRegular.SelectedIndex;
            if (index !=  -1)
            {
                txtClientName.Text = RegularService.ToArray()[index].ClientName;
                txtServiceProblem.Text = RegularService.ToArray()[index].ServiceProblem.ToString();
            }
            
        }
        // 6.13	Create a mouse click method for the express service ListView that will display the Client Name and Service Problem in the related textboxes.
        private void lvExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Same
            int index = lvExpress.SelectedIndex;
            if (index != -1)
            {
                txtClientName.Text = ExpressService.ToArray()[index].ClientName;
                txtServiceProblem.Text = ExpressService.ToArray()[index].ServiceProblem.ToString();

            }
        }

        // 6.14	Create a button click method that will remove a service item from the regular ListView and dequeue the regular service Queue<T> data structure.
        // The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        private void btnRegularComplete_Click(object sender, RoutedEventArgs e)
        {
            // Add the last entry and put it in the finished list
            FinishedList.Add(RegularService.Dequeue());
            DisplayRegularService();
            DisplayFinishedList();
        }
        private void DisplayFinishedList()
        {
            // Display from data
            foreach (var items in FinishedList)
            {
                lbFinishedList.Items.Add(items.displayDrone());
            }
        }     
        // 6.15	Create a button click method that will remove a service item from the express ListView and dequeue the express service Queue<T> data structure.
        // The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        private void btnExpressComplete_Click(object sender, RoutedEventArgs e)
        {
            // Same
            FinishedList.Add(ExpressService.Dequeue());
            DisplayExpressService();
            DisplayFinishedList();
        }
        // 6.16	Create a double mouse click method that will delete a service item from the finished listbox and remove the same item from the List<T>.
        private void lbFinishedList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Grab index and remove at index
            int index = lbFinishedList.SelectedIndex;
            if (index != -1)
            {
                FinishedList.RemoveAt(index);
                lbFinishedList.Items.RemoveAt(index);
            }
        }

        // 6.17	Create a custom method that will clear all the textboxes after each service item has been added.
        private void ClearFields()
        {
            // Clear all textboxes
            txtClientName.Clear();
            txtDroneModel.Clear();
            txtServiceProblem.Clear();
            txtServiceCost.Clear();
            txtServiceCost.Clear();
        }
    }
}
