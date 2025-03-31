using System.Globalization;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        public enum UnitOfMeasure
        {
            milimeter,
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
            else if (unit == UnitOfMeasure.milimeter)
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
                case UnitOfMeasure.milimeter:
                    return value / 1000;
                case UnitOfMeasure.centimeter:
                    return value / 100;
                case UnitOfMeasure.meter:
                    return value;
                default:
                    throw new FormatException();
            }
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format)) format = "m";
            formatProvider ??= CultureInfo.InvariantCulture;

            switch (format.ToLowerInvariant())
            {
                case "cm":
                    return string.Format(formatProvider, "{0:F1} cm × {1:F1} cm × {2:F1} cm", a * 100, b * 100, c * 100);
                case "mm":
                    return string.Format(formatProvider, "{0:F0} mm × {1:F0} mm × {2:F0} mm", a * 1000, b * 1000, c * 1000);
                case "m":
                    return string.Format(formatProvider, "{0:F3} m × {1:F3} m × {2:F3} m", a, b, c);
                default:
                    throw new FormatException();
            }
        }

        public double Objetosc => Math.Round(a * b * c, 9);
        public double Pole => Math.Round(2 * (a * b + b * c + c * a), 6);

        public bool Equals(Pudelko other)
        {
            if (other == null) return false;

            var dimensions = new List<double> { a, b, c };
            var otherDimensions = new List<double> { other.a, other.b, other.c };

            dimensions.Sort();
            otherDimensions.Sort();

            return dimensions.SequenceEqual(otherDimensions);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pudelko otherPudelko)
            {
                return Equals(otherPudelko);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var dimensions = new List<double> { a, b, c };
            dimensions.Sort();
            int hash = 17;
            foreach (var dimension in dimensions)
            {
                hash = hash * 31 + dimension.GetHashCode();
            }
            return hash;
        }

        public static bool operator ==(Pudelko left, Pudelko right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(Pudelko left, Pudelko right)
        {
            return !(left == right);
        }

        public static explicit operator double[](Pudelko p)
        {
            return new double[] { p.A, p.B, p.C };
        }

        public static implicit operator Pudelko((int a, int b, int c) dimensions)
        {
            return new Pudelko(dimensions.a, dimensions.b, dimensions.c, UnitOfMeasure.milimeter);
        }

        public double this[int index]
        {
            get
            {
                return index switch
                {
                    0 => a,
                    1 => b,
                    2 => c,
                    _ => throw new IndexOutOfRangeException()
                };
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return a;
            yield return b;
            yield return c;
        }
    }
}