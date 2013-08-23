using System;

namespace Gwen
{
	public struct Color
	{
		public byte A;
		public byte R;
		public byte G;
		public byte B;

		public static readonly Color White = FromArgb(255, 255, 255);
		public static readonly Color Black = FromArgb(0, 0, 0);
		public static readonly Color Red = FromArgb(255, 0, 0);
		public static readonly Color Green = FromArgb(0, 255, 0);
		public static readonly Color Blue = FromArgb(0, 0, 255);
		public static readonly Color Yellow = FromArgb(255, 255, 0);

		public static Color FromArgb(int red, int green, int blue)
		{
			return FromArgb(255, red, green, blue);
		}

		public static Color FromArgb(int alpha, int red, int green, int blue)
		{
			Color result;
			result.A = (byte)alpha;
			result.R = (byte)red;
			result.G = (byte)green;
			result.B = (byte)blue;
			return result;
		}

		public float GetHue()
		{
			int r = R;
			int g = G;
			int b = B;
			byte minval = (byte) Math.Min (r, Math.Min (g, b));
			byte maxval = (byte) Math.Max (r, Math.Max (g, b));
			
			if (maxval == minval)
				return 0.0f;
			
			float diff = (float)(maxval - minval);
			float rnorm = (maxval - r) / diff;
			float gnorm = (maxval - g) / diff;
			float bnorm = (maxval - b) / diff;
			
			float hue = 0.0f;
			if (r == maxval) 
				hue = 60.0f * (6.0f + bnorm - gnorm);
			if (g == maxval) 
				hue = 60.0f * (2.0f + rnorm - bnorm);
			if (b  == maxval) 
				hue = 60.0f * (4.0f + gnorm - rnorm);
			if (hue > 360.0f) 
				hue = hue - 360.0f;
			
			return hue;
		}
	}
}

