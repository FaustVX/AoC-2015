#nullable enable
namespace AdventOfCode.Y2015.Day15;

[ProblemName("Science for Hungry People")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var ingredients = ParseInput(input);
        var sets = Combine(ingredients, 100);
        return sets.Max(CalculateSetScoreWithoutCalories);
    }

    private static List<Ingredient> ParseInput(string input)
    {
#pragma warning disable CS0612 //ObsoleteAttribute
        var lines = input.SplitLine();
#pragma warning restore CS0612
        var ingredients = new List<Ingredient>(capacity: lines.Length);
        foreach (var line in lines)
        {
            (var name, (_, (var capacity, (_, (var durability, (_, (var flavor, (_, (var texture, (_, (var calories, _))))))))))) = line.Split(" :,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            ingredients.Add(new(name, int.Parse(capacity), int.Parse(durability), int.Parse(flavor), int.Parse(texture), int.Parse(calories)));
        }
        return ingredients;
    }

    private static List<ImmutableHashSet<Quantity>> Combine(List<Ingredient> ingredients, int remainingQuantity)
    {
        var sets = new List<ImmutableHashSet<Quantity>>(capacity: 101);
        var ingredientsList = ImmutableList.CreateRange(ingredients).Remove(ingredients[0]);
        for (var i = 0; i <= remainingQuantity; i++)
        {
            Inner(ingredientsList, sets, ImmutableHashSet.Create(new Quantity(i, ingredients[0])), 100 - i);
        }
        return sets;

        static void Inner(ImmutableList<Ingredient> ingredients, List<ImmutableHashSet<Quantity>> sets, ImmutableHashSet<Quantity> currentSet, int remainingQuantity)
        {
            if (ingredients.Count == 1)
            {
                sets.Add(currentSet.Add(new(remainingQuantity, ingredients[0])));
                return;
            }
            if (remainingQuantity <= 0 && ingredients.IsEmpty)
            {
                sets.Add(currentSet);
                return;
            }
            if (remainingQuantity <= 0 || ingredients.IsEmpty)
                return;
            var ingredientsList = ImmutableList.CreateRange(ingredients).Remove(ingredients[0]);
            for (var i = 0; i <= remainingQuantity; i++)
            {
                Inner(ingredientsList, sets, currentSet.Add(new(i, ingredients[0])), remainingQuantity - i);
            }
        }
    }

    private static int CalculateSetScoreWithoutCalories(ImmutableHashSet<Quantity> set)
    {
        var capacity = 0;
        var durability = 0;
        var flavor = 0;
        var texture = 0;

        foreach (var (quantity, ingredient) in set)
        {
            capacity += quantity * ingredient.Capacity;
            durability += quantity * ingredient.Durability;
            flavor += quantity * ingredient.Flavor;
            texture += quantity * ingredient.Texture;
        }

        Set0(ref capacity);
        Set0(ref durability);
        Set0(ref flavor);
        Set0(ref texture);

        return capacity * durability * flavor * texture;

        static void Set0(ref int stat)
        {
            if (stat < 0)
                stat = 0;
        }
    }

    private static int CalculateSetCalories(ImmutableHashSet<Quantity> set)
    {
        var calories = 0;

        foreach (var (quantity, ingredient) in set)
            calories += quantity * ingredient.Calories;

        return calories;
    }

    public object PartTwo(string input)
    {
        var ingredients = ParseInput(input);
        var sets = Combine(ingredients, 100)
            .Where(static s => CalculateSetCalories(s) == 500);
        return sets.Max(CalculateSetScoreWithoutCalories);
    }
}

record class Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
record class Quantity(int TeaSpoons, Ingredient Ingredient);
