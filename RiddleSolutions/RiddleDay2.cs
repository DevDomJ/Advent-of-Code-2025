using System.Numerics;

public class RiddleDay2
{
	public static (long First, long Second) GetSolution(string inputPath)
	{
		string input = InputParser.GetInputString(inputPath);
		List<string> ranges = input.Split(',').ToList();
		var invalidIds = new List<long>();

		foreach (var range in ranges)
		{
			string[] bounds = range.Split('-');
			var lowerBound = long.Parse(bounds[0]);
			var upperBound = long.Parse(bounds[1]);

			for (long i = lowerBound; i <= upperBound; i++)
			{
				var numberString = i.ToString();
				var middleIndex = numberString.Length / 2;
				var firstHalf = numberString.Substring(0, middleIndex);
				var secondHalf = numberString.Substring(middleIndex);
				if (firstHalf == secondHalf)
				{
					invalidIds.Add(i);
				}
			}
		}
		return (invalidIds.Sum(), 0);
	}
}