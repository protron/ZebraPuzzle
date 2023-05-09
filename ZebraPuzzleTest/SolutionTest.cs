using ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class SolutionTest
    {
        [Fact]
        public void Test1()
        {
            var initialSolution = Solution.Init();
            Assert.All(initialSolution.Hypotheses.Select(x => x.Color), x => Assert.Null(x));
            var solution1 = initialSolution.InitColors();
            TestColors(solution1, Color.Red, Color.Green, Color.Ivory, Color.Yellow, Color.Blue);
            var solution2 = solution1.NextSetOfColors()!;
            TestColors(solution2, Color.Red, Color.Green, Color.Ivory, Color.Blue, Color.Yellow);
            Assert.All(solution2.Hypotheses.Select(x => x.Nationality), x => Assert.Null(x));
            var solution21 = solution2.InitNationalities();
            TestColors(solution21, Color.Red, Color.Green, Color.Ivory, Color.Blue, Color.Yellow);
            TestNationalities(solution21, Nationality.Englishman, Nationality.Spaniard, Nationality.Ukranian, Nationality.Japanese, Nationality.Norwegian);
            var solution22 = solution21.NextSetOfNationalities()!;
            TestNationalities(solution22, Nationality.Englishman, Nationality.Spaniard, Nationality.Ukranian, Nationality.Norwegian, Nationality.Japanese);
            var solution3 = solution2.NextSetOfColors()!;
            Assert.All(solution3.Hypotheses.Select(x => x.Nationality), x => Assert.Null(x));
            TestColors(solution3, Color.Red, Color.Green, Color.Yellow, Color.Ivory, Color.Blue);
            var solution31 = solution3.InitNationalities();
            TestColors(solution31, Color.Red, Color.Green, Color.Yellow, Color.Ivory, Color.Blue);
            TestNationalities(solution31, Nationality.Englishman, Nationality.Spaniard, Nationality.Ukranian, Nationality.Japanese, Nationality.Norwegian);
            var solution32 = solution31.NextSetOfNationalities()!;
            TestColors(solution32, Color.Red, Color.Green, Color.Yellow, Color.Ivory, Color.Blue);
            TestNationalities(solution32, Nationality.Englishman, Nationality.Spaniard, Nationality.Ukranian, Nationality.Norwegian, Nationality.Japanese);
            var solution42 = solution32.NextSetOfColors()!;
            TestColors(solution42, Color.Red, Color.Green, Color.Yellow, Color.Blue, Color.Ivory);
            TestNationalities(solution42, Nationality.Englishman, Nationality.Spaniard, Nationality.Ukranian, Nationality.Norwegian, Nationality.Japanese);
            var solution43 = solution42.NextSetOfColors()!;
            TestColors(solution43, Color.Red, Color.Green, Color.Blue, Color.Ivory, Color.Yellow);
            TestNationalities(solution42, Nationality.Englishman, Nationality.Spaniard, Nationality.Ukranian, Nationality.Norwegian, Nationality.Japanese);
        }

        private static void TestColors(Solution? solution, params Color?[] expectedColors)
        {
            Assert.NotNull(solution);
            Assert.Equal(expectedColors, solution.Hypotheses.Select(x => x.Color).ToArray());
        }

        private static void TestNationalities(Solution? solution, params Nationality?[] expectedNationalities)
        {
            Assert.NotNull(solution);
            Assert.Equal(expectedNationalities, solution.Hypotheses.Select(x => x.Nationality).ToArray());
        }
    }
}