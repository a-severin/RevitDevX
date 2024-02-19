using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class SelectAllElementsCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly IRoomPresenter _vm;

        public SelectAllElementsCommand(UIApplication application, IRoomPresenter vm)
        {
            _application = application;
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (_vm.BoundarySegments.Any())
            {
                _application.ActiveUIDocument.Selection.SetElementIds(_vm.BoundarySegments.Select(_ => _.Element.Id).ToList());
            }
        }
    }
}
