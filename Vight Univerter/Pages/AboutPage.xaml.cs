using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vight_Univerter
{
    public partial class AboutPage : ContentPage
    {
        public static string VERSION = "?.?.?";

        public AboutPage()
        {
            InitializeComponent();

            //显示版本号
            VersionLabel.Text = "版本号: " + VERSION;
        }

        private async void MainPageLabel_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://www.spacetimee.xyz/", BrowserLaunchMode.External);
            }
            catch
            {
                return;
            }
        }
        private async void OpenSourceLabel_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://github.com/SpaceTimee/Vight-Univerter", BrowserLaunchMode.External);
            }
            catch
            {
                return;
            }
        }
        private async void EmailLabel_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                var emailMessage = new EmailMessage("", "", "Zeus6_6@163.com");
                await Email.ComposeAsync(emailMessage);
            }
            catch
            {
                return;
            }
        }
    }
}