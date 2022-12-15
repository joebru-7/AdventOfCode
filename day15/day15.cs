using lib;

//29:27.58

part1();
part2();

void part1()
{
	var Lines = File.ReadAllLines("data.txt");
	List<sensor> sensors = new List<sensor>();

	int max = int.MinValue;
	int min = int.MaxValue;

	foreach (var l in Lines)
	{
		var parts = l.Split(':');
		Point pos = new(int.Parse(parts[0].Split(' ')[2][2..^1]), int.Parse(parts[0].Split(' ')[3][2..]));

		Point Beacon = new(int.Parse(l.Split(' ')[8][2..^1]), int.Parse(l.Split(' ')[9][2..]));

		int dist = Math.Abs(pos.x-Beacon.x) + Math.Abs(pos.y-Beacon.y);

		sensors.Add(new sensor
		{
			pos = pos,
			distance = dist
		});

		if(pos.x > max)
			max = pos.x;
		if(pos.x < min)
			min = pos.x;
		if(Beacon.x > max)
			max = Beacon.x;
		if (Beacon.x < min)
			min = Beacon.x;
	}

	int count = 0;
	for (int x = min; x < max; x++)
	{
		bool inrange = false;
		Point test = (x, 2000000);
		foreach (var sens in sensors)
		{
			int dist = Math.Abs(sens.pos.x - test.x) + Math.Abs(sens.pos.y - test.y);

			if (dist < sens.distance)
			{
				inrange = true;
				break;
			}
		}
		if (!inrange)
		{
			count++;
		}
	
	}
	Console.WriteLine("part1: " + count);

}
void part2()
{

}
class sensor
{
	public Point pos;
	public int distance;
}

