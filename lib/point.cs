using System.Data.Common;
using System.Runtime.CompilerServices;

namespace lib
{
	public struct Point
	{
		public int x; public int y;

		public static implicit operator Point((int, int) p)
		{
			return new Point(p.Item1, p.Item2);
		}
		public static implicit operator (int, int)(Point p)
		{
			return (p.x, p.y);
		}

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Point()
		{
			x = 0;
			y = 0;
		}

		public static Point operator +(Point p, Point q)
		{
			return new Point(p.x + q.x, p.y + q.y);
		}

		public static Point operator -(Point p, Point q)
		{
			return new Point(p.x - q.x, p.y - q.y);
		}

		public static bool operator ==(Point p, Point q)
		{
			return p.x == q.x && p.y == q.y;
		}

		public static bool operator !=(Point p, Point q)
		{
			return p.x != q.x || p.y != q.y;
		}

		public bool Equals(Point p)
		{
			return this == p; 
		}

		public override bool Equals(object? obj)
		{
			if (obj == null) 
				return false;
			if (obj is not Point)
				return false;
			return Equals((Point)obj);

		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}
}
