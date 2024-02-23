using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RevitDevX.UI
{
    public class ConnectorsViewModel : BindableBase
    {
        public ConnectorsViewModel(UIApplication application)
        {
            Load = new LoadConnectorsCommand(application, this);
        }

        public ICommand Load { get; }
        public ObservableCollection<ConnectorPresenter> Connectors { get; } = new ObservableCollection<ConnectorPresenter>();
        public ObservableCollection<MepSystemPresenter> MepSystems { get; } = new ObservableCollection<MepSystemPresenter>();
        public ObservableCollection<MepSystemTypePresenter> MepSystemTypes { get; } = new ObservableCollection<MepSystemTypePresenter>();
    }

    public class LoadConnectorsCommand : ICommand
    {
        private readonly UIApplication _application;
        private readonly ConnectorsViewModel _vm;

        public LoadConnectorsCommand(UIApplication application, ConnectorsViewModel vm)
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
                var document = _application.ActiveUIDocument.Document;

                _vm.MepSystems.Clear();
                var mepSystems = new FilteredElementCollector(document)
                    .OfClass(typeof(MEPSystem))
                    .ToElements()
                    .OfType<MEPSystem>()
                    .OrderBy(_ => _.Name)
                    .ThenBy(_ => _.GetType().Name);
                foreach (var mepSystem in mepSystems)
                {
                    _vm.MepSystems.Add(new MepSystemPresenter(mepSystem));
                }

                _vm.MepSystemTypes.Clear();

                var mepSystemTypes = new FilteredElementCollector(document)
                    .OfClass(typeof(MEPSystemType))
                    .ToElements()
                    .OfType<MEPSystemType>()
                    .OrderBy(_ => _.Name)
                    .ThenBy(_ => _.GetType().Name);
                foreach (var mepSystemType in mepSystemTypes)
                {
                    _vm.MepSystemTypes.Add(new MepSystemTypePresenter(mepSystemType));
                }

                _vm.Connectors.Clear();

                var elementId = _application.ActiveUIDocument.Selection.GetElementIds().FirstOrDefault();
                if (elementId == null)
                {
                    return;
                }

                var element = _application.ActiveUIDocument.Document.GetElement(elementId);
                if (element is FamilyInstance fi)
                {
                    if (fi.MEPModel?.ConnectorManager?.Connectors != null)
                    {
                        var connetors = fi.MEPModel.ConnectorManager
                            .Connectors
                            .OfType<Connector>()
                            .OrderBy(_ => _.Id);
                        foreach (var connector in connetors)
                        {
                            _vm.Connectors.Add(new ConnectorPresenter(connector));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, an error has occurred:\n{ex}", "RevitDevX", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class MepSystemPresenter
    {
        public MepSystemPresenter(MEPSystem s)
        {
            Properties.Add(new Property { Name = "Id", Value = s.Id.IntegerValue.ToString() });
            Properties.Add(new Property { Name = "Name", Value = s.Name });
            Properties.Add(new Property { Name = "Type", Value = s.GetType().Name });
            Properties.Add(new Property { Name = "MEPSystemTypeId", Value = s.GetTypeId()?.IntegerValue.ToString() });
        }

        public List<Property> Properties { get; } = new List<Property>();
    }

    public class MepSystemTypePresenter
    {
        public MepSystemTypePresenter(MEPSystemType st)
        {
            Properties.Add(new Property { Name = "Id", Value = st.Id.IntegerValue.ToString() });
            Properties.Add(new Property { Name = "Name", Value = st.Name });
            Properties.Add(new Property { Name = "Type", Value = st.GetType().Name });
        }

        public List<Property> Properties { get; } = new List<Property>();
    }

    public class ConnectorPresenter
    {
        public ConnectorPresenter(Connector c)
        {
            Id = c.Id;
            try
            {
                Properties.Add(new Property
                {
                    Name = "ConnectorType",
                    Value = c.ConnectorType.ToString()
                }
                );
            }
            catch { }
            try
            {
                Properties.Add(new Property
                {
                    Name = "Domain",
                    Value = c.Domain.ToString()
                }
                );
            }
            catch { }
            try
            {
                Properties.Add(new Property
                {
                    Name = "DuctSystemType",
                    Value = c.DuctSystemType.ToString()
                }
                );
            }
            catch { }
            try
            {
                Properties.Add(new Property
                {
                    Name = "ElectricalSystemType",
                    Value = c.ElectricalSystemType.ToString()
                }
                );
            }
            catch { }
            try
            {
                Properties.Add(new Property
                {
                    Name = "MEPSystem",
                    Value = $"[{c.MEPSystem.Id.IntegerValue}]: {c.MEPSystem.Name}"
                }
                );
            }
            catch { }
            try
            {
                Properties.Add(new Property
                {
                    Name = "PipeSystemType",
                    Value = c.PipeSystemType.ToString()
                }
                );
            }
            catch { }
            try
            {
                Properties.Add(new Property
                {
                    Name = "X",
                    Value = c.Origin.X.ToMillimeters().ToString()
                }
                );
                Properties.Add(new Property
                {
                    Name = "Y",
                    Value = c.Origin.Y.ToMillimeters().ToString()
                }
                );
                Properties.Add(new Property
                {
                    Name = "Z",
                    Value = c.Origin.Z.ToMillimeters().ToString()
                }
                );
            } catch { }
        }

        public int Id { get; }

        public List<Property> Properties { get; } = new List<Property>();
    }
}
