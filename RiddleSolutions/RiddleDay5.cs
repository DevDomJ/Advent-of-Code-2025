public class RiddleDay5
{
	public static (long, long) GetSolution(string inputPath)
	{
		var lines = InputParser.GetInputStringLines(inputPath);
		var freshIdRanges = new List<NumberRange>();
		var availableIngredientIds = new List<string>();
		bool foundSeparator = false;
		foreach (var line in lines)
		{
			if (line == "")
			{
				foundSeparator = true;
			}
			else if (foundSeparator)
			{
				availableIngredientIds.Add(line);
			}
			else
			{
				freshIdRanges.Add(new NumberRange(line));
			}
		}
		long freshIngredientsCounter = 0;
		foreach (var availableIngredientId in availableIngredientIds)
		{
			if (freshIdRanges.Any(range => range.Contains(long.Parse(availableIngredientId))))
			{
				freshIngredientsCounter++;
			}
		}
		return (freshIngredientsCounter, 0);
	}

}

public class NumberRange
{
	public long startIndex;
	public long endIndex;
	public NumberRange(string range)
	{
		var numberStrings = range.Split('-');
		startIndex = long.Parse(numberStrings[0]);
		endIndex = long.Parse(numberStrings[1]);
	}

	public bool Contains(long number)
	{
		return number >= startIndex && number <= endIndex;
	}
}