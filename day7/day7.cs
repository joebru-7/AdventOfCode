part1();
part2();

myDirectory buildFilestructure()
{
	var f = File.OpenText("data.txt");
	//build dirs

	myDirectory root = new myDirectory("/");
	myDirectory current = root;

	var line = f.ReadLine();
	while (!f.EndOfStream)
	{
		if (line == "$ cd ..")
		{
			current = current.parent;
		}
		else if (line == @"$ cd /")
		{
			current = root;
		}
		else if (line.StartsWith("$ cd "))
		{
			current = current.EnterOrAdd(line[5..]);
		}
		else if (line == "$ ls")
		{
			while (!((line = f.ReadLine())?.StartsWith('$') ?? true))
			{
				if (line.StartsWith("dir"))
				{
					current.AddDirectory(new myDirectory(line[4..], current));
				}
				else
				{
					int size = int.Parse(line.Split(' ')[0]);
					string name = line.Split(' ')[1];
					current.AddFile(new myFile(name, size));
				}
			}
			continue;
		}

		line = f.ReadLine();
	}
	f.Close();
	return root;
}
void part1()
{

	myDirectory root = buildFilestructure();
	//get sizes
	int accumulator = 0;
	root.getSize();
	
	root.Addsizesinrange(0,100000,ref accumulator);

	Console.WriteLine("part1 : " + accumulator);
	
}

void part2()
{

	myDirectory root = buildFilestructure();

	int totalSize = 70000000;
	int neededSize = 30000000;

	int freesize = totalSize - root.getSize();
	int need2Free = neededSize - freesize;

	int best = int.MaxValue;
	root.getSizeOfSmalllestDirectoryBiggerThan(need2Free, ref best);


	Console.WriteLine("part2 : " + best);
}



class myDirectory
{
	public myDirectory(string name)
	{
		this.name = name;
	}
	public myDirectory(string name,myDirectory parent)
	{
		this.name = name;
		this.parent = parent;
	}
	public string name;
	public List<myDirectory> children = new();
	public myDirectory parent;

	public List<myFile> myFiles = new();
	public int? size = null;

	public void AddFile(myFile file)
	{
		//myFiles ??= new List<myFile>();
		myFiles.Add(file);
	}

	public myDirectory EnterOrAdd(string name)
	{
		foreach (var d in children)
		{
			if (d.name == name)
			{
				return d;
			}
		}
		return new myDirectory(name, this);

	}
	public void AddDirectory(myDirectory d)
	{
		children ??= new List<myDirectory>();
		children.Add(d);
	}
	public int getSize()
	{
		if(size != null)
		{
			return (int)size;
		}

		int sum = 0;
		foreach (var c in children)
		{
			sum += c.getSize();
		}
		foreach (var f in myFiles)
		{
			sum += f.size;
		}
		size = sum;
		return sum;
	}

	public void Addsizesinrange(int min,int max,ref int acc)
	{
		if (getSize() >= min && (int)size <= max)
		{
			acc += (int)size;
		}
		foreach (var child in children)
		{
			child.Addsizesinrange(min, max, ref acc);
		}
	}

	public void getSizeOfSmalllestDirectoryBiggerThan(int size,ref int currentBest)
	{
		if ((getSize() >= size && (int)size <= currentBest))
		{
			currentBest = (int)this.size;
		}
		foreach (var child in children)
		{
			child.getSizeOfSmalllestDirectoryBiggerThan(size,ref currentBest);
		}


	}

}

class myFile
{
	public myFile(string name,int size)
	{
		this.name = name;
		this.size = size;
	}
	public string name;
	public int size;
}