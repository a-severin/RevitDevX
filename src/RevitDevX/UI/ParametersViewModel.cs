using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class ParametersViewModel: BindableBase
    {
        public ParametersViewModel(UIApplication application)
        {
        }

        public ObservableCollection<Parameter> Parameters { get; } = new ObservableCollection<Parameter>();
    }
}
