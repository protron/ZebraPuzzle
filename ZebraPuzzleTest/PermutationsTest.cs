using ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class PermutationsTest
    {
        public enum TestEnum3 { T1, T2, T3 }

        public enum TestEnum5 { T1, T2, T3, T4, T5 }

        [Fact]
        public void Permutations_FromEnum_TestEnum3()
        {
            var actual = Permutations.FromEnum<TestEnum3>();
            var expected = new[] {
                new[] {0,1,2},
                new[] {0,2,1},
                new[] {1,0,2},
                new[] {1,2,0},
                new[] {2,0,1},
                new[] {2,1,0},
            }.Select(x => x.Select(y => (TestEnum3)y).ToArray()).ToArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Permutations_FromEnum_TestEnum5()
        {
            var actual = Permutations.FromEnum<TestEnum5>();
            Assert.Equal(120, actual.Length);
            Assert.All(actual, x => Assert.Equal(5, x.Length));
            Assert.Contains(new[] { TestEnum5.T4, TestEnum5.T2, TestEnum5.T1, TestEnum5.T5, TestEnum5.T3 }, actual);
            Assert.DoesNotContain(new[] { TestEnum5.T2, TestEnum5.T2, TestEnum5.T1, TestEnum5.T5, TestEnum5.T3 }, actual);
        }
    }
}