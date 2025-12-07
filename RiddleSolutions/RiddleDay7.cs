using System.Drawing;

public class RiddleDay7
{
	public static (long, long) GetSolution(string inputPath)
	{
		var charMap = new CharMap(InputParser.GetInputString(inputPath));
		var beamEntrance = 0;
		for (int x = 0; x <= charMap.HorizontalEdge; x++)
		{
			if (charMap.ElementAtPosition(x, 0) == 'S')
			{
				//Found the entrance
				beamEntrance = x;
				charMap.SetElementAtPosition(new Point(beamEntrance, 0), '.');
				break;
			}
		}
		var tachyonBeam = new TachyonBeam(new Point(beamEntrance, 0), charMap);
		return (tachyonBeam.CalculateSplits(), 0);
	}

}

public class TachyonBeam
{
	Point position;
	CharMap map;
	public TachyonBeam(Point start, CharMap map)
	{
		position = start;
		this.map = map;
	}

	public int CalculateSplits()
	{
		var splits = 0;
		while (map.IsPointOnMap(position) && (map.ElementAtPosition(position) == '.' || map.ElementAtPosition(position) == '|'))
		{
			map.SetElementAtPosition(position, '|');
			position.Y++;
		}
		if (map.IsPointOnMap(position) && map.ElementAtPosition(position) == '^')
		{
			var leftPoint = new Point(position.X - 1, position.Y);
			var rightPoint = new Point(position.X + 1, position.Y);
			bool hasSplit = false;
			if (map.IsPointOnMap(leftPoint) && map.ElementAtPosition(leftPoint) == '.')
			{
				splits += new TachyonBeam(leftPoint, map).CalculateSplits();
				hasSplit = true;
			}
			if (map.IsPointOnMap(rightPoint) && map.ElementAtPosition(rightPoint) == '.')
			{
				splits += new TachyonBeam(rightPoint, map).CalculateSplits();
				hasSplit = true;
			}
			if (hasSplit)
			{
				splits++;
			}
		}
		return splits;
	}
}