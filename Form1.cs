using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InsertionMergeSortPlotting
{

    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Store integer from user input in n
            int n = Convert.ToInt32(textBox1.Text);

            //Create two new double arrays to store execution times for each algorithm
            double[] insertionTimeArray = new double[n];
            double[] mergeTimeArray = new double[n];

            //Two stopwatches to  measure execution times
            var watchInsert = new System.Diagnostics.Stopwatch();
            var watchMerge = new System.Diagnostics.Stopwatch();

            //Create n number of arrays where n varies from 1 to n
            for (int i = 1; i <= n; ++i)
            {
                //Initialize array of length i
                int[] arr = new int[i];
                Random rand = new Random();
                for (int j = 0; j < arr.Length; j++)
                {
                    //populates array with random numbers from 0 to Int max
                    arr[j] = rand.Next(int.MaxValue);
                }

                //Make copy of random array so one can be insertion sorted and one can be merge sorted
                int[] arr2 = new int[i];
                Array.Copy(arr, arr2, i);

                //Insertion sort call
                Form1 objInsert = new Form1();         
                watchInsert.Start(); // Watch start
                objInsert.insertion_sort(arr2); //Method call
                watchInsert.Stop();  // Watch stop
                var elapsedMsInsertion = watchInsert.Elapsed.TotalMilliseconds;
                insertionTimeArray[i-1] = elapsedMsInsertion; //Add that execution time to the array

                //Merge sort call
                Form1 objMerge = new Form1();
                watchMerge.Start();
                objMerge.sort(arr, 0, arr.Length - 1);
                watchMerge.Stop();
                var elapsedMsMerge = watchMerge.Elapsed.TotalMilliseconds;
                mergeTimeArray[i-1] = elapsedMsMerge;

            }

            //Debug info - displays execution times
            //string toDisplay3 = string.Join(Environment.NewLine, insertionTimeArray);
            //MessageBox.Show(toDisplay3); ;
            //
            //string toDisplay4 = string.Join(Environment.NewLine, mergeTimeArray);
            //MessageBox.Show(toDisplay4, "Merge time");

            //Remove default series from chart
            chart1.Series.Clear();
            

            //Config for insertion sort line
            chart1.Series.Add("Insertion Sort");
            chart1.Series["Insertion Sort"].ChartType = SeriesChartType.Line;
            chart1.Series["Insertion Sort"].Color = Color.Orange;

            //Add insertion sort data
            for (int i = 0; i < n; ++i)
            {
                chart1.Series["Insertion Sort"].Points.AddXY(i, insertionTimeArray[i]);
            }

            //Config for merge sort line
            chart1.Series.Add("Merge Sort");
            chart1.Series["Merge Sort"].ChartType = SeriesChartType.Line;
            chart1.Series["Merge Sort"].Color = Color.Blue;

            //Add merge sort data
            for(int i = 0; i < n; ++i)
            {
                chart1.Series["Merge Sort"].Points.AddXY(i, mergeTimeArray[i]);
            }
            
            // Label x axis
            chart1.ChartAreas[0].AxisX.Title = "n";
            chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
            // Label y axis
            chart1.ChartAreas[0].AxisY.Title = "ms";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "";
            chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = true;

            chart1.Visible = true;
        }

        public void merge(int[] arr, int left, int m, int right)
        {
            //Find sizes of two arrays being merged
            int n1 = m - left + 1;
            int n2 = right - m;

            //Create temp arrays, one for left one for right of original array
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            //Populate the two arrays with the appropriate values from the original array
            for (i = 0; i < n1; ++i)
                L[i] = arr[left + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            //Initial index of temp arrays
            //Different from pseudocode since C# is zero indexing
            i = 0;
            j = 0;

            //Merge the two arrays
            int k = left;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            //If any remaining elements in left array, add it to merged array
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            //If any remaining elements in right array, add it to merged array
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        void sort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                //Find mid point of array
                int m = l + (r - l) / 2;

                //Divide original array into 2 down the middle
                //Recursive call to itself to keep dividing array into 2 until array size is 1
                sort(arr, l, m);
                sort(arr, m + 1, r);

                // Merge all subarrays
                merge(arr, l, m, r);
            }
        }

        void insertion_sort(int[] arr2)
        {
            int j, i, key;
            for (j = 1; j < arr2.Length; j++)
            {
                //put arr2[j] into sorted sequence
                key = arr2[j];
                i = j - 1;
                while (i >= 0 && arr2[i] > key)
                {
                    //swap elements if larger number is on the left
                    arr2[i + 1] = arr2[i];
                    i = i - 1;
                }
                //Put smaller number(saved in key) in front
                arr2[i + 1] = key;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
