namespace ZebraPuzzle
{
    public interface IContradictableRule
    {
        bool Contradicts(Hypothesis hypothesis);
    }
}