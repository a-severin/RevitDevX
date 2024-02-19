using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class LoadAllRoomsCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly AllRoomsViewModel _vm;

        public LoadAllRoomsCommand(UIApplication application, AllRoomsViewModel vm)
        {
            _application = application;
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            try
            {
                _vm.Levels.Clear();

                var document = _application.ActiveUIDocument.Document;

                var levels = new FilteredElementCollector(document)
                    .OfClass(typeof(Level))
                    .OfType<Level>()
                    .OrderBy(_ => _.Elevation);
                var levelMap = new Dictionary<ElementId, LevelPresenter>();
                var levelCollection = new List<LevelPresenter>();
                foreach (var level in levels)
                {
                    var levelPresenter = new LevelPresenter(level);
                    levelMap[level.Id] = levelPresenter;
                    levelCollection.Add(levelPresenter);
                }

                var rooms = new FilteredElementCollector(document)
                    .OfClass(typeof(SpatialElement))
                    .OfType<Room>()
                    .OrderBy(_ => _.Number);
                foreach (var room in rooms)
                {
                    levelMap[room.LevelId].Rooms.Add(
                        new RoomPresenter(
                            _application,
                            room,
                            _vm.SpatialElementBoundaryLocation,
                            _vm.StoreFreeBoundaryFaces
                        )
                    );
                }

                foreach (var level in levelCollection)
                {
                    _vm.Levels.Add(level);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, an error has occurred:\n{ex}", "RevitDevX", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
