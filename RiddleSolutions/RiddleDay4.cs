using System.Drawing;

public class RiddleDay4
{
	public static (int First, int Second) GetSolution(string inputPath)
	{
		CharMap map = InputParser.GetInputMap(inputPath);
		var accessiblePositions = new List<Point>();
		map.eachCharacterDo((point, character) =>
		{
			if (character == '@' && map.GetSurroundingPointsForRank(point, 1).Count(p => map.ElementAtPosition(p) == '@') < 4)
			{
				accessiblePositions.Add(point);
			}
		});
		return (accessiblePositions.Count, 0);
	}

}