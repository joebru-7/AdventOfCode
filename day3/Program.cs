part1();
part2();
void part1()
{ 
	var f = File.OpenText("data.txt");

	int score = 0;

	while (!f.EndOfStream)
	{
		var line = f.ReadLine();
		string a = line[..(line.Length / 2)];
		string b = line[(line.Length / 2)..];
		bool[] usedletters = Enumerable.Repeat(false, 27 * 2).ToArray();

		for (int i = 0; i < a.Length; i++)
		{
			for (int j = 0; j < b.Length; j++)
			{
				if (a[i] == b[j])
				{
					if (char.IsLower(a[i]))
					{
						var letternumber = a[i] - 'a' + 1;
						if (!usedletters[letternumber])
						{
							score += letternumber;
							usedletters[letternumber] = true;
						}
					}
					else
					{
						var letternumber = a[i] - 'A' + 1 +26;
						if (!usedletters[letternumber])
						{
							score += letternumber;
							usedletters[letternumber] = true;
						}
					}
				}
			}
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
		bool[][] usedletters = new bool[3][]; ;
		for (int x = 0; x < 3; x++)
		{
			var line = f.ReadLine();
			usedletters[x] = Enumerable.Repeat(false, 27 * 2).ToArray();

			for (int i = 0; i < line.Length; i++)
			{
				if (char.IsLower(line[i]))
				{
					var letternumber = line[i] - 'a' + 1;
					if (!usedletters[x][letternumber])
					{
						usedletters[x][letternumber] = true;
					}
				}
				else
				{
					var letternumber = line[i] - 'A' + 1 + 26;
					if (!usedletters[x][letternumber])
					{
						usedletters[x][letternumber] = true;
					}
				}
			}
		}

		for (int i = 0; i < usedletters[0].Length; i++)
		{
			if (usedletters[0][i] == true&&
				usedletters[1][i] == true&&
				usedletters[2][i] == true
				)
			{

				score += i;
			}
		}
	}

	Console.WriteLine("part2 : " + score);
	f.Close();
}