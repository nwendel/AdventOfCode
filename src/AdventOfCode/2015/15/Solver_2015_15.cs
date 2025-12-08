namespace AdventOfCode._2015._15;

internal class Solver_2015_15 : Solver<Ingredient[]>
{
    protected override Ingredient[] ParseInput(Input input)
    {
        var ingredients = input.Lines
            .Parse<Ingredient>("{Name}: capacity {Capacity}, durability {Durability}, flavor {Flavor}, texture {Texture}, calories {Calories}");

        return ingredients;
    }

    protected override Result SolvePart1Core(Ingredient[] input)
    {
        var result = FindMaxScore(input, null);
        return result;
    }

    protected override Result SolvePart2Core(Ingredient[] input)
    {
        var result = FindMaxScore(input, 500);
        return result;
    }

    private static long FindMaxScore(Ingredient[] ingredients, long? targetCalories)
    {
        var maxScore = 0L;

        foreach (var amounts in Combinatorics.WeakCompositions(ingredients.Length, 100))
        {
            var calories = 0L;
            var capacity = 0L;
            var durability = 0L;
            var flavor = 0L;
            var texture = 0L;

            for (var i = 0; i < ingredients.Length; i++)
            {
                calories += amounts[i] * ingredients[i].Calories;
                capacity += amounts[i] * ingredients[i].Capacity;
                durability += amounts[i] * ingredients[i].Durability;
                flavor += amounts[i] * ingredients[i].Flavor;
                texture += amounts[i] * ingredients[i].Texture;
            }

            calories = Math.Max(calories, 0);
            capacity = Math.Max(capacity, 0);
            durability = Math.Max(durability, 0);
            flavor = Math.Max(flavor, 0);
            texture = Math.Max(texture, 0);

            if (targetCalories.HasValue && targetCalories != calories)
            {
                continue;
            }

            var score = capacity * durability * flavor * texture;

            if (score > maxScore)
            {
                maxScore = score;
            }
        }

        return maxScore;
    }
}

public record Ingredient(
    string Name,
    long Capacity,
    long Durability,
    long Flavor,
    long Texture,
    long Calories);
