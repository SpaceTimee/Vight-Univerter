using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vight_Univerter
{
    public partial class TabPage : TabbedPage
    {
        public TabPage()
        {
            InitializeComponent();

            //必须在初始化之后才能确定每个UniverterPage是分别什么功能
            foreach (var i in Children)
            {
                if (i.GetType() != typeof(AboutPage))
                    Task.Run((i as UniverterPage).InitPickerList);
            }
        }
    }
}