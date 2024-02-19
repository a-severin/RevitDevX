using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace RevitDevX.UI
{
    public class LevelPresenter
    {
        public LevelPresenter(Level level)
        {
            Name = level.Name;
        }
        public string Name { get; set; }
        public List<RoomPresenter> Rooms { get; } = new List<RoomPresenter>();
    }
}
