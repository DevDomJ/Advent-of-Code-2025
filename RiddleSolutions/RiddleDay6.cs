using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

public class RiddleDay6
{
	public static (long, long) GetSolution(string inputPath)
	{
		var lines = InputParser.GetInputStringLines(inputPath).ToList();
		int width = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
		int height = lines.Count;
		var map = new string[width][]; // Can't give height at creation anymore, this is fucking stupid. Was that always like this?
		for (int x = 0; x < width; x++)
		{
			map[x] = new string[height];
		}
		for (int y = 0; y < height; y++)
		{
			var line = lines[y];
			var elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			for (int x = 0; x < width; x++)
			{
				map[x][y] = elements[x];
			}
		}
		var homeWorkProblems = new List<HomeWorkProblem>();
		for (int x = 0; x < width; x++)
		{
			homeWorkProblems.Add(new HomeWorkProblem(map[x]));
		}

		return (homeWorkProblems.Sum(problem => problem.Solve()), 0);
	}

}

public class HomeWorkProblem
{
	private string homeWorkOperator;
	private IEnumerable<long> operands;
	public HomeWorkProblem(string[] column)
	{
		var pieces = column.ToList();
		homeWorkOperator = pieces.Last();
		pieces.RemoveAt(pieces.Count - 1);
		operands = pieces.Select(piece => long.Parse(piece));
	}

	public long Solve()
	{
		if (homeWorkOperator == "+")
		{
			return operands.Sum();
		}
		else
		{
			return operands.Aggregate((a, b) => a * b);
		}
	}
}