using Autodesk.Revit.UI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class ElementViewModel : BindableBase
    {
        public ElementViewModel(UIApplication application)
        {
            ElementParameters = new ParametersViewModel(application);
            SymbolParameters = new ParametersViewModel(application);
            Load = new LoadElementCommand(application, this);
        }

        public ICommand Load { get; }
        public ParametersViewModel ElementParameters { get; }
        public ParametersViewModel SymbolParameters { get; }
        public ObservableCollection<Property> Properties { get; } = new ObservableCollection<Property>();
    }
}
