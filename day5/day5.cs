Stack<char>[] stacks = { 
	new(), new(), new(),
	new(), new(), new(), 
	new(), new(), new(), 
	new()};
Stack<char> tmp = new();

part1();
part2();

void setupStacks(List<string> setuplines)
{
	int botindex = -1;
	while(++botindex < setuplines[^1].Length)
	{
		if (setuplines[^1][botindex] == ' ')
		{
			continue;
		}
		else
		{
			int stacknum = -'0'+setuplines[^1][botindex];
			for (int i = setuplines.Count - 1-1; i >= 0; i--)
			{
				char letter = setuplines[i][botindex];

				if (letter == ' ')
					break;

				stacks[stacknum].Push(letter);
			}
		}
	}
}

void rearrangestacks(string instruction)
{
	int amount=0, from=0, to=0;
	int index = 0;


	amount = int.Parse(instruction[5..7]);
	from = int.Parse(instruction[12..14]);
	to = int.Parse(instruction[17..]);

	for (int i = 0; i < amount; i++)
	{
		stacks[to].Push(stacks[from].Pop());
	}
}

void rearrangestacks2(string instruction)
{
	int amount = 0, from = 0, to = 0;
	int index = 0;


	amount = int.Parse(instruction[5..7]);
	from = int.Parse(instruction[12..14]);
	to = int.Parse(instruction[17..]);

	for (int i = 0; i < amount; i++)
	{
		tmp.Push(stacks[from].Pop());
	}
	for (int i = 0; i < amount; i++)
	{
		stacks[to].Push(tmp.Pop());
	}

}

void clearStacks()
{
	stacks = new Stack<char>[]{
		new(), new(), new(),
	new(), new(), new(), 
	new(), new(), new(), 
	new()};
}
void part1()
{
	clearStacks();

	var f = File.OpenText("data.txt");

	List<string> setuplines = new();

	string line;
	while ((line = f.ReadLine()) != "")
		setuplines.Add(line);

	setupStacks(setuplines);


	int score = 0;

	while (!f.EndOfStream)
	{
		line = f.ReadLine();
		rearrangestacks(line);
	}

	Console.Write("part1 : ");
	for (int i = 1; i < stacks.Length; i++)
	{
		Console.Write(stacks[i].Peek());
	}
	Console.Write('\n');
	f.Close();
}

void part2()
{

	clearStacks();

	var f = File.OpenText("data.txt");

	List<string> setuplines = new();

	string line;
	while ((line = f.ReadLine()) != "")
		setuplines.Add(line);

	setupStacks(setuplines);


	int score = 0;

	while (!f.EndOfStream)
	{
		line = f.ReadLine();
		rearrangestacks2(line);
	}

	Console.Write("part1 : ");
	for (int i = 1; i < stacks.Length; i++)
	{
		Console.Write(stacks[i].Peek());
	}
	Console.Write('\n');
	f.Close();
}