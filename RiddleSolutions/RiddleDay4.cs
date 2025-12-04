using System.Drawing;

public class RiddleDay4
{
	public static int GetSolution(string inputPath)
	{
		CharMap map = InputParser.GetInputMap(inputPath);
		var accessiblePositions = new List<Point>();
		int lastCount;
		do
		{
			lastCount = accessiblePositions.Count;
			map.eachCharacterDo((point, character) =>
			{
				if (character == '@' && map.GetSurroundingPointsForRank(point, 1).Count(p => map.ElementAtPosition(p) == '@') < 4)
				{
					accessiblePositions.Add(point);
					map.SetElementAtPosition(point, '.');
				}
			});
		} while (accessiblePositions.Count > lastCount);
		return accessiblePositions.Count;
	}

}