using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SatelliteDataProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Dongyun Huang 30042104
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Global Methods
        // 4.1 Create two data structures using the LinkedList<T> class from the C# Systems.Collections.Generic namespace.
        // The data must be of type “double”; you are not permitted to use any additional classes, nodes, pointers or
        // data structures (array, list, etc) in the implementation of this application. The two LinkedLists of type
        // double are to be declared as global within the “public partial class”. 
        public LinkedList<double> SensorAData = new LinkedList<double>();
        public LinkedList<double> SensorBData = new LinkedList<double>();

        // 4.2 Copy the Galileo.DLL file into the root directory of your solution folder and add the appropriate reference in the solution explorer.
        // Create a method called “LoadData” which will populate both LinkedLists.
        // Declare an instance of the Galileo library in the method and create the appropriate loop construct to populate the two LinkedList;
        // the data from Sensor A will populate the first LinkedList, while the data from Sensor B will populate the second LinkedList.
        // The LinkedList size will be hardcoded inside the method and must be equal to 400. The input parameters are empty, and the return type is void. 
        public void LoadData()
        {            
            // Create an instance of the Galileo6 library
            Galileo6.ReadData galileo = new Galileo6.ReadData();

            const int LinkedListSize = 400;

            double mu = 0.0;
            double sigma = 0.0;

            // Populate Sensor A data
            for (int i = 0; i < LinkedListSize; i++)
            {
                // Call the SensorA method from the Galileo library
                double sensorAValue = galileo.SensorA(mu, sigma);
                SensorAData.AddLast(sensorAValue); // Adds a new node containing the specified value at the end of the LinkedList<T>.
            }

            // Populate Sensor B data
            for (int i = 0; i < LinkedListSize; i++)
            {
                // Call the SensorB method from the Galileo library
                double sensorBValue = galileo.SensorB(mu, sigma);
                SensorBData.AddLast(sensorBValue);
            }
        }

        // 4.3 Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView.
        // Add column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are empty, and the return type is void. 
        public void ShowAllSensorData()
        {
            //lvSensors.ItemsSource.Clear();

            var myObservableCollection = new ObservableCollection<object>();

            // Traverse both LinkedLists simultaneously and add items to the ListView
            var listAEnumerator = SensorAData.GetEnumerator();
            var listBEnumerator = SensorAData.GetEnumerator();

            myObservableCollection.Add(new { colA = listAEnumerator.Current.ToString(), colB = listBEnumerator.Current.ToString()});
            lvSensors.ItemsSource = myObservableCollection;

            //while (listAEnumerator.MoveNext() && listBEnumerator.MoveNext())
            //{
            //    ListViewItem item = new ListViewItem(new stnrig[] { listAEnumerator.Current.ToString(), listBEnumerator.Current.ToString() });

            //    lvSensors.Items.Add(item);
            //}

        }

        // 4.4 Create a button and associated click method that will call the LoadData and ShowAllSensorData methods.
        // The input parameters are empty, and the return type is void. 
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
            MessageBox.Show("test btn");
        }
        #endregion

        #region Utility Methods
        // 4,5 Create a method called “NumberOfNodes” that will return an integer which is the number of nodes(elements) in a LinkedList.
        // The method signature will have an input parameter of type LinkedList, and the calling code argument is the linkedlist name. 
        private int NumberOfNodes(LinkedList<double> newLinkedList)
        {
            return newLinkedList.Count;
        }

        // 4.6 Create a method called “DisplayListboxData” that will display the content of a LinkedList inside the appropriate ListBox.
        // The method signature will have two input parameters; a LinkedList, and the ListBox name.
        // The calling code argument is the linkedlist name and the listbox name. 
        private void DisplayListboxData(LinkedList<double> newLinkedList, ListBox newListBox)
        {
            newListBox.Items.Clear();

            // Traverse the LinkedList and add items to the ListBox
            var enumerator = newLinkedList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                newListBox.Items.Add(enumerator.Current);
            }
        }
        #endregion

        #region Sort and Search Methods 
        // 4.7 Create a method called “SelectionSort” which has a single input parameter of type LinkedList,
        // while the calling code argument is the linkedlist name.
        // The method code must follow the pseudo code supplied below in the Appendix. The return type is Boolean. 
        private bool SelectionSort(LinkedList<double> newLinkedList)
        {

            return false;  //  --- to be fixed
        }


        // 4.8 Create a method called “InsertionSort” which has a single parameter of type LinkedList,
        // while the calling code argument is the linkedlist name.
        // The method code must follow the pseudo code supplied below in the Appendix. The return type is Boolean. 
        private bool InsertionSort(LinkedList<double> newLinkedList)
        {

            return false;  //  --- to be fixed
        }

        // 4.9 Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum.
        // This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value.
        // The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list.
        // The method code must follow the pseudo code supplied below in the Appendix. 
        private int BinarySearchIterative(LinkedList<double> newLinkedList, int searchValue, int min, int max)
        {

            return 0;  //  --- to be fixed
        }

        // 4.10 Create a method called “BinarySearchRecursive” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum.
        // This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value.
        // The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list.
        // The method code must follow the pseudo code supplied below in the Appendix. 
        private int BinarySearchRecursive(LinkedList<double> newLinkedList, int searchValue, int min, int max)
        {

            return 0;  //  --- to be fixed
        }
        #endregion

        #region UI Button Methods 
        // 4.11 Create four button click methods that will search the LinkedList for an integer value entered into a textbox on the form. The four methods are: 
        // (The search code must check to ensure the data is sorted, then start a stopwatch before calling the search method.
        // Once the search is complete the stopwatch will stop, and the number of ticks will be displayed in a read only textbox.
        // Finally, the code/method will call the “DisplayListboxData” method and highlight the search target number and two values on each side. )

        // 1. Method for Sensor A and Binary Search Iterative
        private void btnIterativeA_Click(object sender, RoutedEventArgs e)
        {

        }

        // 2. Method for Sensor A and Binary Search Recursive 
        private void btnRecursiveA_Click(object sender, RoutedEventArgs e)
        {

        }

        // 3. Method for Sensor B and Binary Search Iterative 
        private void btnIterativeB_Click(object sender, RoutedEventArgs e)
        {

        }

        // 4. Method for Sensor B and Binary Search Recursive 
        private void btnRecursiveB_Click(object sender, RoutedEventArgs e)
        {

        }

        // 4.12 ...
        // 1. Method for Sensor A and Selection Sort 
        private void btnSelectionA_Click(object sender, RoutedEventArgs e)
        {

        }

        // 2. Method for Sensor A and Insertion Sort
        private void btnInsertionA_Click(object sender, RoutedEventArgs e)
        {

        }

        // 3. Method for Sensor B and Selection Sort 
        private void btnSelectionB_Click(object sender, RoutedEventArgs e)
        {

        }

        // 4. Method for Sensor B and Insertion Sort
        private void btnInsertionB_Click(object sender, RoutedEventArgs e)
        {

        }

        // 4.13 Add two numeric input controls for Sigma and Mu. The value for Sigma must be limited with a minimum of 10 and a maximum of 20.
        // Set the default value to 10. The value for Mu must be limited with a minimum of 35 and a maximum of 75. Set the default value to 50. 

        // 4.14 Add two textboxes for the search value; one for each sensor, ensure only numeric integer values can be entered. 
        // Event handler to allow only numeric integer values in the TextBox
        private void NumericIntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the input is a numeric integer (contains only digits)
            // ---- Regex Class: regex101.com/reger.com for more info.
            foreach (char c in e.Text)
            {
                e.Handled = Regex.IsMatch(e.Text, "[^[0-9]+");
                //if (!char.IsDigit(c))
                //{
                //    e.Handled = true; // Set to true to prevent the invalid character from being entered.
                //    break;
                //}
            }
        }

        // 4.15 All code is required to be adequately commented.

        #endregion
    }
}
