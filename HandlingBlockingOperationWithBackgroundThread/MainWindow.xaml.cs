using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HandlingBlockingOperationWithBackgroundThread
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
    }
}
/*
Handling a Blocking Operation with a Background Thread

Handling blocking operations in a graphical application can be difficult. We don’t want to call blocking methods from event handlers because the application will appear to freeze up. We can use a separate thread to handle these operations, but when we’re done, we have to synchronize with the UI thread because we can’t directly modify the GUI from our worker thread. We can use Invoke or BeginInvoke to insert delegates into the Dispatcher of the UI thread. Eventually, these delegates will be executed with permission to modify UI elements.
In this example, we mimic a remote procedure call that retrieves a weather forecast. We use a separate worker thread to execute this call, and we schedule an update method in the Dispatcher of the UI thread when we’re finished.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace SDKSamples
{
    public partial class Window1 : Window
    {
        // Delegates to be used in placking jobs onto the Dispatcher.
        private delegate void NoArgDelegate();
        private delegate void OneArgDelegate(String arg);

        // Storyboards for the animations.
        private Storyboard showClockFaceStoryboard;
        private Storyboard hideClockFaceStoryboard;
        private Storyboard showWeatherImageStoryboard;
        private Storyboard hideWeatherImageStoryboard;

        public Window1() : base()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the storyboard resources.
            showClockFaceStoryboard =
                (Storyboard)this.Resources["ShowClockFaceStoryboard"];
            hideClockFaceStoryboard =
                (Storyboard)this.Resources["HideClockFaceStoryboard"];
            showWeatherImageStoryboard =
                (Storyboard)this.Resources["ShowWeatherImageStoryboard"];
            hideWeatherImageStoryboard =
                (Storyboard)this.Resources["HideWeatherImageStoryboard"];
        }

        private void ForecastButtonHandler(object sender, RoutedEventArgs e)
        {
            // Change the status image and start the rotation animation.
            fetchButton.IsEnabled = false;
            fetchButton.Content = "Contacting Server";
            weatherText.Text = "";
            hideWeatherImageStoryboard.Begin(this);

            // Start fetching the weather forecast asynchronously.
            NoArgDelegate fetcher = new NoArgDelegate(
                this.FetchWeatherFromServer);

            fetcher.BeginInvoke(null, null);
        }

        private void FetchWeatherFromServer()
        {
            // Simulate the delay from network access.
            Thread.Sleep(4000);

            // Tried and true method for weather forecasting - random numbers.
            Random rand = new Random();
            String weather;

            if (rand.Next(2) == 0)
            {
                weather = "rainy";
            }
            else
            {
                weather = "sunny";
            }

            // Schedule the update function in the UI thread.
            tomorrowsWeather.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                new OneArgDelegate(UpdateUserInterface),
                weather);
        }

        private void UpdateUserInterface(String weather)
        {
            //Set the weather image
            if (weather == "sunny")
            {
                weatherIndicatorImage.Source = (ImageSource)this.Resources[
                    "SunnyImageSource"];
            }
            else if (weather == "rainy")
            {
                weatherIndicatorImage.Source = (ImageSource)this.Resources[
                    "RainingImageSource"];
            }

            //Stop clock animation
            showClockFaceStoryboard.Stop(this);
            hideClockFaceStoryboard.Begin(this);

            //Update UI text
            fetchButton.IsEnabled = true;
            fetchButton.Content = "Fetch Forecast";
            weatherText.Text = weather;
        }

        private void HideClockFaceStoryboard_Completed(object sender,
            EventArgs args)
        {
            showWeatherImageStoryboard.Begin(this);
        }

        private void HideWeatherImageStoryboard_Completed(object sender,
            EventArgs args)
        {
            showClockFaceStoryboard.Begin(this, true);
        }
    }
}
*/
