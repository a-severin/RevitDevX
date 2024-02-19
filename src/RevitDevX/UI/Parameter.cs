using Autodesk.Revit.DB;

namespace RevitDevX.UI
{
    public class Parameter
    {
        public Parameter(Autodesk.Revit.DB.Parameter p)
        {
            Name = p.Definition.Name;
            ParameterGroup = p.Definition.ParameterGroup.ToString();
            BuiltInParameter = (p.Definition as InternalDefinition)?.BuiltInParameter.ToString();
            StorageType = p.StorageType.ToString();
            Value = p.AsValueString();
            Visible = (p.Definition as InternalDefinition)?.Visible;
        }

        public string Name { get; }
        public string ParameterGroup { get; }
        public string BuiltInParameter { get; }
        public string StorageType { get; }
        public string Value { get; }
        public bool? Visible { get; }
    }
}
