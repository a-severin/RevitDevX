using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class SelectElementCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly Element _element;

        public SelectElementCommand(UIApplication application, Element element)
        {
            _application = application;
            _element = element;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            _application.ActiveUIDocument.Selection.SetElementIds(new[] { _element.Id });
        }
    }
}
