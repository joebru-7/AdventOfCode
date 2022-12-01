var f = File.OpenText("data.txt");

int maxElfAmount = 0;
int currentElfAmount = 0;

while (!f.EndOfStream)
{
	var line = f.ReadLine();
	if (line == "")
	{
		if (maxElfAmount < currentElfAmount)
			maxElfAmount = currentElfAmount;
		currentElfAmount = 0;
	}
	else
	{
		currentElfAmount += int.Parse(line);
	}
}

Console.WriteLine(maxElfAmount);