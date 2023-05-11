namespace ZebraPuzzle
{
    public interface IMatchablePositionRule
    {
        bool MatchesPositions(IEnumerable<Hypothesis> hypotheses);
    }
}