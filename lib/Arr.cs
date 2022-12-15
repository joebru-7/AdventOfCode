using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lib
{
	public class Arr<T>
	{
		T[] data;
		int Count => data.Length;
		int ranks = 0;

		public Arr(int length)
		{
			data = new T[length];
		}

		public Arr(int length,int width)
		{
			data = new T[length * width];
			ranks = width;
		}

		public void fill(T val)
		{
			for (int i = 0; i < Count; i++)
			{
				data[i] = val;
			}
		}

		#region indexers
		public T this[int i]
		{
			get => data[i];
			set => data[i] = value;
		}
		public T this[int x, int y]
		{
			get => data[x + y * ranks];
			set => data[x + y * ranks] = value;
		}

		public T this[Point p]
		{
			get => data[p.x + p.y * ranks];
			set => data[p.x + p.y * ranks] = value;
		}
		#endregion

	}
}
