namespace ZebraPuzzle
{
    public record Solution
    {
        private Hypothesis[] hypothesis = Enumerable.Range(1, 5).Select(x => new Hypothesis { Position = x }).ToArray();

        internal Solution() {}

        public bool ContradictsSomeRule()
        {
            if (AllRules.Direct.Any(r => hypothesis.Any(h => r.Contradicts(h))))
                return true;
            if (AllRules.ColorPositions.Any(r => hypothesis.Any(h => r.Contradicts(h))))
                return true;
            return false;
        }

        public bool MatchesPositions()
        {
            if (AllRules.ColorPositions.Any(r => !r.MatchesPositions(hypothesis)))
                return false;
            if (AllRules.NextNationalityColors.Any(r => !r.MatchesPositions(hypothesis)))
                return false;
            if (AllRules.NextSmokePets.Any(r => !r.MatchesPositions(hypothesis)))
                return false;
            return true;
        }

        public Nationality GetNationality(Func<Hypothesis, bool> func)
        {
            var first = hypothesis.First(func);
            if (!first.Nationality.HasValue)
                throw new Exception($"first.Nationality should not be null: {first.Position}: {first.Pet}");
            return first.Nationality.Value;
        }
    }
}