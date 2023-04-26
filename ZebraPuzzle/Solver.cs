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

        public class Solution
        {
            private Hypothesis[] hypothesis = Enumerable.Range(1, 5).Select(x => new Hypothesis { Position = x }).ToArray();

            internal Solution() {}

            public bool ContradictsSomeRule()
            {
                if (AllRules.Direct.Any(r => hypothesis.Any(h => r.Contradicts(h))))
                    return true;
                if (AllRules.ColorPositions.Any(r => hypothesis.Any(h => r.Contradicts(h))))
                    return true;
                return false;
            }

            public bool MatchesPositions()
            {
                if (AllRules.ColorPositions.Any(r => !r.MatchesPositions(hypothesis)))
                    return false;
                if (AllRules.NextNationalityColors.Any(r => !r.MatchesPositions(hypothesis)))
                    return false;
                if (AllRules.NextSmokePets.Any(r => !r.MatchesPositions(hypothesis)))
                    return false;
                return true;
            }

            public Nationality GetNationality(Func<Hypothesis, bool> func)
            {
                var first = hypothesis.First(func);
                if (!first.Nationality.HasValue)
                    throw new Exception($"first.Nationality should not be null: {first.Position}: {first.Pet}");
                return first.Nationality.Value;
            }
        }
    }
}