// https://exercism.org/tracks/csharp/exercises/zebra-puzzle/edit
// https://dev.to/tallesl/explaining-lazy-evaluation-in-c-81m3
// https://en.wikipedia.org/wiki/Backtracking

namespace ZebraPuzzle
{
    public static class ZebraPuzzle
    {
        private static Solution solution = new Solver().Solve();

        public static Nationality DrinksWater()
        {
            return solution.GetNationality(x => x.Drink == Drink.Water);
        }

        public static Nationality OwnsZebra()
        {
            return solution.GetNationality(x => x.Pet == Pet.Zebra);
        }
    }
}