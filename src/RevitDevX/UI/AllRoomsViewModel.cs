using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class AllRoomsViewModel : BindableBase
    {
        private RoomPresenter _selectedRoom;

        public AllRoomsViewModel(UIApplication application)
        {
            Load = new LoadAllRoomsCommand(application, this);
        }
        public ICommand Load { get; }
        public SpatialElementBoundaryLocation SpatialElementBoundaryLocation { get; set; }
        public Array SpatialElementBoundaryLocations { get; } = Enum.GetValues(typeof(SpatialElementBoundaryLocation));
        public bool StoreFreeBoundaryFaces { get; set; }

        public ObservableCollection<LevelPresenter> Levels { get; } = new ObservableCollection<LevelPresenter>();
        public RoomPresenter SelectedRoom {
            get => _selectedRoom;
            set => Set(ref _selectedRoom, value);
        }
    }
}
