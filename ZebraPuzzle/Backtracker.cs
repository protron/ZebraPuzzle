namespace ZebraPuzzle
{
    public class Backtracker<T> where T : class
    {
        public interface IFinder
        {
            bool IsRejected(T solution);

            bool IsAccepted(T solution);

            T? GetFirst(T solution);

            T? GetNext(T solution);
        }

        private readonly IFinder finder;

        public Backtracker(IFinder finder)
        {
            this.finder = finder;
        }

        public T? Recurse(T solution)
        {
            if (finder.IsRejected(solution))
                return null;
            if (finder.IsAccepted(solution))
                return solution;
            var newSolution = finder.GetFirst(solution);
            while (newSolution != null)
            {
                var triedSolution = Recurse(newSolution);
                if (triedSolution != null)
                    return triedSolution;
                else
                    newSolution = finder.GetNext(newSolution);
            }
            return null;
        }
    }
}