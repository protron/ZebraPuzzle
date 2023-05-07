using MoreLinq;

namespace ZebraPuzzle
{
    public static class Permutations
    {
        private static T[] GetEnumValues<T>() => (T[])Enum.GetValues(typeof(T));

        public static IEnumerable<IList<T>> FromEnum<T>() => MoreEnumerable.Permutations(GetEnumValues<T>());
    }
}