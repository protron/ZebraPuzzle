namespace ZebraPuzzle
{
    public class Solver
    {
        private bool IsRejected(Solution solution) => solution.ContradictsSomeRule();

        private bool IsAccepted(Solution solution) => solution.MatchesPositions();

        private Solution GetFirst(Solution solution)
        {
            /*
            if (AllRules.NextNationalityColors.FirstOrDefault(x => x.Color == solution.Color) is NextNationalityColorFact nColor)
            {
                return Solve(nColor.Nationality);
            }
            if (AllRules.NextNationalityColors.FirstOrDefault(x => x.Nationality == solution.Nationality) is NextNationalityColorFact nNat)
            {
                return Solve(nNat.Color);
            }
            switch (solution.GetMissingFacts()) {
                case 1:
                    var colors = Permutations.FromEnum<Color>();
                    return Facts.Colors[0];
            }
            */
            return solution;
        }

        private Solution GetNext(Solution solution)
        {
            /*
            var i = ++solution.TryIndex;
            switch (solution.GetMissingFacts())
            {
                case 1: return Facts.Colors[i];
            }*/
            return solution;
        }

        private IEnumerable<Solution> Backtrack(Solution solution) {
            if (IsRejected(solution)) { yield break; }
            if (IsAccepted(solution)) { yield return solution; }
            var newSolution = GetFirst(solution);
            List<Solution> finalSolutions = new List<Solution>();
            while (newSolution != null) {
                finalSolutions.AddRange(Backtrack(newSolution));
                newSolution = GetNext(newSolution);
            }
            foreach (var finalSolution in finalSolutions) {
                yield return finalSolution;
            }
        }

        private Solution solution = new Solution();

        public Solution Solve() {
            solution = Backtrack(solution).Single();
            return solution;
        }
    }
}