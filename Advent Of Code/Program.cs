var f = File.OpenText("data.txt");

int[] maxElfAmount = new int[] { 0, 0, 0 };
int currentElfAmount = 0;

while (!f.EndOfStream)
{
	var line = f.ReadLine();
	if (line == "")
	{
		if (maxElfAmount[0] < currentElfAmount)
		{
			maxElfAmount[0] = currentElfAmount;
			Array.Sort(maxElfAmount);
		}
		currentElfAmount = 0;
	}
	else
	{
		currentElfAmount += int.Parse(line);
	}
}

Console.WriteLine(maxElfAmount[0] + maxElfAmount[1] + maxElfAmount[2]);