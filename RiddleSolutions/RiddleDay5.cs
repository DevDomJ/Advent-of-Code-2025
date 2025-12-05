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

		var mergedRanges = NumberRange.MergeRanges(freshIdRanges);
		return (freshIngredientsCounter, mergedRanges.Sum(r => r.Length()));
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

	public NumberRange(long startIndex, long endIndex)
	{
		this.startIndex = startIndex;
		this.endIndex = endIndex;
	}

	public bool Contains(long number)
	{
		return number >= startIndex && number <= endIndex;
	}

	/// <summary>
	/// Returns true if this range touches or overlaps with the other range. Assumes this.startIndex <= other.startIndex.
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public bool Touches(NumberRange other)
	{
		return this.Contains(other.startIndex) || other.startIndex == this.startIndex + 1;
	}


	/// <summary>
	/// Merges this range with the other range if they touch or overlap. Assumes this.startIndex <= other.startIndex.
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public bool MergeWith(NumberRange other)
	{
		if (this.Touches(other))
		{
			if (other.endIndex > this.endIndex)
			{
				this.endIndex = other.endIndex;
			}
			return true;
		}
		return false;
	}

	public long Length()
	{
		return endIndex - startIndex + 1;
	}

	/// <summary>
	/// Merges the given list of ranges into as few ranges as possible. Repeats until no more merges are possible.
	/// </summary>
	/// <param name="ranges"></param>
	/// <returns></returns>
	public static List<NumberRange> MergeRanges(List<NumberRange> ranges)
	{
		var mergedRanges = new List<NumberRange>(ranges);
		List<NumberRange> workRanges;
		do
		{
			workRanges = mergedRanges;
			workRanges.Sort((a, b) => a.startIndex.CompareTo(b.startIndex));
			mergedRanges = new List<NumberRange>();
			var currentRange = workRanges.First();
			mergedRanges.Add(currentRange);
			foreach (var range in workRanges)
			{
				if (!currentRange.MergeWith(range))
				{
					mergedRanges.Add(range);
					currentRange = range;
				}
			}
		} while (mergedRanges.Count != workRanges.Count);
		return mergedRanges;
	}
}