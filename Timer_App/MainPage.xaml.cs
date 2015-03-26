using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Timer_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        //int minute=0;
        //int second=0;
        //int rest=0;
        int minute1=0, minute2=0, second1=0, second2=0;
        int rest1=0, rest2=0, rest3=0;
        bool flag = false; //default: not running 
        

        public MainPage()
        {
            this.InitializeComponent();

            dispatcherTimer.Tick += new EventHandler<object>(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            //set time interval to 1s

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        //start
        private void bksclick(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
        }

        //stop
        private void btzclick(object sender, RoutedEventArgs e)
        {
            //flag = false;//not running
            dispatcherTimer.Stop();
            ShowTime();
        }

        //reset to zero
        private void bqlclick(object sender, RoutedEventArgs e)
        {
            //flag = false;
            dispatcherTimer.Stop();
            minute1 = 0; minute2 = 0;
            second1 = 0; second2 = 0;
            //    rest1 = 0; rest2 = 0; 

            textblock1.Text = "00:00";
            //textblock2.Text = ".00";
        }

        //Set time
        private void time_changed(object sender, TimePickerValueChangedEventArgs e)
        {
            // e.OldTime - 原时间
            // e.NewTime - 新时间
           // textblock1.Text = TimePicker.Time.ToString();
            textblock2.Text = ".000";
            //TimePicker.Time.ToString
            countdown();
        }

        private void bdsclick(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("/Time_Page.xaml", UriKind.Relative));

        }

        //change time
        private void dispatcherTimer_Tick(object sender, object e)
        {
            /* rest2++;
             if (rest2>9)
             {
                 rest1++;
                 rest2 = 0;
             }
             if (rest1>9)
             {
             */
            second2++;
            //rest1 = 0;
            //}

            if (second2 > 9)
            {
                second1++;
                second2 = 0;
            }
            if (second1 > 5)
            {
                minute2++;
                second1 = 0;
            }
            if (minute2 > 9)
            {
                minute1++;
                minute2 = 0;
            }

            ShowTime();
        }

        private void countdown()
        {
            //count down to 00:00
            //and DING!

        }

        private void ShowTime()
        {
            string temp = Convert.ToString(minute1);
            temp = temp + minute2 + ":" + second1 + second2;
            textblock1.Text = temp;
            //textblock2.Text = "." + rest1 + rest2;

        }
    }
}
