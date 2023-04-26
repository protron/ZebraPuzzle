// https://exercism.org/tracks/csharp/exercises/zebra-puzzle/edit
// https://dev.to/tallesl/explaining-lazy-evaluation-in-c-81m3
// https://en.wikipedia.org/wiki/Backtracking

using System;
using System.Collections.Generic;
using System.Linq;

public enum Color { Red , Green , Ivory , Yellow , Blue }
public enum Nationality { Englishman , Spaniard , Ukranian , Japanese , Norwegian }
public enum Pet { Dog , Snails , Fox , Horse , Zebra }
public enum Drink { Coffee , Tea , Milk , OrangeJuice , Water }
public enum Smoke { OldGold , Kools , Chesterfields , LuckyStrike , Parliaments }

public class Fact
{
    public Color? Color {get; set;}
    public Nationality? Nationality {get; set;}
    public Pet? Pet {get; set;}
    public Drink? Drink {get; set;}
    public Smoke? Smoke {get; set;}
    public int? Position {get; set;}
    
    public const int Props = 6;
    public int GetPropsMissing() => (
        (Color == null ? 1 : 0) +
        (Nationality == null ? 1 : 0) +
        (Pet == null ? 1 : 0) +
        (Drink == null ? 1 : 0) +
        (Smoke == null ? 1 : 0) +
        (Position == null ? 1 : 0)
    );

    public int TryIndex = 0;
}

public class ColorPositionFact
{
    public Color RightColor {get; set;}
    public Color LeftColor {get; set;}
    public int MinPositionRightColor = 2;
    public int MaxPositionLeftColor = 4;
}

public class NextSmokePetFact
{
    public Pet Pet {get; set;}
    public Smoke Smoke {get; set;}
}

public class NextNationalityColorFact
{
    public Nationality Nationality {get; set;}
    public Color Color {get; set;}
}

public static class Facts
{
    private static Fact[] Direct = new [] {
        new Fact { Nationality = Nationality.Englishman, Color = Color.Red }, // rule 2
        new Fact { Nationality = Nationality.Spaniard, Pet = Pet.Dog }, // rule 3
        new Fact { Drink = Drink.Coffee, Color = Color.Green }, // rule 4
        new Fact { Nationality = Nationality.Ukranian, Drink = Drink.Tea }, // rule  5
        new Fact { Smoke = Smoke.OldGold, Pet = Pet.Snails }, // rule 7
        new Fact { Smoke = Smoke.Kools, Color = Color.Yellow }, // rule 8
        new Fact { Drink = Drink.Milk, Position = 3 }, // rule 9
        new Fact { Nationality = Nationality.Norwegian, Position = 1 }, // rule 10
        new Fact { Smoke = Smoke.LuckyStrike, Drink = Drink.OrangeJuice }, // rule 13
        new Fact { Nationality = Nationality.Japanese, Smoke = Smoke.Parliaments }, // rule 14
    };

    public static ColorPositionFact[] ColorPositions = new [] {
        new ColorPositionFact { RightColor = Color.Green, LeftColor = Color.Ivory }, // rule 6
    };

    public static NextSmokePetFact[] NextSmokePets = new [] {
        new NextSmokePetFact { Smoke = Smoke.Chesterfields, Pet = Pet.Fox }, // rule 11
        new NextSmokePetFact { Smoke = Smoke.Kools, Pet = Pet.Horse },  // rule 12
    };

    public static NextNationalityColorFact[] NextNationalityColors = new [] {
        new NextNationalityColorFact { Nationality = Nationality.Norwegian, Color = Color.Blue }, // rule 15
    };

    public static Fact[] Nationalities => Direct.Where(x => x.Nationality != null).ToArray();
    public static Fact[] Colors => Direct.Where(x => x.Color != null).ToArray();
    public static Fact[] Pets => Direct.Where(x => x.Pet != null).ToArray();
    public static Fact[] Drinks => Direct.Where(x => x.Drink != null).ToArray();
    public static Fact[] Smokes => Direct.Where(x => x.Smoke != null).ToArray();
}

public class ZebraPuzzleSolver
{
    private bool IsRejected(Fact fact) {
        switch (fact.GetPropsMissing()) {
            case 5: //Color
                return false;
            case 4: //Color + Nationality
                if (Facts.Nationalities.FirstOrDefault(x => x.Nationality == fact.Nationality) is Fact c1) {
                    return c1.Color != fact.Color;
                }
                return false;
            case 0:
                if (Facts.NextNationalityColors.FirstOrDefault(x => x.Color == fact.Color) is NextNationalityColorFact nColor) {
                    return Solve(nColor.Nationality);
                }
                if (Facts.NextNationalityColors.FirstOrDefault(x => x.Nationality == fact.Nationality) is NextNationalityColorFact nNat) {
                    return Solve(nNat.Color);
                }
                return false;
        }
	}

    private bool IsAccepted(Fact fact) {
        if (fact.GetPropsMissing() < 0) {
            return false;
        }
        return true;
	}

    private Fact GetFirst(Fact fact) {
        switch (fact.GetPropsMissing()) {
            case 1: return Facts.Colors[0];
        }
	}

    private Fact GetNext(Fact fact) {
        var i = ++fact.TryIndex;
        switch (fact.GetPropsMissing()) {
            case 1: return Facts.Colors[i];
        }
	}

    private IEnumerable<Fact> Backtrack(Fact fact) {
        if (IsRejected(fact)) { yield break; }
        if (IsAccepted(fact)) { yield return fact; }
        var newFact = GetFirst(fact);
        List<Fact> results = new List<Fact>();
        while (newFact != null) {
            results.AddRange(Backtrack(newFact));
            newFact = GetNext(newFact);
		}
        foreach (var r in results) {
            yield return r;
        }
	}

    private Fact[] solved = Array.Empty<Fact>();

    public Fact[] Solve() {
        if (solved.Length == 0)
        {
            IEnumerable<Fact> facts = Backtrack(new Fact());
            solved = facts.ToArray();
            if (solved.Length != 5) {
                throw new Exception($"Expected 5 solutions but got {solved.Length}");
            }
        }
        return solved;
    }
}

public static class ZebraPuzzle
{
    private static ZebraPuzzleSolver solver = new ZebraPuzzleSolver();

    public static Nationality GetNationality(Func<Fact, bool> factFunc)
    {
        var first = solver.Solve().First(factFunc);
        if (!first.Nationality.HasValue) {
            throw new Exception($"first.Nationality should not be null: {first.Position}: {first.Pet}");
        }
        return first.Nationality.Value;
    }

    public static Nationality DrinksWater()
    {
        return GetNationality(x => x.Drink == Drink.Water);
    }

    public static Nationality OwnsZebra()
    {
        return GetNationality(x => x.Pet == Pet.Zebra);
    }
}