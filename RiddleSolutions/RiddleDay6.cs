using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;

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

		//Celaphoid solution requires a different approach due to their stupid way to save their data in the most inconvenient way possible.
		var charMap = new CharMap(InputParser.GetInputString(inputPath));
		//Find separator column indices first
		var separatorIndices = new List<int>();
		for (int x = charMap.HorizontalEdge; x >= 0; x--)
		{
			bool isSeparator = true;
			for (int y = 0; y <= charMap.VerticalEdge; y++)
			{
				if (charMap.ElementAtPosition(x, y) != ' ')
				{
					isSeparator = false;
					break;
				}
			}
			if (isSeparator)
			{
				separatorIndices.Add(x);
			}
		}
		var celapphoidHomeWorkProblems = new List<HomeWorkProblem>();
		separatorIndices.Add(-1); //Add a fake separator at the end
		var currentStartIndex = charMap.HorizontalEdge;
		//Walk backwards to get columns
		foreach (var separatorIndex in separatorIndices)
		{
			List<long> operands = new List<long>();
			var currentOperator = '+';
			for (int x = currentStartIndex; x > separatorIndex; x--)
			{
				var builder = new StringBuilder();
				for (int y = 0; y <= charMap.VerticalEdge; y++)
				{
					var currentChar = charMap.ElementAtPosition(x, y);
					if (currentChar == '+' || currentChar == '*')
					{
						currentOperator = currentChar;
					}
					else if (currentChar != ' ')
					{
						builder.Append(currentChar);
					}
				}
				operands.Add(long.Parse(builder.ToString()));
			}
			celapphoidHomeWorkProblems.Add(new HomeWorkProblem(operands, currentOperator));
			currentStartIndex = separatorIndex - 1;
		}
		return (homeWorkProblems.Sum(problem => problem.Solve()), celapphoidHomeWorkProblems.Sum(problem => problem.Solve()));
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

	public HomeWorkProblem(IEnumerable<long> operands, char homeWorkOperator)
	{
		this.operands = operands;
		this.homeWorkOperator = homeWorkOperator.ToString();
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