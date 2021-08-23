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

            LoadApplication(new Vight_Univerter.App());
        }
    }
}
