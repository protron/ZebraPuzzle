using ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class NextNationalityColorRuleTest
    {
        [Theory]
        [InlineData(Color.Ivory, Color.Red, Color.Green, Color.Blue, Color.Yellow)]
        [InlineData(Color.Yellow, Color.Red, Color.Ivory, Color.Blue, Color.Green)]
        public void MatchesPositions_RedNextToFirst_ReturnsTrue(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new NextNationalityColorRule { Color = Color.Red, Nationality = Nationality.Ukranian };
            var hypotheses = new[] {
                new Hypothesis { Color = color1, Nationality = Nationality.Ukranian },
                new Hypothesis { Color = color2, Nationality = Nationality.Spaniard },
                new Hypothesis { Color = color3, Nationality = Nationality.Englishman },
                new Hypothesis { Color = color4, Nationality = Nationality.Japanese },
                new Hypothesis { Color = color5, Nationality = Nationality.Norwegian },
            };
            Assert.True(sut.MatchesPositions(hypotheses));
        }

        [Theory]
        [InlineData(Color.Red, Color.Blue, Color.Yellow, Color.Ivory, Color.Green)]
        [InlineData(Color.Blue, Color.Green, Color.Red, Color.Ivory, Color.Yellow)]
        [InlineData(Color.Blue, Color.Ivory, Color.Yellow, Color.Red, Color.Green)]
        [InlineData(Color.Yellow, Color.Ivory, Color.Green, Color.Blue, Color.Red)]
        public void MatchesPositions_RedNotNextToFirst_ReturnsFalse(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new NextNationalityColorRule { Color = Color.Red, Nationality = Nationality.Ukranian };
            var hypotheses = new[] {
                new Hypothesis { Color = color1, Nationality = Nationality.Ukranian },
                new Hypothesis { Color = color2, Nationality = Nationality.Spaniard },
                new Hypothesis { Color = color3, Nationality = Nationality.Englishman },
                new Hypothesis { Color = color4, Nationality = Nationality.Japanese },
                new Hypothesis { Color = color5, Nationality = Nationality.Norwegian },
            };
            Assert.False(sut.MatchesPositions(hypotheses));
        }

        [Theory]
        [InlineData(Color.Ivory, Color.Green, Color.Red, Color.Blue, Color.Yellow)]
        [InlineData(Color.Blue, Color.Yellow, Color.Red, Color.Ivory, Color.Green)]
        [InlineData(Color.Red, Color.Blue, Color.Green, Color.Ivory, Color.Yellow)]
        [InlineData(Color.Red, Color.Blue, Color.Yellow, Color.Ivory, Color.Green)]
        public void MatchesPositions_RedNextToSecond_ReturnsTrue(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new NextNationalityColorRule { Color = Color.Red, Nationality = Nationality.Spaniard };
            var hypotheses = new[] {
                new Hypothesis { Color = color1, Nationality = Nationality.Ukranian },
                new Hypothesis { Color = color2, Nationality = Nationality.Spaniard },
                new Hypothesis { Color = color3, Nationality = Nationality.Englishman },
                new Hypothesis { Color = color4, Nationality = Nationality.Japanese },
                new Hypothesis { Color = color5, Nationality = Nationality.Norwegian },
            };
            Assert.True(sut.MatchesPositions(hypotheses));
        }

        [Theory]
        [InlineData(Color.Ivory, Color.Red, Color.Blue, Color.Yellow, Color.Green)]
        [InlineData(Color.Blue, Color.Ivory, Color.Yellow, Color.Red, Color.Green)]
        [InlineData(Color.Yellow, Color.Ivory, Color.Green, Color.Blue, Color.Red)]
        public void MatchesPositions_RedNotNextToSecond_ReturnsFalse(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new NextNationalityColorRule { Color = Color.Red, Nationality = Nationality.Spaniard };
            var hypotheses = new[] {
                new Hypothesis { Color = color1, Nationality = Nationality.Ukranian },
                new Hypothesis { Color = color2, Nationality = Nationality.Spaniard },
                new Hypothesis { Color = color3, Nationality = Nationality.Englishman },
                new Hypothesis { Color = color4, Nationality = Nationality.Japanese },
                new Hypothesis { Color = color5, Nationality = Nationality.Norwegian },
            };
            Assert.False(sut.MatchesPositions(hypotheses));
        }

        [Theory]
        [InlineData(Color.Ivory, Color.Green, Color.Blue, Color.Red, Color.Yellow)]
        [InlineData(Color.Yellow, Color.Ivory, Color.Blue, Color.Red, Color.Green)]
        public void MatchesPositions_RedNextToLast_ReturnsTrue(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new NextNationalityColorRule { Color = Color.Red, Nationality = Nationality.Norwegian };
            var hypotheses = new[] {
                new Hypothesis { Color = color1, Nationality = Nationality.Ukranian },
                new Hypothesis { Color = color2, Nationality = Nationality.Spaniard },
                new Hypothesis { Color = color3, Nationality = Nationality.Englishman },
                new Hypothesis { Color = color4, Nationality = Nationality.Japanese },
                new Hypothesis { Color = color5, Nationality = Nationality.Norwegian },
            };
            Assert.True(sut.MatchesPositions(hypotheses));
        }

        [Theory]
        [InlineData(Color.Red, Color.Blue, Color.Yellow, Color.Ivory, Color.Green)]
        [InlineData(Color.Blue, Color.Red, Color.Ivory, Color.Yellow, Color.Green)]
        [InlineData(Color.Blue, Color.Green, Color.Red, Color.Ivory, Color.Yellow)]
        [InlineData(Color.Yellow, Color.Ivory, Color.Green, Color.Blue, Color.Red)]
        public void MatchesPositions_RedNotNextToLast_ReturnsFalse(
            Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            var sut = new NextNationalityColorRule { Color = Color.Red, Nationality = Nationality.Norwegian };
            var hypotheses = new[] {
                new Hypothesis { Color = color1, Nationality = Nationality.Ukranian },
                new Hypothesis { Color = color2, Nationality = Nationality.Spaniard },
                new Hypothesis { Color = color3, Nationality = Nationality.Englishman },
                new Hypothesis { Color = color4, Nationality = Nationality.Japanese },
                new Hypothesis { Color = color5, Nationality = Nationality.Norwegian },
            };
            Assert.False(sut.MatchesPositions(hypotheses));
        }
    }
}