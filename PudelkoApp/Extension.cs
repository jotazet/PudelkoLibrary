using PudelkoLibrary;

namespace PudelkoApp
{
    internal static class Extension
    {
        public static Pudelko Kompresuj(Pudelko p)
        {
            double a = Math.Pow(p.Objetosc, 1.0 / 3.0);

            return new Pudelko(a, a, a, Pudelko.UnitOfMeasure.meter);
        }
    }
}