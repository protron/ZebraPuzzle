using System.Diagnostics;

namespace ZebraPuzzle
{
    public class Program
    {
        public static void Main()
        {
            var sw = Stopwatch.StartNew();
            var drinksWater = ZebraPuzzle.DrinksWater();
            Console.WriteLine($"drinksWater={drinksWater}");
            var ownsZebra = ZebraPuzzle.OwnsZebra();
            Console.WriteLine($"ownsZebra={ownsZebra}");
            Console.WriteLine(sw.Elapsed);
        }
    }
}