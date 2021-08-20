using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vight_Univerter
{
    public partial class TabPage : TabbedPage
    {
        public TabPage()
        {
            InitializeComponent();

            Task.Factory.StartNew
            (
                () =>
                {
                    foreach (var i in Children)
                    {
                        (i as UniverterPage).InitPickerList();
                    }
                }
            );
        }
    }
}