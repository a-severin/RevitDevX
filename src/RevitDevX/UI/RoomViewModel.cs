using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class RoomViewModel : BindableBase
    {
        public RoomViewModel(UIApplication application)
        {
            Load = new LoadRoomCommand(application, this);
            SelectAll = new SelectAllElementsCommand(application, this);
        }

        public ObservableCollection<BoundarySegmentPresenter> BoundarySegments { get; } = new ObservableCollection<BoundarySegmentPresenter>();
        public ICommand Load { get; }
        public ICommand SelectAll { get; }
        public SpatialElementBoundaryLocation SpatialElementBoundaryLocation { get; set; }
        public Array SpatialElementBoundaryLocations { get; } = Enum.GetValues(typeof(SpatialElementBoundaryLocation));
        public bool StoreFreeBoundaryFaces { get; set; }
    }
}
