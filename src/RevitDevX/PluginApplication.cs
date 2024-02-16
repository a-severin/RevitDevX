using Autodesk.Revit.UI;
using RevitDevX.Resources;
using RevitDevX.RevitCommands;
using System;
using System.Windows;
using System.Windows.Media;

namespace RevitDevX
{
    public class PluginApplication : PluginBase
    {
        public override Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab("RevitDevX");

            var panel = application.CreateRibbonPanel("RevitDevX", "RevitDevX_Browser");
            panel.Title = "Browser";

            var assemblyPath = GetType().Assembly.Location;

            var iconsResourceDictionary = new IconsDictionary();
            var browserIconDrawingImage = iconsResourceDictionary["BrowserIcon"] as DrawingImage;

            var browserButton = new PushButtonData("browserButton", "Browser", assemblyPath,
                typeof(BrowserRevitCommand).FullName)
            {
                LargeImage = ToBitmapSource(browserIconDrawingImage),
                Image = ToBitmapSource(browserIconDrawingImage)
            };
            panel.AddItem(browserButton);

            return Result.Succeeded;
        }
    }
}
