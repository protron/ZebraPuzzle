namespace ZebraPuzzle
{
    public static class AllRules
    {
        public static readonly CommonRule[] Direct = new[] {
            new CommonRule { Nationality = Nationality.Englishman, Color = Color.Red }, // rule 2
            new CommonRule { Nationality = Nationality.Spaniard, Pet = Pet.Dog }, // rule 3
            new CommonRule { Drink = Drink.Coffee, Color = Color.Green }, // rule 4
            new CommonRule { Nationality = Nationality.Ukranian, Drink = Drink.Tea }, // rule  5
            new CommonRule { Smoke = Smoke.OldGold, Pet = Pet.Snails }, // rule 7
            new CommonRule { Smoke = Smoke.Kools, Color = Color.Yellow }, // rule 8
            new CommonRule { Drink = Drink.Milk, Position = 3 }, // rule 9
            new CommonRule { Nationality = Nationality.Norwegian, Position = 1 }, // rule 10
            new CommonRule { Smoke = Smoke.LuckyStrike, Drink = Drink.OrangeJuice }, // rule 13
            new CommonRule { Nationality = Nationality.Japanese, Smoke = Smoke.Parliaments }, // rule 14
        };

        public static readonly ColorPositionRule[] ColorPositions = new[] {
            new ColorPositionRule { RightColor = Color.Green, LeftColor = Color.Ivory }, // rule 6
        };

        public static readonly NextSmokePetRule[] NextSmokePets = new[] {
            new NextSmokePetRule { Smoke = Smoke.Chesterfields, Pet = Pet.Fox }, // rule 11
            new NextSmokePetRule { Smoke = Smoke.Kools, Pet = Pet.Horse },  // rule 12
        };

        public static readonly NextNationalityColorRule[] NextNationalityColors = new[] {
            new NextNationalityColorRule { Nationality = Nationality.Norwegian, Color = Color.Blue }, // rule 15
        };
    }
}