﻿using System.Globalization;

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

        // Add nullable properties for a, b, c to check is user provide values
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.a = a.HasValue ? Math.Floor(ConvertToMeters(a.Value, unit) * 1000) / 1000 : 0.1;
            this.b = b.HasValue ? Math.Floor(ConvertToMeters(b.Value, unit) * 1000) / 1000 : 0.1;
            this.c = c.HasValue ? Math.Floor(ConvertToMeters(c.Value, unit) * 1000) / 1000 : 0.1;


            if (this.a < 0.001 || this.b < 0.001 || this.c < 0.001 || this.a > 10 || this.b > 10 || this.c > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
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

        public override string ToString()
        {
            return ToString("m", null);
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

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            return new Pudelko(
                Math.Max(p1.a, p2.a),
                Math.Max(p1.b, p2.b),
                Math.Max(p1.c, p2.c));
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