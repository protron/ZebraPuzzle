namespace ZebraPuzzle
{
    public class ColorPositionRule
    {
        private const int MinPositionRightColor = 2;
        private const int MaxPositionLeftColor = 4;
        public Color RightColor { get; set; }
        public Color LeftColor { get; set; }

        public bool Contradicts(Hypothesis hypothesis) =>
            (hypothesis.Color == LeftColor && hypothesis.Position > MaxPositionLeftColor) ||
            (hypothesis.Color == RightColor && hypothesis.Position < MinPositionRightColor);

        public bool MatchesPositions(IEnumerable<Hypothesis> hypotheses)
        {
            var stage = 0; // 0=NotFound, 1=FoundFirstItem, 2=BothMatch
            foreach (var hypothesis in hypotheses)
            {
                if (hypothesis.Color == LeftColor)
                {
                    if (stage == 0)
                        stage = 1;
                    else
                        return false;
                }
                else if (hypothesis.Color == RightColor)
                {
                    if (stage == 1)
                        stage = 2;
                    else
                        return false;
                }
                else if (stage == 1)
                    return false;
            }
            return stage == 2;
        }
    }
}