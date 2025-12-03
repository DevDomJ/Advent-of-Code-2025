public class RiddleDay3
{
	public static (long First, long Second) GetSolution(string inputPath)
	{
		var batteryBanks = InputParser.GetInputStringLines(inputPath);
		var partOneMaxVoltages = new List<long>();
		var partOneMaxBatteries = 2;
		var partTwoMaxVoltages = new List<long>();
		var partTwoMaxBatteries = 12;
		foreach (var bank in batteryBanks)
		{
			partOneMaxVoltages.Add(GetMaximumVoltageFromBank(bank, partOneMaxBatteries));
			partTwoMaxVoltages.Add(GetMaximumVoltageFromBank(bank, partTwoMaxBatteries));
		}
		return (partOneMaxVoltages.Sum(), partTwoMaxVoltages.Sum());
	}

	public static long GetMaximumVoltageFromBank(string bank, int maxBatteries)
	{
		var digits = new char[maxBatteries];
		var indexOfLastUsedDigit = -1;
		for (int batteriesLeft = maxBatteries; batteriesLeft > 0; batteriesLeft--)
		{
			var newStartIndex = indexOfLastUsedDigit + 1;
			// Find maximum digit in sub string that makes sure there are enough digits left to fill the remaining batteries
			digits[maxBatteries - batteriesLeft] = FindMaximumDigitInString(bank.Substring(newStartIndex, bank.Length - batteriesLeft - newStartIndex + 1));
			indexOfLastUsedDigit = bank.IndexOf(digits[maxBatteries - batteriesLeft], newStartIndex);
		}
		// Combine digits to form the maximum voltage number
		return long.Parse(digits.Select(digit => digit.ToString()).Aggregate((a, b) => a + b));
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