public static class InputParser
{
	public static string GetInputString()
	{
		return File.ReadAllText(".\\Input\\Input.txt");
	}

	public static string[] GetInputStringLines()
	{
		return GetInputString().Split(Environment.NewLine);
	}

	public static CharMap GetInputMap()
	{
		return new CharMap(GetInputString());
	}
}