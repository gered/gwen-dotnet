using System;

namespace Gwen
{
	public struct Rectangle
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;

		public static readonly Rectangle Empty;

		public Rectangle(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public int Left
		{
			get
			{
				return X;
			}
		}

		public int Top
		{
			get
			{
				return Y;
			}
		}

		public int Right
		{
			get
			{
				return X + Width;
			}
		}

		public int Bottom
		{
			get
			{
				return Y + Height;
			}
		}
	}
}

