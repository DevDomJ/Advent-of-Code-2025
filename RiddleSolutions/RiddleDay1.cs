public class RiddleDay1
{
	public static (int First, int Second) GetSolution(string inputPath)
	{
		string[] lines = InputParser.GetInputStringLines(inputPath);

		var zeroCounter = 0;
		var zeroCounterWithExtraSteps = 0;
		var currentPosition = 50;

		foreach (var line in lines)
		{
			// Move position and count zeros passed
			var direction = line[0];
			var steps = int.Parse(line.Substring(1));

			if (direction == 'R')
			{
				currentPosition += steps;
				zeroCounterWithExtraSteps += currentPosition / 100;
			}
			else
			{
				var difference = steps - currentPosition;
				if (difference >= 0)
				{
					var zeroPasses = difference / 100;
					if (currentPosition != 0)
					{
						zeroPasses++;
					}
					zeroCounterWithExtraSteps += zeroPasses;
				}
				currentPosition -= steps;
			}
			// Adjust position and count end position zeros
			while (currentPosition > 99)
			{
				currentPosition -= 100;
			}
			while (currentPosition < 0)
			{
				currentPosition += 100;
			}
			if (currentPosition == 0)
			{
				zeroCounter++;
			}
		}

		return (zeroCounter, zeroCounterWithExtraSteps);
	}
}