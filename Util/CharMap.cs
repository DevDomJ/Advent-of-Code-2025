using System.Drawing;
using System.Numerics;

public class CharMap
{
	public char[,] Map { get; private set; }
	/// <summary>
	/// The inner edge, so the last valid index for the horizontal dimension.
	/// </summary>
	public int HorizontalEdge { get; set; }
	/// <summary>
	/// The inner edge, so the last valid index for the vertical dimension.
	/// </summary>
	public int VerticalEdge { get; set; }
	public CharMap(char[,] map)
	{
		Map = map;
		HorizontalEdge = Map.GetLength(0) - 1;
		VerticalEdge = Map.GetLength(1) - 1;
	}

	public CharMap(string inputString) : this(CreateCharMapFromString(inputString))
	{

	}

	private static char[,] CreateCharMapFromString(string inputString)
	{
		string[] lines = inputString.Split(Environment.NewLine);
		int lineLength = lines[0].Length;
		char[,] map = new char[lineLength, lines.Length];

		for (int j = 0; j < lines.Length; j++)
		{
			var currentString = lines[j];
			for (int i = 0; i < lineLength; i++)
			{
				map[i, j] = currentString[i];
			}
		}
		return map;
	}

	public bool IsPointOnMap(Point point)
	{
		return point.X >= 0 && point.X <= HorizontalEdge && point.Y >= 0 && point.Y <= VerticalEdge;
	}

	public char ElementAtPosition(Point point)
	{
		return ElementAtPosition(point.X, point.Y);
	}

	public char ElementAtPosition(int x, int y)
	{
		return Map[x, y];
	}
}