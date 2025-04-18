using System;
using PudelkoLibrary;

namespace PudelkoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example usage of Pudelko class
            try
            {
                var pudelko = new PudelkoLibrary.Pudelko(1.2, 0.5, 0.3);
                Console.WriteLine(pudelko.ToString("m", null));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}