
part1();
part2();

void part1()
{
	var f = File.OpenText("data.txt");

	var sum = 0;

	int X = 1;
	int clock = 0;


	while (!f.EndOfStream)
	{
		var line = f.ReadLine();

		if (line == "noop")
		{
			clock++;
			if (isTime(clock))
				sum += X * clock;
		}
		else if (line.StartsWith("addx"))
		{
			clock++;
			if (isTime(clock))
				sum += X * clock;
			clock++;
			if (isTime(clock))
				sum += X * clock;
			X += int.Parse(line[5..]);

		}

	}


	Console.WriteLine("Part1: " + sum);
}

bool isTime(int clock)
{
	switch(clock)
	{
		case 20:
		case 60:
		case 100:
		case 140:
		case 180:
		case 220:
			return true;
		default:
			return false;
	}
}

void part2()
{
	Console.WriteLine("Part2: ");
	var c = new part2C();
	c.run();
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 40; j++)
		{
			Console.Write(c.screen[i * 40 + j ]);
		}
		Console.Write('\n');
	}
}

class part2C
{
	StreamReader f = File.OpenText("data.txt");

	public char[] screen = Enumerable.Repeat(' ', 240).ToArray();

	int X = 1;
	int clock = 0;

	void tick()
	{
		if (clock%40 == X || clock % 40 == X + 1 || clock % 40 == X - 1)
		{
			screen[clock] = '#';
		}
		clock++;
	}

	public void run()
	{
		while (!f.EndOfStream)
		{
			var line = f.ReadLine();

			if (line == "noop")
			{
				tick();
			}
			else if (line.StartsWith("addx"))
			{
				tick();
				tick();
				X += int.Parse(line[5..]);
			}
		}
	}
}
