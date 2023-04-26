namespace ZebraPuzzle
{
    public record NextRuleBase
    {
        protected bool MatchesPositions(
            IEnumerable<Hypothesis> hypotheses, Func<Hypothesis, bool> matchOne, Func<Hypothesis, bool> matchTwo)
        {
            var stage = 0; // 0=NotFound, 1=FoundOneFirst, 2=FoundTwoFirst, 3=BothMatch
            foreach (var hypothesis in hypotheses)
            {
                if (matchOne(hypothesis))
                {
                    if (stage == 0 || stage == 2)
                        stage++;
                    else
                        return false;
                    if (matchTwo(hypothesis))
                        return false;
                }
                else if (matchTwo(hypothesis))
                {
                    if (stage == 0)
                        stage = 2;
                    else if (stage == 1)
                        stage = 3;
                    else
                        return false;
                }
                else if (stage == 1 || stage == 2)
                    return false;
            }
            return stage == 3;
        }
    }

    public record NextNationalityColorRule : NextRuleBase
    {
        public Nationality Nationality { get; set; }
        public Color Color { get; set; }

        public bool MatchesPositions(IEnumerable<Hypothesis> hypotheses) =>
            MatchesPositions(
                hypotheses,
                x => x.Nationality == Nationality,
                x => x.Color == Color);
    }

    public record NextSmokePetRule : NextRuleBase
    {
        public Pet Pet { get; set; }
        public Smoke Smoke { get; set; }

        public bool MatchesPositions(IEnumerable<Hypothesis> hypotheses) =>
            MatchesPositions(
                hypotheses,
                x => x.Pet == Pet,
                x => x.Smoke == Smoke);
    }
}