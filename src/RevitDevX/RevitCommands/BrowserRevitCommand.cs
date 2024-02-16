using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDevX.UI;

namespace RevitDevX.RevitCommands
{
    [Transaction(TransactionMode.Manual)]
    public class BrowserRevitCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            new MainWindow(new MainViewModel(commandData.Application)).Show();

            return Result.Succeeded;
        }
    }
}
