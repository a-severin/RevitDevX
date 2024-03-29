﻿using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class LoadElementCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly ElementViewModel _vm;

        public LoadElementCommand(UIApplication application, ElementViewModel vm)
        {
            _application = application;
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            try
            {

                _vm.Properties.Clear();
                _vm.ElementParameters.Parameters.Clear();
                _vm.SymbolParameters.Parameters.Clear();

                var elementId = _application.ActiveUIDocument.Selection.GetElementIds().FirstOrDefault();
                if (elementId == null)
                {
                    return;
                }

                var element = _application.ActiveUIDocument.Document.GetElement(elementId);


                _vm.Properties.Add(new Property { Name = nameof(element.Id), Value = element.Id.IntegerValue.ToString() });
                _vm.Properties.Add(new Property { Name = nameof(element.UniqueId), Value = element.UniqueId });
                _vm.Properties.Add(new Property { Name = nameof(element.Name), Value = element.Name });


                foreach (var p in element.Parameters.OfType<Autodesk.Revit.DB.Parameter>().OrderBy(_ => _.Definition.Name))
                {
                    _vm.ElementParameters.Parameters.Add(new Parameter(p));
                }

                if (element is FamilyInstance fi)
                {
                    _vm.Properties.Add(new Property { Name = "Category Id", Value = fi.Symbol.Family.FamilyCategoryId.IntegerValue.ToString() });
                    _vm.Properties.Add(new Property { Name = "Category Name", Value = fi.Symbol.Family.FamilyCategory.Name });
                    _vm.Properties.Add(new Property { Name = "Family Id", Value = fi.Symbol.Family.Id.IntegerValue.ToString() });
                    _vm.Properties.Add(new Property { Name = "Family Name", Value = fi.Symbol.Family.Name });
                    _vm.Properties.Add(new Property { Name = "Symbol Id", Value = fi.Symbol.Id.IntegerValue.ToString() });
                    _vm.Properties.Add(new Property { Name = "Symbol Name", Value = fi.Symbol.Name });
                    _vm.Properties.Add(new Property { Name = "Room Id", Value = fi.Room?.Id.IntegerValue.ToString() });
                    _vm.Properties.Add(new Property { Name = "Room Name", Value = fi.Room?.Name });


                    foreach (Autodesk.Revit.DB.Parameter p in fi.Symbol.Parameters.OfType<Autodesk.Revit.DB.Parameter>().OrderBy(_ => _.Definition.Name))
                    {
                        _vm.SymbolParameters.Parameters.Add(new Parameter(p));
                    }

                    var ge = fi.Symbol.get_Geometry(new Options());
                    if (ge != null)
                    {
                        var bb = ge.GetBoundingBox();
                        var bbt = $"[{bb.Min.X:F2};{bb.Min.Y:F2};{bb.Min.Z:F2}] - [{bb.Max.X:F2};{bb.Max.Y:F2};{bb.Max.Z:F2}]";
                        _vm.Properties.Add(new Property { Name = "Symbol Bounding Box", Value = bbt });
                    }
                }
                else
                {
                    var symbolIdParameter = element.get_Parameter(BuiltInParameter.SYMBOL_ID_PARAM);
                    if (symbolIdParameter != null)
                    {
                        var symbolId = symbolIdParameter.AsElementId();
                        var symbol = _application.ActiveUIDocument.Document.GetElement(symbolId);
                        if (symbol != null)
                        {
                            foreach (Autodesk.Revit.DB.Parameter p in symbol.Parameters.OfType<Autodesk.Revit.DB.Parameter>().OrderBy(_ => _.Definition?.Name))
                            {
                                _vm.SymbolParameters.Parameters.Add(new Parameter(p));
                            }

                            var ge = symbol.get_Geometry(new Options());
                            if (ge != null)
                            {
                                var bb = ge.GetBoundingBox();
                                var bbt = $"[{bb.Min.X:F2};{bb.Min.Y:F2};{bb.Min.Z:F2}] - [{bb.Max.X:F2};{bb.Max.Y:F2};{bb.Max.Z:F2}]";
                                _vm.Properties.Add(new Property { Name = "Symbol Bounding Box", Value = bbt });
                            }
                        }
                    }
                }

                {
                    var ge = element.get_Geometry(new Options());
                    if (ge != null)
                    {
                        var bb = ge.GetBoundingBox();
                        var bbt = $"[{bb.Min.X:F2};{bb.Min.Y:F2};{bb.Min.Z:F2}] - [{bb.Max.X:F2};{bb.Max.Y:F2};{bb.Max.Z:F2}]";
                        _vm.Properties.Add(new Property { Name = "Bounding Box", Value = bbt });
                    }
                }

                _vm.Properties.Add(new Property { Name = nameof(element.LevelId), Value = element.LevelId?.IntegerValue.ToString() });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, an error has occurred:\n{ex}", "RevitDevX", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
