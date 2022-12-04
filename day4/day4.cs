using System.Security.Cryptography.X509Certificates;

part1();
part2();
void part1()
{
	var f = File.OpenText("data.txt");

	int score = 0;

	while (!f.EndOfStream)
	{
		var line = f.ReadLine();

		var elfs = line.Split(',');
		var a = int.Parse(elfs[0].Split('-')[0]);
		var b = int.Parse(elfs[0].Split('-')[1]);
		var c = int.Parse(elfs[1].Split('-')[0]);
		var d = int.Parse(elfs[1].Split('-')[1]);

		if ((a>=c && b <= d) ||( a <= c && b >= d))
		{
			score++;
		}

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

		var elfs = line.Split(',');
		var a = int.Parse(elfs[0].Split('-')[0]);
		var b = int.Parse(elfs[0].Split('-')[1]);
		var c = int.Parse(elfs[1].Split('-')[0]);
		var d = int.Parse(elfs[1].Split('-')[1]);

		if ((a >= c && ! (a>d))
			||
			(c >= a)&&(!(c>b))
			)
		
		{
			score++;
		}

	}


	Console.WriteLine("part2 : " + score);
	f.Close();
}
