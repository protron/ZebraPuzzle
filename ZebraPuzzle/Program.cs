namespace ZebraPuzzle
{
    public class Program
    {
        public static void Main()
        {
            var drinksWater = ZebraPuzzle.DrinksWater();
            Console.WriteLine($"drinksWater={drinksWater}");
            var ownsZebra = ZebraPuzzle.OwnsZebra();
            Console.WriteLine($"ownsZebra={ownsZebra}");
        }
    }
}