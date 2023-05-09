namespace ZebraPuzzle
{
    public static class AllPermutations
    {
        public static IList<IList<Color>> ColorsSets = Permutations.FromEnum<Color>().ToArray();
        public static IList<IList<Nationality>> NationalitiesSets = Permutations.FromEnum<Nationality>().ToArray();
        public static IList<IList<Pet>> PetsSets = Permutations.FromEnum<Pet>().ToArray();
        public static IList<IList<Drink>> DrinksSets = Permutations.FromEnum<Drink>().ToArray();
        public static IList<IList<Smoke>> SmokesSets = Permutations.FromEnum<Smoke>().ToArray();
    }
}