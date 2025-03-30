namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable
    {
        public enum UnitOfMeasure
        {
            millimeter,
            centimeter,
            meter
        }

        private double a;
        private double b;
        private double c;

        public double A => a;
        public double B => b;
        public double C => c;

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (unit == UnitOfMeasure.meter)
            {
                a = ConvertToMeters(a, unit);
                b = ConvertToMeters(b, unit);
                c = ConvertToMeters(c, unit);
            }
            else if (unit == UnitOfMeasure.centimeter)
            {
                a = ConvertToMeters(a, unit);
                b = ConvertToMeters(b, unit);
                c = ConvertToMeters(c, unit);
            }
            else if (unit == UnitOfMeasure.millimeter)
            {
                a = ConvertToMeters(a, unit);
                b = ConvertToMeters(b, unit);
                c = ConvertToMeters(c, unit);
            }

            if (a <= 0 || b <= 0 || c <= 0 || a > 10 || b > 10 || c > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.a = a;
            this.b = b;
            this.c = c;
        }
        
        private double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.millimeter:
                    return value / 1000;
                case UnitOfMeasure.centimeter:
                    return value / 100;
                case UnitOfMeasure.meter:
                    return value;
                default:
                    throw new FormatException();
            }
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "cm")
            {
                return $"{a * 100} cm × {b * 100} cm × {c * 100} cm";
            }
            else if (format == "mm")
            {
                return $"{a * 1000} mm × {b * 1000} mm × {c * 1000} mm";
            }
            else
            {
                return $"{a} m × {b} m × {c} m";
            }
        }
    }
}
