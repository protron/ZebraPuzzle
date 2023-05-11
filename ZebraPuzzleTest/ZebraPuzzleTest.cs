using ZebraPuzzle;
using ZP = ZebraPuzzle.ZebraPuzzle;

namespace ZebraPuzzleTest
{
    public class ZebraPuzzleTest
    {
        [Fact]
        public void TestBothMethods()
        {
            var drinksWater = ZP.DrinksWater();
            var resultOwnsZebra = ZP.OwnsZebra();
            Assert.Equal(Nationality.Norwegian, drinksWater);
            Assert.Equal(Nationality.Japanese, resultOwnsZebra);
        }
    }
}