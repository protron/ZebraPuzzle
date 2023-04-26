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

public class BaseFact
{
    public int? Position { get; set; }
    public Color? Color { get; set; }
    public Nationality? Nationality { get; set; }
    public Pet? Pet { get; set; }
    public Drink? Drink { get; set; }
    public Smoke? Smoke { get; set; }

    public int?[] Values => new int?[] {
        Position,
        (int?)Color,
        (int?)Nationality,
        (int?)Pet,
        (int?)Drink,
        (int?)Smoke,
    };
}

public class TryFact : BaseFact
{
}

public class Fact : BaseFact
{
    private bool SharesSomeValue(TryFact other) =>
        Values.Zip(other.Values).Any(x => x.First.HasValue && x.First == x.Second);

    private bool DiffersInSomeValue(TryFact other) =>
        Values.Zip(other.Values).Any(x => x.First.HasValue && x.Second.HasValue && x.First != x.Second);

    public bool Contradicts(TryFact other) =>
        SharesSomeValue(other) && DiffersInSomeValue(other);
}

public class ColorPositionFact
{
    public Color RightColor {get; set;}
    public Color LeftColor {get; set;}
    public int MinPositionRightColor = 2;
    public int MaxPositionLeftColor = 4;

    public bool Contradicts(TryFact other) =>
        (other.Color == LeftColor && other.Position > MaxPositionLeftColor) ||
        (other.Color == RightColor && other.Position < MinPositionRightColor);

    public bool MatchesPositions(TryFact[] several)
    {
        var stage = 0; // 0=NotFound, 1=FoundFirstItem, 2=BothMatch
        for (int position = 0; position < several.Length; position++)
        {
            var item = several[position];
            if (item.Color == LeftColor)
            {
                if (stage == 0)
                    stage = 1;
                else
                    return false;
            }
            else if (item.Color == RightColor)
            {
                if (stage == 1)
                    stage = 2;
                else
                    return false;
            }
            else if (stage == 1)
                return false;
        }
        return stage == 2;
    }
}

public class NextSmokePetFact
{
    public Pet Pet {get; set;}
    public Smoke Smoke {get; set;}

    public bool MatchesPositions(TryFact[] several)
    {
        var found = 0;
        var lastFirstPosition = several.Length - 1;
        for (int firstPosition = 0; firstPosition < lastFirstPosition; firstPosition++)
        {
            var firstItem = several[firstPosition];
            if (firstItem.Pet == Pet)
            {
                var secondItem = several[firstPosition++];
                if (Smoke != secondItem.Smoke)
                {
                    return false;
                }
                found = true;
            }
            else if (firstItem.Smoke == Smoke)
            {
                return false;
            }
        }
        return found;
    }
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
}
/*
public class ZebraPuzzleSolver
{
    private bool IsRejected(Solution solution) {
	}

    private bool IsAccepted(Solution solution) {
        if (solution.IsInvalid()) {
            return false;
        }
        return true;
	}

    private Solution GetFirst(Solution solution) {
        switch (solution.GetMissingFacts()) {
            case 1:
                return Facts.Colors[0];
        }
	}

    private Solution GetNext(Solution solution) {
        var i = ++solution.TryIndex;
        switch (solution.GetMissingFacts()) {
            case 1: return Facts.Colors[i];
        }
	}

    private IEnumerable<Solution> Backtrack(Solution solution) {
        if (IsRejected(solution)) { yield break; }
        if (IsAccepted(solution)) { yield return solution; }
        var newSolution = GetFirst(solution);
        List<Solution> finalSolutions = new List<Solution>();
        while (newSolution != null) {
            finalSolutions.AddRange(Backtrack(newSolution));
            newSolution = GetNext(newSolution);
		}
        foreach (var finalSolution in finalSolutions) {
            yield return finalSolution;
        }
	}

    private Solution solution = new Solution();

    public Solution Solve() {
        solution = Backtrack(solution).Single();
        return solution;
    }
    
    public class Solution
    {
        private Fact[] facts = Enumerable.Range(1, 5).Select(x => new Fact { Position = x }).ToArray();

        internal Solution() {}

        public bool IsInvalid() {
            if (facts.Any(x => x.Position == )){
                return true;
            }
            if (Facts.NextNationalityColors.FirstOrDefault(x => x.Color == solution.Color) is NextNationalityColorFact nColor) {
                return Solve(nColor.Nationality);
            }
            if (Facts.NextNationalityColors.FirstOrDefault(x => x.Nationality == solution.Nationality) is NextNationalityColorFact nNat) {
                return Solve(nNat.Color);
            }
            return false;
        }

        public Nationality GetNationality(Func<Fact, bool> func)
        {
            var first = facts.First(func);
            if (!first.Nationality.HasValue) {
                throw new Exception($"first.Nationality should not be null: {first.Position}: {first.Pet}");
            }
            return first.Nationality.Value;
        }
    }
}
*/
public  class ZebraPuzzleSolver
{
    public Solution Solve() {
        return new Solution();
    }
    
    public class Solution
    {
        private Fact[] facts = Enumerable.Range(1, 5).Select(x => new Fact { Position = x }).ToArray();

        internal Solution() {}

        public bool IsInvalid() {
            if (facts.Any(x => true)){
                return true;
            }
            return false;
        }

        public Nationality GetNationality(Func<Fact, bool> func)
        {
            var first = facts.First(func);
            if (!first.Nationality.HasValue) {
                throw new Exception($"first.Nationality should not be null: {first.Position}: {first.Pet}");
            }
            return first.Nationality.Value;
        }
    }
}
public static class ZebraPuzzle
{
    private static ZebraPuzzleSolver.Solution solution = new ZebraPuzzleSolver().Solve();

    public static Nationality DrinksWater()
    {
        return solution.GetNationality(x => x.Drink == Drink.Water);
    }

    public static Nationality OwnsZebra()
    {
        return solution.GetNationality(x => x.Pet == Pet.Zebra);
    }
}
