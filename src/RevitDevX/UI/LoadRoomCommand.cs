using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class LoadRoomCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly RoomViewModel _vm;

        public LoadRoomCommand(UIApplication application, RoomViewModel vm)
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
                _vm.BoundarySegments.Clear();

                var elementId = _application.ActiveUIDocument.Selection.GetElementIds().FirstOrDefault();
                if (elementId == null)
                {
                    return;
                }

                var room = _application.ActiveUIDocument.Document.GetElement(elementId) as Room;

                if (room == null)
                {
                    return;
                }

                var segments = room.GetBoundarySegments(
                    new SpatialElementBoundaryOptions
                    {
                        SpatialElementBoundaryLocation = _vm.SpatialElementBoundaryLocation,
                        StoreFreeBoundaryFaces = _vm.StoreFreeBoundaryFaces
                    }
                );
                if (null != segments)
                {
                    foreach (var segmentList in segments)
                    {
                        foreach (var boundarySegment in segmentList)
                        {
                            var element = room.Document.GetElement(boundarySegment.ElementId);
                            if (element == null)
                            {
                                continue;
                            }
                            _vm.BoundarySegments.Add(new BoundarySegmentPresenter(_application, element));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, an error has occurred:\n{ex}", "RevitDevX", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
