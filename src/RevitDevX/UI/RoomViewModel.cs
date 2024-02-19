using Autodesk.Revit.UI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class RoomViewModel : BindableBase
    {
        public RoomViewModel(UIApplication application)
        {
            Load = new LoadRoomCommand(application, this);
        }

        public ICommand Load { get; }
        public ObservableCollection<BoundarySegmentPresenter> BoundarySegments { get; } = new ObservableCollection<BoundarySegmentPresenter>();
    }
}
