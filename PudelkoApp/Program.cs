using PudelkoLibrary;

namespace PudelkoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> pudelka = new List<Pudelko>
            {
                new Pudelko(250, 310, 100, Pudelko.UnitOfMeasure.centimeter),
                new Pudelko(2500, 3100, 1000, Pudelko.UnitOfMeasure.milimeter),
                new Pudelko(3500, 3500, 3000, Pudelko.UnitOfMeasure.milimeter),
                new Pudelko(2.5, 3.1, 1.0, Pudelko.UnitOfMeasure.meter), 
                new Pudelko(1.0, 2.5),
                new Pudelko(3.0),
                new Pudelko()
            };

            Console.WriteLine("Nieposortowana lista pudełek:");
            foreach (var p in pudelka)
            {
                Console.WriteLine(p);
            }

            Comparison<Pudelko> comparison = (p1, p2) =>
            {
                int volumeComparison = p1.Objetosc.CompareTo(p2.Objetosc);
                if (volumeComparison != 0) return volumeComparison;

                int surfaceAreaComparison = p1.Pole.CompareTo(p2.Pole);
                if (surfaceAreaComparison != 0) return surfaceAreaComparison;

                double edgeSum1 = p1.A + p1.B + p1.C;
                double edgeSum2 = p2.A + p2.B + p2.C;
                return edgeSum1.CompareTo(edgeSum2);
            };

            pudelka.Sort(comparison);

            Console.WriteLine("\n\nPosortowana lista pudełek:");
            foreach (var p in pudelka)
            {
                Console.WriteLine(p);
            }
        }
    }
}
