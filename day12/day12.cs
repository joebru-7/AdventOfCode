part1();
part2();
void part1()
{
	var lines = File.ReadAllLines("data.txt");
	point end = new();
	point start = new();

	int[,] data = new int[lines[0].Length, lines.Length];

	for (int i = 0; i < lines.Length; i++)
	{
		for (int j = 0; j < lines[i].Length; j++)
		{
			data[j, i] = lines[i][j] - 'a';
			if (lines[i][j] == 'S')
			{
				start.x = j;
				start.y = i;
				data[j, i] = 0;
			}
			else if (lines[i][j] == 'E')
			{
				end.x = j;
				end.y = i;
				data[j, i] = 'z' - 'a';
			}
		}
	}

	List<point> found= new List<point>();
	List<point> explored = new List<point>();
	found.Add(start);

	while(true)
	{
		found.Sort();
		var curr = found[0];
		found.RemoveAt(0);
		explored.Add(curr);

		if (curr.Equals(end))
		{
			end.parent = curr.parent;
			break;
		}

		List<point> neibs = new ();

		foreach (var item in new point[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
		{
			point neb = curr + item;

			if (neb.x < 0 || neb.x >= lines[0].Length || neb.y < 0 || neb.y >= lines.Length)
				continue;

			if (data[neb.x, neb.y] <= 1 + data[curr.x, curr.y])
			{
				if (explored.Contains(neb))
					continue;

				neb.parent = curr;
				neb.dist = curr.dist + 1;
				neb.est = neb.dist +
					(float)Math.Sqrt(
						(end.x - neb.x) * (end.x - neb.x) +
						(end.y - neb.y) * (end.y - neb.y));

				int ind = found.IndexOf(neb);
				if (ind == -1)
					found.Add(neb);
				else
				{
					if (neb.dist < found[ind].dist)
					{
						found[ind] = neb;
					}
				}
			}
		
		}

	}
	int count = 0;
	point curr2 = end;
	while (curr2.parent != null)
	{
		count++;
		curr2 = curr2.parent;
	}

	Console.WriteLine("1: " + count);

}

void part2()
{
	var lines = File.ReadAllLines("data.txt");
	point end = new();
	point start = new();

	int[,] data = new int[lines[0].Length, lines.Length];

	for (int i = 0; i < lines.Length; i++)
	{
		for (int j = 0; j < lines[i].Length; j++)
		{
			data[j, i] = lines[i][j] - 'a';
			if (lines[i][j] == 'S')
			{
				start.x = j;
				start.y = i;
				data[j, i] = 0;
			}
			else if (lines[i][j] == 'E')
			{
				end.x = j;
				end.y = i;
				data[j, i] = 'z' - 'a';
			}
		}
	}

	List<point> found = new List<point>();
	List<point> explored = new List<point>();
	found.Add(end);

	while (true)
	{
		var curr = found[0];
		found.RemoveAt(0);
		explored.Add(curr);

		if (data[curr.x,curr.y] == 0)
		{
			start.parent = curr.parent;
			break;
		}

		List<point> neibs = new();

		foreach (var item in new point[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
		{
			point neb = curr + item;

			if (neb.x < 0 || neb.x >= lines[0].Length || neb.y < 0 || neb.y >= lines.Length)
				continue;

			if (data[neb.x, neb.y] + 1 >=  data[curr.x, curr.y])
			{
				if (explored.Contains(neb))
					continue;

				neb.parent = curr;
				neb.dist = curr.dist + 1;
				/*neb.est = neb.dist +
					(float)Math.Sqrt(
						(end.x - neb.x) * (end.x - neb.x) +
						(end.y - neb.y) * (end.y - neb.y));
				*/
				int ind = found.IndexOf(neb);
				if (ind == -1)
					found.Add(neb);
				else
				{
					if (neb.dist < found[ind].dist)
					{
						found[ind] = neb;
					}
				}
			}

		}

	}
	int count = 0;
	point curr2 = start;
	while (curr2.parent != null)
	{
		count++;
		curr2 = curr2.parent;
	}

	Console.WriteLine("2: " + count);

}
class point : IComparable<point>
{
	public int x; public int y;
	public int dist = 0;
	public float est;
	public point parent;

	public point(int x, int y, int dist, float est, point parent = null)
	{
		this.x = x;
		this.y = y;
		this.dist = dist;
		this.est = est;
		this.parent = parent;
	}

	public static implicit operator point((int,int) p)
	{
		return new point( p.Item1,p.Item2);
	}
	public static implicit operator (int,int)(point p)
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

	public static point operator+(point p,point q)
	{
		return new point(p.x+q.x,p.y+q.y);
	}

	public static bool operator<(point p1, point p2) => p1.est < p2.est;
	public static bool operator>(point p1, point p2) => p1.est > p2.est;

	public int CompareTo(point obj) =>
		 (int)(est - obj.est) * 100;

	public override bool Equals(object? obj)
	{
		return obj is point point &&
			   x == point.x &&
			   y == point.y;
	}
}