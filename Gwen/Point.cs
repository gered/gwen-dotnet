using System;

namespace Gwen
{
	[Serializable]
	public struct Point
	{
		public int X;
		public int Y;

		public static readonly Point Empty;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}

