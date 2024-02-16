using Autodesk.Revit.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitDevX.UI
{
    public class MainViewModel : BindableBase
    {
        private UIApplication _application;

        public MainViewModel(UIApplication application)
        {
            _application = application;

            ElementPropertiesViewModel = new ElementPropertiesViewModel(application);
        }

        public ElementPropertiesViewModel ElementPropertiesViewModel { get; }
    }
}
