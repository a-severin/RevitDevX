using Autodesk.Revit.UI;

namespace RevitDevX.UI
{
    public class MainViewModel : BindableBase
    {
        private UIApplication _application;

        public MainViewModel(UIApplication application)
        {
            _application = application;

            ElementViewModel = new ElementViewModel(application);
            RoomViewModel = new RoomViewModel(application);
            AllRoomsViewModel = new AllRoomsViewModel(application);
        }

        public ElementViewModel ElementViewModel { get; }
        public RoomViewModel RoomViewModel { get; }
        public AllRoomsViewModel AllRoomsViewModel { get; }
    }
}
