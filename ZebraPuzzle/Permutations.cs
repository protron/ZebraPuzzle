namespace ZebraPuzzle
{
    public static class Permutations
    {
        private static IEnumerable<IEnumerable<T>> FromList<T>(T[] list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return FromList(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static T[][] FromList<T>(T[] list) => FromList(list, list.Length).Select(x => x.ToArray()).ToArray();

        public static T[][] FromEnum<T>() => FromList((T[])Enum.GetValues(typeof(T)));
    }
}