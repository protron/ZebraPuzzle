namespace ZebraPuzzle
{
    public abstract class BaseBacktracker<T> where T : class
    {
        abstract protected bool IsRejected(T solution);
        abstract protected bool IsAccepted(T solution);
        abstract protected T? GetFirst(T solution);
        abstract protected T? GetNext(T solution);

        public T? Recurse(T solution)
        {
            // start recursion with a visited set to avoid cycles / infinite loops
            var visited = new HashSet<T>(EqualityComparer<T>.Default);
            return Recurse(solution, visited);
        }

        private T? Recurse(T solution, HashSet<T> visited)
        {
            if (!visited.Add(solution))
                return null;

            if (IsRejected(solution))
                return null;
            if (IsAccepted(solution))
                return solution;

            var newSolution = GetFirst(solution);
            while (newSolution != null)
            {
                var triedSolution = Recurse(newSolution, visited);
                if (triedSolution != null)
                    return triedSolution;
                else
                    newSolution = GetNext(newSolution);
            }
            return null;
        }
    }
}