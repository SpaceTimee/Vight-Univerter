using System;
using Windows.ApplicationModel;

namespace Vight_Univerter.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            AboutPage.VERSION = Convert.ToString(version.Major) + '.' + Convert.ToString(version.Minor) + '.' + Convert.ToString(version.Build);
            LoadApplication(new Vight_Univerter.App());
        }
    }
}