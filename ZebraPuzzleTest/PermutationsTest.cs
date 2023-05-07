using ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class PermutationsTest
    {
        enum TestEnum3 { T1, T2, T3 }

        enum TestEnum5 { T1, T2, T3, T4, T5 }

        [Fact]
        public void FromEnum_TestEnum3_ReturnsExpected6SetsOf3Each()
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
        public void FromEnum_TestEnum5_ContainsAValidCase()
        {
            var actual = Permutations.FromEnum<TestEnum5>();
            Assert.Contains(new[] { TestEnum5.T4, TestEnum5.T2, TestEnum5.T1, TestEnum5.T5, TestEnum5.T3 }, actual);
        }

        [Fact]
        public void FromEnum_TestEnum5_DoesNotContainRepetitions()
        {
            var actual = Permutations.FromEnum<TestEnum5>();
            Assert.DoesNotContain(new[] { TestEnum5.T2, TestEnum5.T2, TestEnum5.T1, TestEnum5.T5, TestEnum5.T3 }, actual);
        }

        [Fact]
        public void FromEnum_TestEnum5_Returns120SetsOf5Each()
        {
            var actual = Permutations.FromEnum<TestEnum5>();
            List<IList<TestEnum5>> r = actual.ToList();
            Assert.Equal(120, r.Count);
            Assert.All(r, x => Assert.Equal(5, x.Count));
        }
    }
}