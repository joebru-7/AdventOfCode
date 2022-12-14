

using System.Runtime.CompilerServices;

int width = 200;
part1();

width = 1000;
part2();

void part1()
{
	Status[,] cave = new Status[200, 200];

	for (int x = 0; x < 200; x++)
	{
		for (int y = 0; y < 200; y++)
		{
			cave[x, y] = Status.air;
		}
	}

	// create rocks
	var f = File.OpenText("data.txt");
	while (!f.EndOfStream)
	{
		var points = f.ReadLine().Split(" -> ");

		point prev = new(points[0]);

		for (int i = 1; i < points.Length; i++)
		{
			point curr = new(points[i]);

			cave[curr.x , curr.y] = Status.rock;

			var dir = curr - prev;

			dir.x = Math.Sign(dir.x);
			dir.y = Math.Sign(dir.y);

			for (point p = prev; p != curr; p+= dir)
			{
				cave[p.x, p.y] = Status.rock;
			}

			//printCave(cave);

			prev = curr;
		}
		//printCave(cave);
		;
	}
	printCave(cave);
	int count = 0; 
	try
	{
		while (true)
		{
			//sand falls uppward
			point sand = new(500-400, 0);
			while (true)
			{
				if (cave[sand.x, sand.y + 1] == Status.air)
				{
					sand += (0, 1);
				}
				else if (cave[sand.x - 1, sand.y + 1] == Status.air)
				{
					sand += (-1, 1);
				}
				else if (cave[sand.x + 1, sand.y + 1] == Status.air)
				{
					sand += (1, 1);
				}
				else
				{
					cave[sand.x, sand.y] = Status.sand;
					count++;
					break;
				}
			}
			//printCave(cave);
			;

		}
	}
	catch(IndexOutOfRangeException)
	{
		//done
	}

	Console.WriteLine("part1: " + count);
}



void part2()
{
	Status[,] cave = new Status[1000, 200];

	for (int x = 0; x < 1000; x++)
	{
		for (int y = 0; y < 200; y++)
		{
			cave[x, y] = Status.air;
		}
	}

	// create rocks
	var f = File.OpenText("data.txt");
	int lowestpoint = 0; // max value
	while (!f.EndOfStream)
	{
		var points = f.ReadLine().Split(" -> ");

		point2 prev = new(points[0]);

		for (int i = 1; i < points.Length; i++)
		{
			point2 curr = new(points[i]);

			lowestpoint = Math.Max(lowestpoint, curr.y);

			cave[curr.x, curr.y] = Status.rock;

			var dir = curr - prev;

			dir.x = Math.Sign(dir.x);
			dir.y = Math.Sign(dir.y);

			for (point2 p = prev; p != curr; p = new(p + dir))
			{
				cave[p.x, p.y] = Status.rock;
			}

			//printCave(cave);

			prev = curr;
		}
		//printCave(cave);
		;
	}

	for (int i = 0; i < 1000; i++)
	{
		cave[i, lowestpoint + 2] = Status.rock;
	}

	printCave(cave);

	bool doPrint = false;

	int count = 0;
	while (true)
	{
		//sand falls uppward
		point sand = new point2("500,0");
		if (cave[sand.x, sand.y] == Status.sand)
			break;

		; ;
		while (true)
		{
			if (cave[sand.x, sand.y + 1] == Status.air)
			{
				sand += (0, 1);
			}
			else if (cave[sand.x - 1, sand.y + 1] == Status.air)
			{
				sand += (-1, 1);
			}
			else if (cave[sand.x + 1, sand.y + 1] == Status.air)
			{
				sand += (1, 1);
			}
			else
			{
				cave[sand.x, sand.y] = Status.sand;
				count++;
				break;
			}
		}
		if(doPrint)
			printCave(cave);
		;

	}

	printCave(cave);
	Console.WriteLine("part2: " + count);
}

void printCave(Status[,] cave)
{
	Console.Clear();
	for (int x = 0; x < width; x++)
	{
		for (int y = 000; y < 200; y++)
		{

			Console.Write((char)cave[x, y]);
		}
		Console.Write('\n');
	}
	Console.SetCursorPosition(0, 0);
}

class point
{
	public int x; public int y;
	 
	public point(string s)
	{
		x = int.Parse(s.Split(',')[0]) - 400;
		y = int.Parse(s.Split(',')[1]);

	}
	public point(int x, int y, int dist, float est, point parent = null)
	{
		this.x = x;
		this.y = y;
	}

	public static implicit operator point((int, int) p)
	{
		return new point(p.Item1, p.Item2);
	}
	public static implicit operator (int, int)(point p)
	{
		return (p.x, p.y);
	}

	public point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public point()
	{
	}

	public static point operator +(point p, point q)
	{
		return new point(p.x + q.x, p.y + q.y);
	}

	public static point operator -(point p, point q)
	{
		return new point(p.x - q.x, p.y - q.y);
	}

	public static bool operator ==(point p,point q)
	{
		return p.x == q.x && p.y == q.y;
	}

	public static bool operator!=(point p,point q)
	{
		return p.x != q.x || p.y != q.y;
	}

}

class point2: point
{

	public point2(point p)
	{
		x = p.x;
		y = p.y;
	}

	public point2(string s)
	{
		x = int.Parse(s.Split(',')[0]);
		y = int.Parse(s.Split(',')[1]);

	}
}

class done : Exception { }
	enum Status
{
	air = ' ',
	sand = 's', 
	rock = '#'
}