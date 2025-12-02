public static class InputParser
{
	public static string GetInputString(string inputPath)
	{
		return File.ReadAllText(inputPath);
	}

	public static string[] GetInputStringLines(string inputPath)
	{
		return GetInputString(inputPath).Split(Environment.NewLine);
	}

	public static CharMap GetInputMap(string inputPath)
	{
		return new CharMap(GetInputString(inputPath));
	}
}