using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace GetPrimeUsingUIThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void NextPrimeDelegete();

        private long num = 3;

        private bool continueCalculating = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void startStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (continueCalculating)
            {
                continueCalculating = false;
                startStopButton.Content = "Resume";
            }
            else
            {
                continueCalculating = true;
                startStopButton.Content = "Stop";
                startStopButton.Dispatcher.BeginInvoke(new NextPrimeDelegete(CheckNextNumber), DispatcherPriority.Normal, null);

            }


            #region test change the UI in a new thread             
            //try
            //{
            //    Thread t = new Thread(TestChangeUIInANewThread);
            //    t.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //} 
            #endregion
        }

        //public void TestChangeUIInANewThread()
        //{
        //    try
        //    {
        //        if (bigPrime.CheckAccess())
        //        {
        //            bigPrime.Text = "This is a new thread!";
        //        }

        //        int tid = Thread.CurrentThread.ManagedThreadId;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private bool notAPrime = false;
        public void CheckNextNumber()
        {
            //reset flag
            notAPrime = false;

            for (long i = 3; i <= Math.Sqrt(num); i++)
            {
                if ((num % i) == 0)
                {
                    notAPrime = true;
                    break;
                }
            }

            //If a prime number
            if (!notAPrime)
            {
                bigPrime.Text = num.ToString();
            }

            num += 2;

            if (continueCalculating)
            {
                startStopButton.Dispatcher.BeginInvoke(new NextPrimeDelegete(CheckNextNumber) ,DispatcherPriority.SystemIdle, null);
            }
        }
    }
}
