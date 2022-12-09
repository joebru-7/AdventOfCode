part1();
part2();

void part1()
{
	var f = File.OpenText("data.txt");

	int score = 0;

	while (!f.EndOfStream)
	{
		var line = f.ReadLine();
	}

	Console.WriteLine("part1 : " + score);
	f.Close();
}

void part2()
{

	var f = File.OpenText("data.txt");

	int score = 0;

	while (!f.EndOfStream)
	{
		var line = f.ReadLine();
	}


	Console.WriteLine("part2 : " + score);
	f.Close();
}
