namespace ZebraPuzzle
{
    public static class AllPermutations
    {
        public static IList<IList<Color>> ColorsSets = Permutations.FromEnum<Color>().ToArray();
        public static IList<IList<Nationality>> NationalitiesSets = Permutations.FromEnum<Nationality>().ToArray();
        public static IList<IList<Pet>> PetsSets = Permutations.FromEnum<Pet>().ToArray();
        public static IList<IList<Drink>> DrinksSets = Permutations.FromEnum<Drink>().ToArray();
        public static IList<IList<Smoke>> SmokesSets = Permutations.FromEnum<Smoke>().ToArray();

        public static int PermutationsCount = 
            ColorsSets.Count == NationalitiesSets.Count &&
            ColorsSets.Count == PetsSets.Count &&
            ColorsSets.Count == DrinksSets.Count &&
            ColorsSets.Count == SmokesSets.Count
            ? ColorsSets.Count : throw new InvalidOperationException("All sets should match in size");
    }
}