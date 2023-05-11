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
            if (IsRejected(solution))
                return null;
            if (IsAccepted(solution))
                return solution;
            var newSolution = GetFirst(solution);
            while (newSolution != null)
            {
                var triedSolution = Recurse(newSolution);
                if (triedSolution != null)
                    return triedSolution;
                else
                    newSolution = GetNext(newSolution);
            }
            return null;
        }
    }
}