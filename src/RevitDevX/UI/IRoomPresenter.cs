using System.Collections.ObjectModel;

namespace RevitDevX.UI
{
    public interface IRoomPresenter
    {
        ObservableCollection<BoundarySegmentPresenter> BoundarySegments { get; }
    }
}
