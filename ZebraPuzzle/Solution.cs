namespace ZebraPuzzle
{
    public record CombinationNumbers(
        int ColorIndex = 0,
        int NationalityIndex = 0,
        int PetIndex = 0,
        int DrinkIndex = 0,
        int SmokeIndex = 0);

    public record Solution(int Level, CombinationNumbers Numbers, Hypothesis H1, Hypothesis H2, Hypothesis H3, Hypothesis H4, Hypothesis H5)
    {
        public static Solution BuildInitial() => new Solution(0, new CombinationNumbers(), new(1), new(2), new(3), new(4), new(5));

        public readonly Hypothesis[] Hypotheses = new[] { H1, H2, H3, H4, H5 };

        private const int CombinationsPerProp = 120; // 5 ^ 5

        private Solution? Set(int attIndex, int nextNumber)
        {
            if (nextNumber >= CombinationsPerProp)
                return null;
            return new Solution(
                Level,
                numbersSetters[attIndex](Numbers, nextNumber),
                hypothesisSetters[attIndex](H1, 0, nextNumber),
                hypothesisSetters[attIndex](H2, 1, nextNumber),
                hypothesisSetters[attIndex](H3, 2, nextNumber),
                hypothesisSetters[attIndex](H4, 3, nextNumber),
                hypothesisSetters[attIndex](H5, 4, nextNumber));
        }

        private Solution Init(int attIndex) => Set(attIndex, 0)!;

        private Solution? Next(int attIndex) => Set(attIndex, numberGetters[attIndex](Numbers) + 1);

        private readonly Func<CombinationNumbers, int>[] numberGetters = new Func<CombinationNumbers, int>[] {
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
    }
}