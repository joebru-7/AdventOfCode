using System.Net.Mail;

part1();
part2();

void part1()
{
	var f = File.OpenText("testdata.txt");

	HashSet<Point> visited = new();

	int score = 0;

	Point head = (0,0);
	Point tail = (0,0);

	while (!f.EndOfStream)
	{
		var line = f.ReadLine();

		Point dir = line[0] switch
		{
			'U' => (0, 1),
			'D' => (0, -1),
			'R' => (1, 0),
			'L' => (-1, 0),
			_ => throw new NotImplementedException()
		};

		for (int i = 0; i < int.Parse(line[2..]); i++)
		{
			head += dir;

			//follow head
			if (Math.Abs(tail.x - head.x) > 1 || Math.Abs(tail.y - head.y) > 1)
			{
				tail.x += Math.Sign(head.x - tail.x);
				tail.y += Math.Sign(head.y - tail.y);
			}

			_ = visited.Add(tail);

		}
	}

	Console.WriteLine("part1 : " + visited.Count);
	f.Close();
}

void part2()
{


	var f = File.OpenText("data.txt");

	HashSet<Point> visited = new();

	Point[] knots = new Point[10];

	while (!f.EndOfStream)
	{
		var line = f.ReadLine();

		Point dir = line[0] switch
		{
			'U' => (0, 1),
			'D' => (0, -1),
			'R' => (1, 0),
			'L' => (-1, 0),
			_ => throw new NotImplementedException()
		};

		for (int i = 0; i < int.Parse(line[2..]); i++)
		{
			knots[0] += dir;

			for (int j = 1; j < knots.Length; j++)
			{
				if (Math.Abs(knots[j].x - knots[j-1].x) > 1 || Math.Abs(knots[j].y - knots[j-1].y) > 1)
				{
					knots[j].x += Math.Sign(knots[j-1].x - knots[j].x);
					knots[j].y += Math.Sign(knots[j-1].y - knots[j].y);
				}

			}

			_ = visited.Add(knots[9]);

		}
	}

	Console.WriteLine("part1 : " + visited.Count);
	f.Close();
}

struct Point
{
	public static implicit operator Point((int,int) v )
	{
		return new Point(v.Item1, v.Item2);
	}
	public Point (int x,int y)
	{
		this.x = x;
		this.y = y;
	}

	public static Point operator+(Point p , Point q)
	{
		return new Point(p.x+q.x,p.y+q.y);
	}
	public int x;
	public int y;
}