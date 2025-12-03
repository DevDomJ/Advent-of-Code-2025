public class RiddleDay3
{
	public static (int First, int Second) GetSolution(string inputPath)
	{
		var batteryBanks = InputParser.GetInputStringLines(inputPath);
		var maxVoltages = new List<int>();
		foreach (var bank in batteryBanks)
		{
			// find maximum digit in bank except last digit.
			var firstDigit = FindMaximumDigitInString(bank.Substring(0, bank.Length - 1));
			// find maximum in bank after first digit.
			var secondDigit = FindMaximumDigitInString(bank.Substring(bank.IndexOf(firstDigit) + 1));
			// combine both digits to form maximum voltage.
			maxVoltages.Add(int.Parse(firstDigit.ToString() + secondDigit));
		}
		return (maxVoltages.Sum(), 0);
	}

	public static char FindMaximumDigitInString(string bank)
	{
		var maxDigit = '0';
		foreach (var digit in bank)
		{
			if (digit > maxDigit)
			{
				maxDigit = digit;
			}
		}
		return maxDigit;
	}
}