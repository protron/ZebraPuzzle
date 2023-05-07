namespace ZebraPuzzle
{
    public class Solver
    {
        private bool IsRejected(Solution solution) => solution.ContradictsSomeRule();

        private bool IsAccepted(Solution solution) => solution.Level == 5 && solution.MatchesPositions();

        private Solution GetFirst(Solution solution)
        {
            var newSolution = solution with { Level = solution.Level + 1 };
            return GetNext(newSolution)!;
        }

        private Solution? GetNext(Solution solution)
        {
            return solution.Level switch
            {
                1 => solution.NextSetOfColors(),
                2 => solution.NextSetOfNationalities(),
                3 => solution.NextSetOfPets(),
                4 => solution.NextSetOfDrinks(),
                5 => solution.NextSetOfSmokes(),
                _ => throw new IndexOutOfRangeException(),
            };
        }

        private Solution? Backtrack(Solution solution) {
            if (IsRejected(solution))
            {
                return null;
            }
            if (IsAccepted(solution))
            {
                return solution;
            }
            var newSolution = GetFirst(solution);
            while (newSolution != null) {
                var triedSolution = Backtrack(newSolution);
                if (triedSolution != null)
                {
                    return triedSolution;
                }
                else
                {
                    newSolution = GetNext(newSolution);
                }
            }
            return null;
        }

        public Solution Solve() {
            Solution? solution = Backtrack(new Solution());
            if (solution == null)
                throw new ApplicationException("Solution not found");
            if (solution.Level < 5)
                throw new ApplicationException($"Level should be 5 but was {solution.Level}!");
            return solution!;
        }
    }
}