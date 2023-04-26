namespace ZebraPuzzle
{
    public class Rule : BaseRule
    {
        private bool SharesSomeValue(Hypothesis hypothesis) =>
            Values.Zip(hypothesis.Values).Any(x => x.First.HasValue && x.First == x.Second);

        private bool DiffersInSomeValue(Hypothesis hypothesis) =>
            Values.Zip(hypothesis.Values).Any(x => x.First.HasValue && x.Second.HasValue && x.First != x.Second);

        public bool Contradicts(Hypothesis hypothesis) =>
            SharesSomeValue(hypothesis) && DiffersInSomeValue(hypothesis);
    }
}