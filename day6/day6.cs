part1();
part2();
void part1()
{
	var f = File.OpenText("data.txt");

	int score = 0;

	var line = f.ReadLine();
	//line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
	char[] last4 = { ' ', ' ', ' ', ' ' };

	for (int i = 0; i < line.Length; last4[i % 4] = line[i],i++)
	{
		bool flag = false;
		foreach (var item in last4)
		{
			if (
				Array.FindAll(last4, (c) => c == item).Length > 1
				)
				{
				flag = true;
				break;
				}
		}
		if (flag)
			continue;

		var las= 	Array.FindLastIndex(last4, (c => c == line[i]));
		var fir = Array.FindIndex(last4, (c => c == line[i]));

		if (las==fir && (las == -1 || las == i%4))
		{
			if (i < 4) continue;
			score = i;
			break;
		}

		last4[i%4] = line[i];
		
	}

	Console.WriteLine("part1 : " + score);
	f.Close();
}

void part2()
{

	var f = File.OpenText("data.txt");

	int score = 0;

	var line = f.ReadLine();
	//line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
	char[] lastx = Enumerable.Repeat(' ', 14).ToArray(); ;

	for (int i = 0; i < line.Length; lastx[i % 14] = line[i], i++)
	{
		bool flag = false;
		foreach (var item in lastx)
		{
			if (
				Array.FindAll(lastx, (c) => c == item).Length > 1
				)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			continue;
		}

		score = i;
		break;
		/*
		var las = Array.FindLastIndex(lastx, (c => c == line[i]));
		var fir = Array.FindIndex(lastx, (c => c == line[i]));

		if (las == fir && (las == -1 || las == i % 14))
		{
			if (i < 14) continue;
			score = i;
			break;
		}
		*/
	}

	Console.WriteLine("part2 : " + score);
	f.Close();
}
