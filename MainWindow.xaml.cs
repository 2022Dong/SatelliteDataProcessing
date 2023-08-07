using Galileo6;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Develop a .NET Multi-platform App for Malin Space Science Systems to process and analyze satellite data 
    /// using sorting and searching algorithms.
    /// 
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

            // Clear existing data before populating the LinkedLists again
            SensorAData.Clear();
            SensorBData.Clear();

            // Populate Sensor A data
            for (int i = 0; i < 400; i++)
            {
                // Call the SensorA method from the Galileo library
                double sensorAValue = galileo.SensorA((double)Mu.Value, (double)Sigma.Value);
                SensorAData.AddLast(sensorAValue); // Adds a new node containing the specified value at the end of the LinkedList<T>.
            }

            // Populate Sensor B data
            for (int i = 0; i < 400; i++)
            {
                // Call the SensorB method from the Galileo library
                double sensorBValue = galileo.SensorB((double)Mu.Value, (double)Sigma.Value);
                SensorBData.AddLast(sensorBValue);
            }
        }

        // 4.3 Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView.
        // Add column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are empty, and the return type is void. 
        public void ShowAllSensorData()
        {
            lvSensors.Items.Clear();
            for (int x = 0; x < 400; x++)
            {
                lvSensors.Items.Add(new
                {
                    SensorA = SensorAData.ElementAt(x).ToString(),
                    SensorB = SensorBData.ElementAt(x).ToString()
                });
            }

            //var myObservableCollection = new ObservableCollection<object>();

            //// Traverse both LinkedLists simultaneously and add items to the ListView
            //var listAEnumerator = SensorAData.GetEnumerator();
            //var listBEnumerator = SensorBData.GetEnumerator();

            ////myObservableCollection.Add(new { colA = listAEnumerator.Current.ToString(), colB = listBEnumerator.Current.ToString()});
            ////lvSensors.ItemsSource = myObservableCollection;

            //while (listAEnumerator.MoveNext() && listBEnumerator.MoveNext())
            //{
            //    myObservableCollection.Add(new { SensorA = listAEnumerator.Current, SensorB = listBEnumerator.Current });
            //}
            //// Clear the existing items in the ListView
            //lvSensors.ItemsSource = null;
            //// Set the ObservableCollection as the ItemsSource for the ListView
            //lvSensors.ItemsSource = myObservableCollection;            
        }

        // 4.4 Create a button and associated click method that will call the LoadData and ShowAllSensorData methods.
        // The input parameters are empty, and the return type is void. 
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();

            DisplayListboxData(SensorAData, lbSensorA);
            DisplayListboxData(SensorBData, lbSensorB);

            // Enable all sort buttons by default.
            btnSelectionA.IsEnabled = true;
            btnSelectionB.IsEnabled = true;
            btnInsertionA.IsEnabled = true;
            btnInsertionB.IsEnabled = true;
            // Disable all search buttons, because of unsorted lists
            btnIterativeA.IsEnabled = false;
            btnRecursiveA.IsEnabled = false;
            btnIterativeB.IsEnabled = false;
            btnRecursiveB.IsEnabled = false;
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

            // Traverse the LinkedList and add each element to the ListBox
            foreach (var item in newLinkedList)
            {
                newListBox.Items.Add(item);
            }
        }
        #endregion

        #region Sort and Search Methods 
        // 4.7 Create a method called “SelectionSort” which has a single input parameter of type LinkedList,
        // while the calling code argument is the linkedlist name.
        // The method code must follow the pseudo code supplied below in the Appendix. The return type is Boolean. 
        private bool SelectionSort(LinkedList<double> list)
        {
            if (list == null || list.Count <= 1)
            {
                // The list is already sorted if it contains 0 or 1 element.
                return true;
            }

            int min = 0;
            int max = NumberOfNodes(list);

            for (int i = 0; i < max - 1; i++)
            {
                min = i;

                for (int j = i + 1; j < max; j++)
                {
                    if (list.ElementAt(j) < list.ElementAt(min))
                    {
                        min = j;
                    }
                }

                // Swap elements at index i and min.
                LinkedListNode<double> currentMin = list.Find(list.ElementAt(min));
                LinkedListNode<double> currentI = list.Find(list.ElementAt(i));

                double temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;
            }
            return true;
        }

        // 4.8 Create a method called “InsertionSort” which has a single parameter of type LinkedList,
        // while the calling code argument is the linkedlist name.
        // The method code must follow the pseudo code supplied below in the Appendix. The return type is Boolean. 
        private bool InsertionSort(LinkedList<double> list)
        {
            if (list == null || list.Count <= 1)
            {
                // The list is already sorted if it contains 0 or 1 element.
                return true;
            }

            int max = NumberOfNodes(list);

            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (list.ElementAt(j - 1) > list.ElementAt(j))
                    {
                        // Swap elements at index j - 1 and j.
                        LinkedListNode<double> current = list.Find(list.ElementAt(j));
                        LinkedListNode<double> previous = list.Find(list.ElementAt(j - 1));
                        double temp = current.Value;
                        current.Value = previous.Value;
                        previous.Value = temp;
                    }
                    else
                    {
                        // The element at index j is in the correct position.
                        // Break the inner loop as further iterations are not required.
                        break;
                    }
                }
            }
            return true;
        }

        // 4.9 Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum.
        // This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value.
        // The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list.
        // The method code must follow the pseudo code supplied below in the Appendix. 
        private int BinarySearchIterative(LinkedList<double> list, int searchValue, int min, int max)
        {            
            while (min <= max -1)
            {
                int middle = (min + max) / 2;
                double middleValue = list.ElementAt(middle);

                if (searchValue == middleValue)
                {
                    // If the search value is found, return the index (position) of the element.
                    return ++middle;
                }
                else if (searchValue < middleValue)
                {
                    // If the search value is less than the middle element,
                    // update the maximum to search in the left half.
                    max = middle - 1;
                }
                else
                {
                    // If the search value is greater than the middle element,
                    // update the minimum to search in the right half.
                    min = middle + 1;
                }
            }

            // If the search value is not found, return the position where it should be inserted (nearest neighbor).
            return min;
        }

        // 4.10 Create a method called “BinarySearchRecursive” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum.
        // This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value.
        // The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list.
        // The method code must follow the pseudo code supplied below in the Appendix. 
        private int BinarySearchRecursive(LinkedList<double> list, int searchValue, int min, int max)
        {
            if (min <= max - 1)
            {
                int middle = (min + max) / 2;
                double middleValue = list.ElementAt(middle);

                if (searchValue == middleValue)
                {
                    // If the search value is found, return the index (position) of the element.
                    return middle;
                }
                else if (searchValue < middleValue)
                {
                    // If the search value is less than the middle element,
                    // recursively search in the left half.
                    return BinarySearchRecursive(list, searchValue, min, middle - 1);
                }
                else
                {
                    // If the search value is greater than the middle element,
                    // recursively search in the right half.
                    return BinarySearchRecursive(list, searchValue, middle + 1, max);
                }
            }

            // If the search value is not found, return the position where it should be inserted (nearest neighbor).
            return min;
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
            txtIterativeA.Text = "";

            if (int.TryParse(txtSearchTargetA.Text, out int searchA))
            {
                int min = 0;
                int max = NumberOfNodes(SensorAData);

                var watch = System.Diagnostics.Stopwatch.StartNew();
                int found = BinarySearchIterative(SensorAData, searchA, min, max);
                watch.Stop();
                var elapsedTicks = watch.ElapsedTicks;

                txtIterativeA.Text = $"{elapsedTicks} Ticks";

                HightlightListbox(found, lbSensorA);
            }
            else
            {
                txtIterativeA.Text = "emtpy target";
            }
        }

        // 2. Method for Sensor A and Binary Search Recursive 
        private void btnRecursiveA_Click(object sender, RoutedEventArgs e)
        {
            txtRecursiveA.Text = "";
            if (int.TryParse(txtSearchTargetA.Text, out int searchA))
            {
                int min = 0;
                int max = NumberOfNodes(SensorAData);

                var watch = System.Diagnostics.Stopwatch.StartNew();
                int found = BinarySearchIterative(SensorAData, searchA, min, max);
                watch.Stop();
                var elapsedTicks = watch.ElapsedTicks;

                txtRecursiveA.Text = $"{elapsedTicks} Ticks";

                HightlightListbox(found, lbSensorA);
            }
            else
            {
                txtRecursiveA.Text = "emtpy target";
            }

        }

        // 3. Method for Sensor B and Binary Search Iterative 
        private void btnIterativeB_Click(object sender, RoutedEventArgs e)
        {
            txtIterativeB.Text = "";

            if (int.TryParse(txtSearchTargetB.Text, out int searchB))
            {
                int min = 0;
                int max = NumberOfNodes(SensorBData);

                var watch = System.Diagnostics.Stopwatch.StartNew();
                int found = BinarySearchIterative(SensorBData, searchB, min, max);
                watch.Stop();
                var elapsedTicks = watch.ElapsedTicks;

                txtIterativeB.Text = $"{elapsedTicks} Ticks";

                HightlightListbox(found, lbSensorB);
            }
            else
            {
                txtIterativeB.Text = "emtpy target";
            }

        }

        // 4. Method for Sensor B and Binary Search Recursive 
        private void btnRecursiveB_Click(object sender, RoutedEventArgs e)
        {
            txtRecursiveB.Text = "";
            if (int.TryParse(txtSearchTargetB.Text, out int searchB))
            {
                int min = 0;
                int max = NumberOfNodes(SensorBData);

                var watch = System.Diagnostics.Stopwatch.StartNew();
                int found = BinarySearchIterative(SensorBData, searchB, min, max);
                watch.Stop();
                var elapsedTicks = watch.ElapsedTicks;

                txtRecursiveB.Text = $"{elapsedTicks} Ticks";

                HightlightListbox(found, lbSensorB);
            }
            else
            {
                txtRecursiveB.Text = "emtpy target";
            }
        }

        // 4.12 Create four button click methods that will sort the LinkedList using the Selection and Insertion methods.
        // (The button method must start a stopwatch before calling the sort method. Once the sort is complete the stopwatch will stop,
        // and the number of milliseconds will be displayed in a read only textbox.
        // Finally, the code/method will call the “ShowAllSensorData” method and “DisplayListboxData” for the appropriate sensor. )
        // 1. Method for Sensor A and Selection Sort 
        private void btnSelectionA_Click(object sender, RoutedEventArgs e)
        {
            // A tick (0.0000001 seconds) represents a single increment of the system's internal clock.
            // Ticks are a unit of time used to measure the passage of time in computer systems. 
            // A milliseconds(ms) (0.001 seconds) is commonly used to measure very short durations, especially in computer programming,
            // where operations are often executed in milliseconds.

            txtSelectionA.Text = "";

            var watch = System.Diagnostics.Stopwatch.StartNew(); // more precise than DateTime.Now()
            SelectionSort(SensorAData);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            txtSelectionA.Text = $"{elapsedMs} milliseconds";
            DisplayListboxData(SensorAData, lbSensorA);
            btnIterativeA.IsEnabled = true;
            btnRecursiveA.IsEnabled = true;
            btnInsertionA.IsEnabled = false;
        }

        // 2. Method for Sensor A and Insertion Sort
        private void btnInsertionA_Click(object sender, RoutedEventArgs e)
        {
            txtInsertionA.Text = "";

            var watch = System.Diagnostics.Stopwatch.StartNew();
            InsertionSort(SensorAData);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            txtInsertionA.Text = $"{elapsedMs} milliseconds";
            DisplayListboxData(SensorAData, lbSensorA);
            btnIterativeA.IsEnabled = true;
            btnRecursiveA.IsEnabled = true;
            btnSelectionA.IsEnabled = false;
        }

        // 3. Method for Sensor B and Selection Sort 
        private void btnSelectionB_Click(object sender, RoutedEventArgs e)
        {
            txtSelectionB.Text = "";

            var watch = System.Diagnostics.Stopwatch.StartNew();
            SelectionSort(SensorBData);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            txtSelectionB.Text = $"{elapsedMs} milliseconds";
            DisplayListboxData(SensorBData, lbSensorB);
            btnIterativeB.IsEnabled = true;
            btnRecursiveB.IsEnabled = true;
            btnInsertionB.IsEnabled = false;

        }

        // 4. Method for Sensor B and Insertion Sort
        private void btnInsertionB_Click(object sender, RoutedEventArgs e)
        {
            txtInsertionB.Text = "";

            var watch = System.Diagnostics.Stopwatch.StartNew();
            InsertionSort(SensorBData);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            txtInsertionB.Text = $"{elapsedMs} milliseconds";
            DisplayListboxData(SensorBData, lbSensorB);
            btnIterativeB.IsEnabled = true;
            btnRecursiveB.IsEnabled = true;
            btnSelectionB.IsEnabled = false;
        }

        // 4.13 Add two numeric input controls for Sigma and Mu. The value for Sigma must be limited with a minimum of 10 and a maximum of 20.
        // Set the default value to 10. The value for Mu must be limited with a minimum of 35 and a maximum of 75. Set the default value to 50. 
        private void Sigma_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Sigma.Value = (int)e.NewValue;
        }

        private void Mu_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Mu.Value = (int)e.NewValue;
        }
        // 4.14 Add two textboxes for the search value; one for each sensor, ensure only numeric integer values can be entered. 
        // Event handler to allow only numeric integer values in the TextBox
        private void NumericIntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the input is a numeric integer (contains only digits)
            // ---- Regex Class: regex101.com/reger.com for more info.
            foreach (char c in e.Text)
            {
                e.Handled = Regex.IsMatch(e.Text, "[^[0-9]+");
            }
        }

        // Highlight the search target number and two values on each side. 
        private void HightlightListbox(int newFound, ListBox newListBox)
        {
            // Set listbox.
            newListBox.SelectionMode = SelectionMode.Multiple;
            // Clear any previous selection
            newListBox.SelectedItems.Clear();

            if (newFound >= 0 && newFound < 400)
            {
                // Highlight the range (5 elements) centered around the result
                int start = Math.Max(0, newFound - 2); // Ensure start index is >= 0
                int end = Math.Min(399, newFound + 2); // Ensure end index is within bounds

                // Select the items in the specified range
                for (int i = start; i <= end; i++)
                {
                    newListBox.SelectedItems.Add(newListBox.Items[i]);
                }
            }
        }

        // 4.15 All code is required to be adequately commented.
        #endregion
    }
}
