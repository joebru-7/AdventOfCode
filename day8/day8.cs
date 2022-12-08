part1();
part2();
void part1()
{
	var f = File.ReadAllText("data.txt").Split("\r\n");
	//f = "30373\r\n25512\r\n65332\r\n33549\r\n35390".Split("\r\n");

	int width = f[0].Length;
	int height = f.Length;

	bool[,] counted = new bool[width, height];

	int score = 0;

	char maxa = (char)('0'- 1);
	char maxb = (char)('0' - 1);
	
	for (int i = 0; i < height; i++)
	{
		maxa = (char)('0' - 1);
		maxb = (char)('0' - 1);
		for (int j = 0; j < width; j++)
		{
			if(f[i][j] > maxa)
			{
				maxa= f[i][j];
				if (!counted[i, j])
				{
					counted[i, j] = true;
					score++;
				}
			}
			if (f[i][(width-j-1)] > maxb)
			{
				maxb = f[i][width - j-1];
				if (!counted[i, width - j - 1])
				{
					score++;	
					counted[i, width - j - 1] = true;
				}
			}
		} 
	}

	for (int j = 0; j < width; j++)
	{
		maxa = (char)('0' - 1);
		maxb = (char)('0' - 1);
		for (int i = 0; i < height; i++)
		{
			if (f[i][j] > maxa)
			{
				maxa = f[i][j];
				if (!counted[i,j])
				{
					score++;
					counted[i,j] = true;
				}
			}
			if (f[height - i-1][j] > maxb)
			{
				maxb = f[height - i-1][j];
				if (!counted[height - i - 1, j])
				{
					counted[height - i-1,j] = true;
					score++;
				}
			}
		}
	}


	Console.WriteLine("part1 : " + score);
	
}

void part2()
{

	var f = File.ReadAllText("data.txt").Split("\r\n");
	//f = "30373\r\n25512\r\n65332\r\n33549\r\n35390".Split("\r\n");

	int width = f[0].Length;
	int height = f.Length;
	int score = 0;

	
	for (int y = 0; y < height; y++)
	{
		for (int x = 0; x < width; x++)
		{
			var t = CalcultateScore(f,x,y);
			if( t>score) 
				score = t;
		}
	}


	Console.WriteLine("part2 : " + score);
}

int CalcultateScore(string[] arr,int x,int y)
{

	int width = arr[0].Length;
	int height = arr.Length;
	int score = 1;
	// r
	int tmp = 1;
	try
	{
		for (int i = 1; i < width; i++)
			if (arr[y][x+i] < arr[y][x])
				 tmp++;
			else
				break;
	}
	catch (Exception)
	{
		tmp--;
	}
	//l
	score *= tmp;
	tmp = 1;
	try
	{
		for (int i = 1; i < width; i++)
			if (arr[y][x-i] < arr[y][x])
				tmp++;
			else
				break;
	}
	catch (Exception)
	{
		tmp--;
	}
	//d
	score *= tmp;
	tmp = 1;
	try
	{
		for (int i = 1; i < height; i++)
			if (arr[y + i][x] < arr[y][x])
				tmp++;
			else
				break;
	}
	catch (Exception)
	{
		tmp--;
	}
	//u
	score *= tmp;
	tmp = 1;
	try
	{
		for (int i = 1; i < height; i++)
			if (arr[y - i][x] < arr[y][x])
				tmp++;
			else
				break;
	}
	catch (Exception)
	{
		tmp--;
	}
	score *= tmp;
	return score;
}