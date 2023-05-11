namespace ZebraPuzzle
{
    public sealed record Hypothesis : BaseRule
    {
        public Hypothesis()
        {
        }

        public Hypothesis(int position)
        {
            Position = position;
        }
    }
}