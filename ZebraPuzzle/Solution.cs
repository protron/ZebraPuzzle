namespace ZebraPuzzle
{
    public record Solution
    {
        private record PermutationIndices(
            int ColorIndex = 0,
            int NationalityIndex = 0,
            int PetIndex = 0,
            int DrinkIndex = 0,
            int SmokeIndex = 0);

        public static Solution BuildInitial() => new Solution();

        public int Level { get; init; } = 0;
        private PermutationIndices PIndices = new PermutationIndices();
        private Hypothesis H1 = new(1);
        private Hypothesis H2 = new(2);
        private Hypothesis H3 = new(3);
        private Hypothesis H4 = new(4);
        private Hypothesis H5 = new(5);

        public Hypothesis[] Hypotheses => new[] { H1, H2, H3, H4, H5 };

        private Solution? Set(int hIndex, int pIndex)
        {
            if (pIndex >= AllPermutations.PermutationsCount)
                return null;
            var hypothesisSetter = hypothesisSetters[hIndex];
            var permutationSetter = permutationSetters[hIndex];
            return this with {
                H1 = hypothesisSetter(H1, 0, pIndex),
                H2 = hypothesisSetter(H2, 1, pIndex),
                H3 = hypothesisSetter(H3, 2, pIndex),
                H4 = hypothesisSetter(H4, 3, pIndex),
                H5 = hypothesisSetter(H5, 4, pIndex),
                PIndices = permutationSetter(PIndices, pIndex)
            };
        }

        private Solution Init(int hIndex) => Set(hIndex, 0)!;

        private Solution? Next(int hIndex) => Set(hIndex, permutationGetters[hIndex](PIndices) + 1);

        private static readonly Func<PermutationIndices, int>[] permutationGetters =
            new Func<PermutationIndices, int>[] {
                pIndices => pIndices.ColorIndex,
                pIndices => pIndices.NationalityIndex,
                pIndices => pIndices.PetIndex,
                pIndices => pIndices.DrinkIndex,
                pIndices => pIndices.SmokeIndex,
            };

        private static readonly Func<PermutationIndices, int, PermutationIndices>[] permutationSetters =
            new Func<PermutationIndices, int, PermutationIndices>[] {
                (pIndices, pIndex) => pIndices with { ColorIndex = pIndex },
                (pIndices, pIndex) => pIndices with { NationalityIndex = pIndex },
                (pIndices, pIndex) => pIndices with { PetIndex = pIndex },
                (pIndices, pIndex) => pIndices with { DrinkIndex = pIndex },
                (pIndices, pIndex) => pIndices with { SmokeIndex = pIndex },
            };

        private static readonly Func<Hypothesis, int, int, Hypothesis>[] hypothesisSetters =
            new Func<Hypothesis, int, int, Hypothesis>[] {
                (Hypothesis h, int hIndex, int pIndex) => h with { Color = AllPermutations.ColorsSets[pIndex][hIndex] },
                (Hypothesis h, int hIndex, int pIndex) => h with { Nationality = AllPermutations.NationalitiesSets[pIndex][hIndex] },
                (Hypothesis h, int hIndex, int pIndex) => h with { Pet = AllPermutations.PetsSets[pIndex][hIndex] },
                (Hypothesis h, int hIndex, int pIndex) => h with { Drink = AllPermutations.DrinksSets[pIndex][hIndex] },
                (Hypothesis h, int hIndex, int pIndex) => h with { Smoke = AllPermutations.SmokesSets[pIndex][hIndex] },
            };

        public Solution InitColors() => Init(0);
        public Solution InitNationalities() => Init(1);
        public Solution InitPets() => Init(2);
        public Solution InitDrinks() => Init(3);
        public Solution InitSmokes() => Init(4);

        public Solution? NextSetOfColors() => Next(0);
        public Solution? NextSetOfNationalities() => Next(1);
        public Solution? NextSetOfPets() => Next(2);
        public Solution? NextSetOfDrinks() => Next(3);
        public Solution? NextSetOfSmokes() => Next(4);
    }
}