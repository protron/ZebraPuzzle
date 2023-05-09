namespace ZebraPuzzle
{
    public record CombinationNumbers(int ColorIndex = 0, int NationalityIndex = 0, int PetIndex = 0, int DrinkIndex = 0, int SmokeIndex = 0);

    public record Solution(CombinationNumbers Numbers, int Level = 0)
    {
        public static Solution Init() => new Solution(new CombinationNumbers());

        public Hypothesis[] Hypotheses = Enumerable.Range(1, 5).Select(x => new Hypothesis { Position = x }).ToArray();

        private Solution? Set(int attIndex, int nextNumber)
        {
            if (nextNumber > 4)
                return null;
            return this with
            {
                Numbers = numbersSetters[attIndex](Numbers, nextNumber),
                Hypotheses = Hypotheses.Select((h, hIndex) => (hypothesisSetters[attIndex](h, hIndex, nextNumber))).ToArray()
            };
        }

        private Solution Init(int attIndex) => Set(attIndex, 0)!;

        private Solution? Next(int attIndex) => Set(attIndex, numberGetters[attIndex](Numbers) + 1);

        private readonly new Func<CombinationNumbers, int>[] numberGetters = new Func<CombinationNumbers, int>[] {
            (numbers) => numbers.ColorIndex,
            (numbers) => numbers.NationalityIndex,
            (numbers) => numbers.PetIndex,
            (numbers) => numbers.DrinkIndex,
            (numbers) => numbers.SmokeIndex,
        };

        private readonly Func<CombinationNumbers, int, CombinationNumbers>[] numbersSetters = new Func<CombinationNumbers, int, CombinationNumbers>[] {
            (numbers, i) => numbers with { ColorIndex = i },
            (numbers, i) => numbers with { NationalityIndex = i },
            (numbers, i) => numbers with { PetIndex = i },
            (numbers, i) => numbers with { DrinkIndex = i },
            (numbers, i) => numbers with { SmokeIndex = i },
        };

        private readonly Func<Hypothesis, int, int, Hypothesis>[] hypothesisSetters = new Func<Hypothesis, int, int, Hypothesis>[] {
            (Hypothesis h, int hIndex, int nextNumber) => h with { Color = AllPermutations.ColorsSets[nextNumber][hIndex] },
            (Hypothesis h, int hIndex, int nextNumber) => h with { Nationality = AllPermutations.NationalitiesSets[nextNumber][hIndex] },
            (Hypothesis h, int hIndex, int nextNumber) => h with { Pet = AllPermutations.PetsSets[nextNumber][hIndex] },
            (Hypothesis h, int hIndex, int nextNumber) => h with { Drink = AllPermutations.DrinksSets[nextNumber][hIndex] },
            (Hypothesis h, int hIndex, int nextNumber) => h with { Smoke = AllPermutations.SmokesSets[nextNumber][hIndex] },
        };

        public Solution InitColors() => Init(0);
        public Solution? NextSetOfColors() => Next(0);
        public Solution InitNationalities() => Init(1);
        public Solution? NextSetOfNationalities() => Next(1);
        public Solution InitPets() => Init(2);
        public Solution? NextSetOfPets() => Next(2);
        public Solution InitDrinks() => Init(3);
        public Solution? NextSetOfDrinks() => Next(3);
        public Solution InitSmokes() => Init(4);
        public Solution? NextSetOfSmokes() => Next(4);


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