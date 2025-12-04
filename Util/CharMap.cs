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

	public void SetElementAtPosition(Point point, char character)
	{
		SetElementAtPosition(point.X, point.Y, character);
	}

	public void SetElementAtPosition(int x, int y, char character)
	{
		Map[x, y] = character;
	}

	public List<Point> GetSurroundingPointsForRank(Point position, int rank)
	{
		return GetSurroundingSizesForRank(rank).Select(size => position + size).Where(IsPointOnMap).ToList();
	}

	/// <summary>
	/// Get Surrrounding Sizes for rank. Sizes are the relative positions around a point at the given rank. Rank means the n-th layer around the point.
	/// </summary>
	/// <param name="rank"></param>
	/// <returns></returns>
	public List<Size> GetSurroundingSizesForRank(int rank)
	{
		var surroundingSizes = new List<Size>();
		if (rank > 0)
		{
			for (int i = -rank; i <= rank; i++)
			{
				surroundingSizes.Add(new Size(i, rank));
			}
			for (int i = rank - 1; i >= -rank; i--)
			{
				surroundingSizes.Add(new Size(rank, i));
			}
			for (int i = rank - 1; i >= -rank; i--)
			{
				surroundingSizes.Add(new Size(i, -rank));
			}
			for (int i = -rank + 1; i < rank; i++)
			{
				surroundingSizes.Add(new Size(-rank, i));
			}
		}
		return surroundingSizes;
	}

	public void eachCharacterDo(Action<Point, char> action)
	{
		for (int y = 0; y <= VerticalEdge; y++)
		{
			for (int x = 0; x <= HorizontalEdge; x++)
			{
				action(new Point(x, y), Map[x, y]);
			}
		}
	}
}