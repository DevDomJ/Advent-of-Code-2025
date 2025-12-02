using System.Numerics;

public class RiddleDay2
{
	public static (long First, long Second) GetSolution(string inputPath)
	{
		string input = InputParser.GetInputString(inputPath);
		List<string> ranges = input.Split(',').ToList();
		var invalidIds = new List<long>();
		var invalidIdsWithExtraStreps = new List<long>();

		foreach (var range in ranges)
		{
			string[] bounds = range.Split('-');
			var lowerBound = long.Parse(bounds[0]);
			var upperBound = long.Parse(bounds[1]);

			for (long currentNumber = lowerBound; currentNumber <= upperBound; currentNumber++)
			{
				// Check invalidIDs Part 1
				var numberString = currentNumber.ToString();
				var middleIndex = numberString.Length / 2;
				var firstHalf = numberString.Substring(0, middleIndex);
				var secondHalf = numberString.Substring(middleIndex);
				if (firstHalf == secondHalf)
				{
					invalidIds.Add(currentNumber);
				}

				//Check invalidIDs Part 2
				for (int subStringLength = 1; subStringLength <= numberString.Length / 2; subStringLength++)
				{
					//Only consider substring lengths that divide the number into parts of equal length
					if (numberString.Length % subStringLength == 0)
					{
						List<string> subStrings = new List<string>();
						for (int i = 0; i < numberString.Length / subStringLength; i++)
						{
							subStrings.Add(numberString.Substring(subStringLength * i, subStringLength));
						}
						//Check if all substrings match the first one -> repeating the same pattern all over
						if (subStrings.All(s => s == subStrings[0]))
						{
							invalidIdsWithExtraStreps.Add(currentNumber);
							break;
						}
					}
				}

			}
		}
		return (invalidIds.Sum(), invalidIdsWithExtraStreps.Sum());
	}
}