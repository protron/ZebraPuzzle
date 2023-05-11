using ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class BacktrakerTest
    {
        public record Test1Solution
        {
            public int Level { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
        }

        public class Test1Backtracker : BaseBacktracker<Test1Solution>
        {
            protected override bool IsRejected(Test1Solution s) => s.A > 1 || s.B > 2 || s.C > 3;

            protected override bool IsAccepted(Test1Solution s) => (s.A == 1) && (s.B == 2) && (s.C == 3);

            protected override Test1Solution? GetFirst(Test1Solution s) => s.Level switch
            {
                0 => s with { Level = 1, A = 1 },
                1 => s with { Level = 2, B = 1 },
                2 => s with { Level = 3, C = 1 },
                _ => null,
            };

            protected override Test1Solution? GetNext(Test1Solution s) => s.Level switch
            {
                1 when s.A < 1 => s with { A = s.A + 1 },
                2 when s.B < 2 => s with { B = s.B + 1 },
                3 when s.C < 3 => s with { C = s.C + 1 },
                _ => null
            };
        }

        [Fact]
        public void Test1()
        {
            var backtracker = new Test1Backtracker();
            var initialSolution = new Test1Solution();
            var result = backtracker.Recurse(initialSolution);
            Assert.NotNull(result);
            Assert.Equal(1, result.A);
            Assert.Equal(2, result.B);
            Assert.Equal(3, result.C);
            Assert.Equal(3, result.Level);
        }
    }
}