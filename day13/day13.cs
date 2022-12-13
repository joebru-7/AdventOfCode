//1:24:26
using System.Data;

part1();
//1:33:07
part2();

void part1()
{
	var f = File.OpenText("data.txt");
	int index = 0;
	int score = 0;

	while(!f.EndOfStream)
	{
		index++;
		message a = new(f.ReadLine());
		message b = new(f.ReadLine());
		f.ReadLine();

		if (a.CompareTo(b)<0)
		{
			score += index;
		}
	}
	Console.WriteLine(score);

}

void part2()
{
	var f = File.OpenText("data.txt");
	int index = 0;

	List<message> msg = new();
	message a = new("[[2]]");
	message b = new("[[6]]");


	msg.Add(a);
	msg.Add(b);

	while (!f.EndOfStream)
	{
		index++;
		msg.Add(new(f.ReadLine()));
		msg.Add(new(f.ReadLine()));
		f.ReadLine();
	}

	msg.Sort();

	int inda = msg.FindIndex((m) => m.CompareTo(a) == 0);
	int indb = msg.FindIndex((m) => m.CompareTo(b) == 0);

	Console.WriteLine((inda+1)*(1+indb));
}

class message : IComparable<message>
{
	public message(string? s)
	{
		if (s == null)
			throw new NotImplementedException();

		if (!(isList = s.StartsWith('[')))
		{
			val = int.Parse(s);
		}
		else
		{
			list = new();
			s = s[1..^1];
			if (s == "") return;
			var parts = s.Split(',');
			for (int i = 0; i < parts.Length; i++)
			{
				if ( ! parts[i].StartsWith('['))
				{
					list.Add(new(parts[i]));
				}
				else
				{
					int start = i;
					int count = 0;
					do 
					{
						if (parts[i].StartsWith('['))
							count += 1 + parts[i].LastIndexOf('[');
						if (parts[i].EndsWith(']'))
							count -= parts[i].Length - parts[i].TrimEnd(']').Length;
						i++;
					} 
					while(count > 0 );
					list.Add(new(string.Join(',', parts[start..i])));
					i--;
				}
			}
		}
	}

	public int CompareTo(message? b)
	{
		if (b == null) throw new Exception();
		var a = this;
		if (a.isList != b.isList)
		{
			if (a.isList)
				return a.CompareTo(b.ToList());
			else
				return a.ToList().CompareTo(b);
		}
		if (!a.isList /*&& !b.isList*/)
		{
			return a.val - b.val;
		}
		else
		{
			if (a.isList && b.isList)
			{
				int minlen = Math.Min(a.list.Count, b.list.Count);
				for (int i = 0; i < minlen; i++)
				{
					var cmp = a.list[i].CompareTo(b.list[i]);
					if (cmp != 0)
					{
						return cmp;
					}

				}
				return a.list.Count.CompareTo(b.list.Count);
			}
		}
		throw new Exception();
	}
	public message()
	{
	}

	message ToList()
	{
		return new message
		{
			isList = true,
			list = new(){ this }
		};
	}

	int val;
	bool isList;
	List<message>? list;
}