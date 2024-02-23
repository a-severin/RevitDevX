using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class RoomPresenter : IRoomPresenter
    {
        public RoomPresenter(
            UIApplication application,
            Room room,
            SpatialElementBoundaryLocation spatialElementBoundaryLocation,
            bool storeFreeBoundaryFaces
        )
        {
            Name = room.Name;
            SelectAll = new SelectAllElementsCommand(application, this);

            var segments = room.GetBoundarySegments(
                    new SpatialElementBoundaryOptions
                    {
                        SpatialElementBoundaryLocation = spatialElementBoundaryLocation,
                        StoreFreeBoundaryFaces = storeFreeBoundaryFaces
                    }
                );
            if (segments != null)
            {
                var index = 1;
                foreach (var segmentList in segments)
                {
                    foreach (var boundarySegment in segmentList)
                    {
                        var element = room.Document.GetElement(boundarySegment.ElementId);
                        if (element == null)
                        {
                            continue;
                        }
                        BoundarySegments.Add(new BoundarySegmentPresenter(application, element, index));
                    }
                    index++;
                }
            }
        }

        public string Name { get; set; }
        public ObservableCollection<BoundarySegmentPresenter> BoundarySegments { get; } = new ObservableCollection<BoundarySegmentPresenter>();
        public ICommand SelectAll { get; }

    }
}
