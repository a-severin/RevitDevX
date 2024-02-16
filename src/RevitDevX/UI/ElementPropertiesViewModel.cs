using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class ElementPropertiesViewModel: BindableBase
    {
        public ElementPropertiesViewModel(UIApplication application)
        {
            Load = new LoadCommand(application, ElementProperties);
        }

        public ObservableCollection<ElementProperty> ElementProperties { get; } = new ObservableCollection<ElementProperty>();

        public ICommand Load { get; }
    }

    public class ElementProperty
    {
        public ElementProperty(Parameter p)
        {
            Name = p.Definition.Name;
            BuiltInParameter = (p.Definition as InternalDefinition)?.BuiltInParameter.ToString();
            Value = p.AsValueString();
        }

        public string Name { get; }
        public string BuiltInParameter { get; }
        public string Value { get; }
    }

    public class LoadCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly ICollection<ElementProperty> _collection;

        public LoadCommand(UIApplication application, ICollection<ElementProperty> collection)
        {
            _application = application;
            _collection = collection;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            _collection.Clear();
            var elementId = _application.ActiveUIDocument.Selection.GetElementIds().FirstOrDefault();
            if (elementId == null)
            {
                return;
            }
            var element = _application.ActiveUIDocument.Document.GetElement(elementId);
            foreach(Parameter p in element.Parameters)
            {
                _collection.Add(new ElementProperty(p));
            }
        }
    }
}
