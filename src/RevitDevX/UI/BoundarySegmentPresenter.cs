﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class BoundarySegmentPresenter
    {
        public BoundarySegmentPresenter(UIApplication application, Element element)
        {
            Select = new SelectElementCommand(application, element);

            Properties.Add(new Property { Name = nameof(element.Id), Value = element.Id.IntegerValue.ToString() });
            Properties.Add(new Property { Name = nameof(element.UniqueId), Value = element.UniqueId });
            Properties.Add(new Property { Name = nameof(element.Name), Value = element.Name });
            Properties.Add(new Property { Name = "Type", Value = element.GetType().Name });
        }

        public ObservableCollection<Property> Properties { get; } = new ObservableCollection<Property>();
        public ICommand Select { get; }
    }
}
