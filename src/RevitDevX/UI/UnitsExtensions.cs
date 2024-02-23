using Autodesk.Revit.DB;
using System;

namespace RevitDevX.UI
{
    public static class UnitsExtensions
    {
        public static double ToMillimeters(this double internalValue)
        {
            return UnitUtils.ConvertFromInternalUnits(internalValue, UnitTypeId.Millimeters);
        }

        public static double ToInternalUnits(this double millimeters)
        {
            return UnitUtils.ConvertToInternalUnits(millimeters, UnitTypeId.Millimeters);
        }

        public static bool AlmostEqualTo(this double value1, double value2)
        {
            return Math.Abs(value1.ToMillimeters() - value2.ToMillimeters()) < 0.001;
        }
    }
}
