var f = File.OpenText("data.txt");

int score = 0;

while (!f.EndOfStream)
{
	var line = f.ReadLine();

	char[] parts = new[] { line[0], line[2] };

	//score for win
	score += (parts[1] - 'X') *3;



	int choice =  ((parts[0] - 'A') + (parts[1] - 'X'-1)+3)%3;

	switch(choice)
	{
		case 0: //rock A
			score+= 1;
			break;
		case 1:// paper B
			score += 2;
			break;
		case 2: //sissors C
			score += 3;
			break;
	}
}

Console.WriteLine(score);