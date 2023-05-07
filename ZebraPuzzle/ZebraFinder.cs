namespace ZebraPuzzle
{
    public class ZebraFinder : Backtracker<Solution>.IFinder
    {
        public bool IsRejected(Solution solution) => solution.ContradictsSomeRule();

        public bool IsAccepted(Solution solution) => solution.Level == 5 && solution.MatchesPositions();

        public Solution? GetFirst(Solution solution)
        {
            if (solution.Level > 4)
                return null;
            var newSolution = solution with { Level = solution.Level + 1 };
            return GetNext(newSolution)!;
        }

        public Solution? GetNext(Solution solution)
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