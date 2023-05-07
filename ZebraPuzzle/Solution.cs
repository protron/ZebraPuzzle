namespace ZebraPuzzle
{
    public record Solution(int Level = 0)
    {
        public Hypothesis[] Hypotheses = Enumerable.Range(1, 5).Select(x => new Hypothesis { Position = x }).ToArray();

        // CombinationsEnumerators
        private IEnumerator<IList<Color>> colorsCE = Permutations.FromEnum<Color>().GetEnumerator();
        private IEnumerator<IList<Nationality>> natsCE = Permutations.FromEnum<Nationality>().GetEnumerator();
        private IEnumerator<IList<Pet>> petsCE = Permutations.FromEnum<Pet>().GetEnumerator();
        private IEnumerator<IList<Drink>> drinksCE = Permutations.FromEnum<Drink>().GetEnumerator();
        private IEnumerator<IList<Smoke>> smokesCE = Permutations.FromEnum<Smoke>().GetEnumerator();

        public Solution? Next<T>(IEnumerator<IList<T>> combinationsEnumerator, Func<Hypothesis, T, Hypothesis> setter)
        {
            if (!combinationsEnumerator.MoveNext())
                return null;
            var values = combinationsEnumerator.Current;
            return this with { Hypotheses = Hypotheses.Select((h, i) => setter(h, values[i])).ToArray() };
        }

        public Solution? NextSetOfColors() => Next(colorsCE, (h, v) => h with { Color = v });
        public Solution? NextSetOfNationalities() => Next(natsCE, (h, v) => h with { Nationality = v });
        public Solution? NextSetOfPets() => Next(petsCE, (h, v) => h with { Pet = v });
        public Solution? NextSetOfDrinks() => Next(drinksCE, (h, v) => h with { Drink = v });
        public Solution? NextSetOfSmokes() => Next(smokesCE, (h, v) => h with { Smoke = v });

        public bool ContradictsSomeRule()
        {
            if (AllRules.Direct.Any(r => Hypotheses.Any(h => r.Contradicts(h))))
                return true;
            if (AllRules.ColorPositions.Any(r => Hypotheses.Any(h => r.Contradicts(h))))
                return true;
            return false;
        }

        public bool MatchesPositions()
        {
            if (AllRules.ColorPositions.Any(r => !r.MatchesPositions(Hypotheses)))
                return false;
            if (AllRules.NextNationalityColors.Any(r => !r.MatchesPositions(Hypotheses)))
                return false;
            if (AllRules.NextSmokePets.Any(r => !r.MatchesPositions(Hypotheses)))
                return false;
            return true;
        }
    }
}