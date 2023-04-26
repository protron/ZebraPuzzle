using ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class ColorPositionRuleTest
    {
        [Fact]
        public void Contradicts_LeftColorInPosition5_ReturnsTrue()
        {
            var sut = new ColorPositionRule { LeftColor = Color.Ivory, RightColor = Color.Yellow };
            var hypothesis = new Hypothesis { Color = Color.Ivory, Position = 5 };
            Assert.True(sut.Contradicts(hypothesis));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Contradicts_LeftColorInPositionLessThan5_ReturnsFalse(int position)
        {
            var sut = new ColorPositionRule { LeftColor = Color.Ivory, RightColor = Color.Yellow };
            var hypothesis = new Hypothesis { Color = Color.Ivory, Position = position };
            Assert.False(sut.Contradicts(hypothesis));
        }

        [Theory]
        [InlineData(Color.Blue, Color.Ivory, Color.Yellow, Color.Red, Color.Green)]
        [InlineData(Color.Ivory, Color.Yellow, Color.Red, Color.Blue, Color.Green)]
        [InlineData(Color.Red, Color.Blue, Color.Green, Color.Ivory, Color.Yellow)]
        public void MatchesPositions_InConsecutivePositions_ReturnsTrue(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new ColorPositionRule { LeftColor = Color.Ivory, RightColor = Color.Yellow };
            var hypotheses = new[] {
                new Hypothesis { Color = color1 },
                new Hypothesis { Color = color2 },
                new Hypothesis { Color = color3 },
                new Hypothesis { Color = color4 },
                new Hypothesis { Color = color5 },
            };
            Assert.True(sut.MatchesPositions(hypotheses));
        }

        [Theory]
        [InlineData(Color.Blue, Color.Yellow, Color.Red, Color.Ivory, Color.Green)]
        [InlineData(Color.Ivory, Color.Blue, Color.Red, Color.Yellow, Color.Green)]
        public void MatchesPositions_InWrongPositions_ReturnsFalse(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new ColorPositionRule { LeftColor = Color.Ivory, RightColor = Color.Yellow };
            var hypotheses = new[] {
                new Hypothesis { Color = color1 },
                new Hypothesis { Color = color2 },
                new Hypothesis { Color = color3 },
                new Hypothesis { Color = color4 },
                new Hypothesis { Color = color5 },
            };
            Assert.False(sut.MatchesPositions(hypotheses));
        }
    }
}