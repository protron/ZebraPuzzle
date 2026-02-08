namespace ZebraPuzzle
{
    public abstract class BaseBacktracker<T> where T : class
    {
        abstract protected bool IsRejected(T solution);
        abstract protected bool IsAccepted(T solution);
        abstract protected T? GetFirst(T solution);
        abstract protected T? GetNext(T solution);
        abstract protected string GetStateKey(T solution);

        public T? Recurse(T solution)
        {
            var visited = new HashSet<string>();
            return Recurse(solution, visited);
        }

        private T? Recurse(T solution, HashSet<string> visited)
        {
            var stateKey = GetStateKey(solution);
            if (!visited.Add(stateKey))
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