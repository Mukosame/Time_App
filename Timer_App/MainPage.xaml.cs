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
using Windows.ApplicationModel;

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
        int rest1 = 0, rest2 = 0, rest3;
        bool flag = true; //default: add time running 
        //MediaElement MyMedia = new MediaElement();
        String appversion = GetAppVersion();

        public static string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;
            string temp = String.Format("{0}.{0}.{0}.{0}", version.Major, version.Minor, version.Build, version.Revision);
            return temp;
        }

        public MainPage()
        {
            this.InitializeComponent();
            List<TimeDuration> datas = new List<TimeDuration>
            {
                new TimeDuration {Num=30, Unit="秒"},// min=0, second=30},
                new TimeDuration {Num=1, Unit="分钟"},// , min=1, second=0},
                new TimeDuration {Num=2, Unit="分钟"},// , min=2, second=0},
                new TimeDuration {Num=5, Unit="分钟"},// , min=5, second=0},
                new TimeDuration {Num=10, Unit="分钟"},// , min=10, second=0},
            };
            //data source binding
            combobox.ItemsSource = datas;

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
            flag = true;
            dispatcherTimer.Start();
        }

        //stop
        private void btzclick(object sender, RoutedEventArgs e)
        {
            //flag = false;//not running
            dispatcherTimer.Stop();
            ShowTime();
            sound.Stop();
        }

        //reset to zero
        private void bqlclick(object sender, RoutedEventArgs e){
            reset();
        }

        private void reset()
        {
            flag = true;
            dispatcherTimer.Stop();
            minute1 = 0; minute2 = 0;
            second1 = 0; second2 = 0;
            //    rest1 = 0; rest2 = 0; 

            textblock1.Text = "00:00";
            //textblock2.Text = ".00";
        }

        //Set time
        //when combobox drop down closed
        private void ds (object sender, object e)
        {
            //select certain time duration
            if (combobox.SelectedItem != null)
            {
                int s = 0; int m = 0;
                TimeDuration duration = combobox.SelectedItem as TimeDuration;
                if (duration.Unit=="秒")
                {
                    s = duration.Num;
                }

                if (duration.Unit=="分钟")
                {
                    m = duration.Num;
                }
               
                countdown(s,m);
            }
           // textblock2.Text = ".000";            
        }

        //change time
        private void dispatcherTimer_Tick(object sender, object e)
        {
            ShowTime();
            if (flag == true)
            {
                AddTime();
            }
            else
            {
                MinusTime();
            }            
            
        }

        private void AddTime()
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
        }

        private void MinusTime()
        {
            second2--;
            if (second2<0)
            { 
                second1--;
                second2 = 9;
            }
            if (second1<0)
            {
                minute2--;
                second1 = 5;
            }
            if (minute2<0)
            {
                minute1--;
                minute2 = 9;
            }
            //if( (minute1==0)&(minute2==0)&(second1==0)&(second2==0) )
            if (minute1<0)
            {
                //Ding!
                //sound.Source = new Uri(@"ms-appx:///local/ding.mp3"); 
                sound.Source = new Uri("ms-appx:///ding.mp3", UriKind.Absolute);
                sound.Play();
                reset();//set time to zero
            }

        }



        private void countdown(int s, int m)
        {
            //count down to 00:00
            //and DING!
            flag = false;
            if(m>=10) //只有一个10分钟
            //{ minute1 = m/10; minute2 = m%10; second1 = second2 = 0; }
            { minute1 = 1; minute2 = 0; second1 = second2 = 0; }
            else
            { minute1 = 0; minute2 = m; second1 = second2 = 0; }
            if (m==0)
            { minute1 = 0; minute2 = 0; second1 = 3; second2 = 0; }
            dispatcherTimer.Start();

        }

        private void ShowTime()
        {
            string temp = Convert.ToString(minute1);
            temp = temp + minute2 + ":" + second1 + second2;
            textblock1.Text = temp;
            //textblock2.Text = "." + rest1 + rest2;

        }

        //Review Me！
        private async void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(
    new Uri(string.Format("ms-windows-store:reviewapp?appid=" + "cbfb5828-038a-4b6e-993f-a388ce2259a6")));
        }
        /*
        async void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            //await Windows.System.Launcher.LaunchUriAsync(new Uri(@"zune:reviewapp?appid=app" + YourAppID));
            //new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId);
            var uri = new Uri(string.Format("ms-windows-store:reviewapp?appid={0}", appid));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
         */

        //Info Page
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("/Info_Page.xaml", UriKind.Relative));
            Frame.Navigate(typeof(Time_Page),appversion);            
        }

    //
    //IF YOU WANT Add something here
    //
    }
}

//define data class
public class TimeDuration
{
    public int Num { get; set; }
    public string Unit { get; set; }
    //public int min { get; set; }
    //public int second { get; set; }
}
