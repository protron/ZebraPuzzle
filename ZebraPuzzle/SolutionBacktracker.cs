namespace ZebraPuzzle
{
    public sealed class SolutionBacktracker : BaseBacktracker<Solution>
    {
        protected override bool IsRejected(Solution solution)
        {
            if (ContradictsAny(AllRules.Direct))
                return true;
            if (ContradictsAny(AllRules.ColorPositions))
                return true;
            return false;

            bool ContradictsAny(IContradictableRule[] contradictableRules) =>
                contradictableRules.Any(r => solution.Hypotheses.Any(h => r.Contradicts(h)));
        }

        protected override bool IsAccepted(Solution solution)
        {
            if (solution.Level != 5)
                return false;
            if (!MatchesAll(AllRules.ColorPositions))
                return false;
            if (!MatchesAll(AllRules.NextNationalityColors))
                return false;
            if (!MatchesAll(AllRules.NextSmokePets))
                return false;
            return true;

            bool MatchesAll(IMatchablePositionRule[] matchablePositionRules) =>
                matchablePositionRules.All(r => r.MatchesPositions(solution.Hypotheses));
        }

        protected override Solution? GetFirst(Solution solution)
        {
            if (solution.Level > 4)
                return null;
            var newSolution = solution with { Level = solution.Level + 1 };
            return newSolution.Level switch
            {
                1 => newSolution.InitColors(),
                2 => newSolution.InitNationalities(),
                3 => newSolution.InitPets(),
                4 => newSolution.InitDrinks(),
                5 => newSolution.InitSmokes(),
                _ => throw new IndexOutOfRangeException(),
            };
        }

        protected override Solution? GetNext(Solution solution)
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
    }
}