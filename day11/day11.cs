using System.Numerics;

part1();
part2();
//part1fun();
void part1()
{
	List<monkey1> monkeys = new List<monkey1>();

	var f = File.ReadAllText("data.txt").Split("\r\n\r\n");

	foreach (string item in f)
	{
		monkeys.Add(new monkey1(item));
	}

	for (int i = 0; i < 20; i++)
	{
		foreach (monkey1 monke in monkeys)
		{
			monke.throwAllItems(monkeys);
		}
	}

	int[] maxs = { -1, -1 };
	foreach (monkey1 monke in monkeys)
	{
		if (monke.throws > maxs[0])
		{
			maxs[0] = monke.throws;
			Array.Sort(maxs);
		}
	}

	Console.WriteLine("part1: " + (maxs[0] * maxs[1]));
}

void part2()
{
	List<monkey2> monkeys = new List<monkey2>();

	var f = File.ReadAllText("data.txt").Split("\r\n\r\n");

	foreach (string item in f)
	{
		monkeys.Add(new monkey2(item));
	}

	for (int i = 0; i < 10000; i++)
	{
		if (i % 100 == 0)
		{
			Console.WriteLine(i);
		}
		foreach (monkey2 monke in monkeys)
		{
			monke.throwAllItems(monkeys);
		}
	}

	BigInteger[] maxs = { -1, -1 };
	foreach (monkey2 monke in monkeys)
	{
		if (monke.throws > maxs[0])
		{
			maxs[0] = monke.throws;
			Array.Sort(maxs);
		}
	}

	Console.WriteLine("part2: " + (maxs[0] * maxs[1]));

}

void part1fun()
{

	var monkeys = new List<ThrowingMonkey>();

	var f = File.ReadAllText("data.txt").Split("\r\n\r\n");

	foreach (string item in f)
	{
		monkeys.Add(new ThrowingMonkey(item));
	}

	for (int i = 0; i < 20; i++)
	{
		foreach (var monke in monkeys)
		{
			monke.throwAllItems(monkeys);
		}
	}

	int[] maxs = { -1, -1 };
	foreach (var monke in monkeys)
	{
		if (monke.throws > maxs[0])
		{
			maxs[0] = monke.throws;
			Array.Sort(maxs);
		}
	}

	Console.WriteLine("partfun: " + (maxs[0] * maxs[1]));
}

class ThrowingMonkey
{
	Queue<int> items = new();
	Func<int, int> operation;
	Func<int, int> test;
	int ToThrow;

	public int throws = 0;
	public ThrowingMonkey(string item)
	{
		var lines = item.Split("\r\n");
		lines[1] = lines[1][17..];
		var stuff = lines[1].Split(',');
		foreach (var x in stuff)
		{
			items.Enqueue(int.Parse(x));
		}

		var expr = lines[2][23..];
		operation = (n) =>
		{
			int add(int a, int b) => a + b;
			int mult(int a, int b) => a * b;

			Func<int, int, int> op = expr[0] == '+' ? add : mult;

			if (expr.Contains("old"))
			{
				return op(n, n);
			}

			return op(n, int.Parse(expr[1..]));
		};

		test = (n) =>
		{
			if (n % int.Parse(lines[3][21..]) == 0)
			{
				return int.Parse(lines[4][29..]);
			}
			else
			{
				return int.Parse(lines[5][29..]);
			}
		};
	}

	internal void throwAllItems(List<ThrowingMonkey> monkeys)
	{
		while (items.Count > 0)
		{
			throws++;
			var curr = items.Dequeue();
			curr = operation(curr);
			curr = curr / 3;
			ToThrow = curr;
			monkeys[test(curr)].ready(this);
		}
	}

	void Throw()
	{
		throw new Exception(ToThrow.ToString());
	}

	void ready(ThrowingMonkey m)
	{
		try
		{
			m.Throw();
		}
		catch (Exception e)
		{
			items.Enqueue(int.Parse(e.Message));
		}
	}
}



class monkey1
{
	Queue<int> items = new();
	Func<int, int> operation;
	Func<int, int> test;

	public int throws = 0;
	public monkey1(string item)
	{
		var lines = item.Split("\r\n");
		lines[1] = lines[1][17..];
		var stuff = lines[1].Split(',');
		foreach (var x in stuff)
		{
			items.Enqueue(int.Parse(x));
		}

		var expr = lines[2][23..];
		operation = (n) =>
		{
			int add(int a, int b) => a + b;
			int mult(int a, int b) => a * b;

			Func<int, int, int> op = expr[0] == '+' ? add : mult;

			if (expr.Contains("old"))
			{
				return op(n, n);
			}

			return op(n, int.Parse(expr[1..]));
		};

		test = (n) =>
		{
			if (n % int.Parse(lines[3][21..]) == 0)
			{
				return int.Parse(lines[4][29..]);
			}
			else { 
				return int.Parse(lines[5][29..]);
			}
		};
	}

	internal void throwAllItems(List<monkey1> monkeys)
	{
		while(items.Count > 0)
		{
			throws++;
			var curr = items.Dequeue();
			curr = operation(curr);
			curr = curr / 3;
			monkeys[test(curr)].catchItem(curr);
		}
	}

	void catchItem(int item)
	{
		items.Enqueue((int)item);
	}
}

class monkey2
{
	public struct Thing
	{
		public BigInteger worry = 0;
		static int amt = 0;
		public int index = amt++;

		public Thing()
		{
		}

		public Thing(BigInteger worry)
		{
			this.worry = worry;
		}
		Thing(BigInteger worry, int index)
		{
			this.worry = worry;
			this.index = index;
		}
		public static bool operator==(Thing x, Thing y)
		{
			return x.index == y.index;
		}
		public static bool operator !=(Thing x, Thing y)
		{
			return x.index != y.index;
		}

		public static Thing add(Thing a,BigInteger b)
		{
			return new Thing((a.worry + b) % monkey2.gcm, a.index);
		}
		public static Thing mult(Thing a, BigInteger b)
		{
			return new Thing((a.worry * b) % monkey2.gcm , a.index);
		}
	}

	public List<Thing> items = new();
	Func<Thing, Thing> operation;
	Func<Thing, int> test;

	public BigInteger throws = 0;

	static BigInteger gcm = 1;
	BigInteger modamt;
	public monkey2(string item)
	{	
		var lines = item.Split("\r\n");
		lines[1] = lines[1][17..];
		var stuff = lines[1].Split(',');
		foreach (var x in stuff)
		{
			items.Add(new Thing(BigInteger.Parse(x)));
		}

		var expr = lines[2][23..];
		operation = (n) =>
		{

			Func<Thing, BigInteger, Thing> op = expr[0] == '+' ? Thing.add : Thing.mult;

			if (expr.Contains("old"))
			{
				return op(n, n.worry);
			}

			return op(n, BigInteger.Parse(expr[1..]));
		};

		modamt = int.Parse(lines[3][21..]);
		monkey2.gcm *= modamt;
		test = (n) =>
		{
			if (n.worry %  modamt == 0)
			{
				return int.Parse(lines[4][29..]);
			}
			else
			{
				return int.Parse(lines[5][29..]);
			}
		};
	}

	monkey2() { }

	public monkey2 copy()
	{
		var ret = new monkey2();
		ret.test = test;
		ret.operation = operation;
		ret.throws = throws;
		
		ret.items = new List<Thing>();
		foreach (var item in items)
		{
			ret.items.Add(item);
		}
		


		return ret;
			
	}

	public void throwAllItems(List<monkey2> monkeys)
	{
		throws += items.Count;
		Parallel.For(0, items.Count, (i) =>
		{
			Thing curr = items[i];
			curr = operation(curr);
			items[i] = curr;
		});
		foreach (var curr in items)
		{
			monkeys[test(curr)].catchItem(curr);
		}
		items.Clear();

	}

	void catchItem(Thing item)
	{
		items.Add(item);
	}
}