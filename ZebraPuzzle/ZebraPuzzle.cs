// https://exercism.org/tracks/csharp/exercises/zebra-puzzle/edit
// https://dev.to/tallesl/explaining-lazy-evaluation-in-c-81m3
// https://en.wikipedia.org/wiki/Backtracking

namespace ZebraPuzzle
{
    public static class ZebraPuzzle
    {
        private static Solution? solution;

        private static Nationality GetNationality(Func<Hypothesis, bool> func)
        {
            solution ??= new Solver().Solve();
            var first = solution.Hypotheses.FirstOrDefault(func);
            if (first == null)
                throw new ApplicationException("Hypotheses filled but condition 'func' does not match any");
            if (!first.Nationality.HasValue)
                throw new ApplicationException($"first.Nationality should not be null: {first.Position}: {first.Pet}");
            return first.Nationality.Value;
        }

        public static Nationality DrinksWater() => GetNationality(x => x.Drink == Drink.Water);

        public static Nationality OwnsZebra() => GetNationality(x => x.Pet == Pet.Zebra);
    }
}