using System;

namespace Eto.Drawing
{
	public struct Size
	{
		public int Width { get; set; }
		public int Height { get; set; }
		
		public static readonly Size Empty = new Size(0, 0);
		

		public static Size Round(SizeF size)
		{
			return new Size((int)Math.Round(size.Width), (int)Math.Round(size.Height));
		}

		public static Size Truncate(SizeF size)
		{
			return new Size((int)size.Width, (int)size.Height);
		}
		
		public Size(int width, int height)
			: this()
		{
			Width = width;
			Height = height;
		}


		public static Size operator *(Size size1, Size size2)
		{
			Size result = size1;
			result.Width = size1.Width * size2.Width;
			result.Height = size1.Height * size2.Height;
			return result;
		}

		public static Size operator *(Size size1, int multiplier)
		{
			Size result = size1;
			result.Width = size1.Width * multiplier;
			result.Height = size1.Height * multiplier;
			return result;
		}
		
		public static Size operator /(Size size1, Size size2)
		{
			Size result = size1;
			result.Width = size1.Width / size2.Width;
			result.Height = size1.Height / size2.Height;
			return result;
		}

		public static Size operator /(Size size1, int divider)
		{
			Size result = size1;
			result.Width = size1.Width / divider;
			result.Height = size1.Height / divider;
			return result;
		}
		
		public static Size operator +(Size size1, Size size2)
		{
			Size result = size1;
			result.Width = size1.Width + size2.Width;
			result.Height = size1.Height + size2.Height;
			return result;
		}

		public static Size operator +(Size size1, int adder)
		{
			Size result = size1;
			result.Width = size1.Width + adder;
			result.Height = size1.Height + adder;
			return result;
		}
		
		public static bool operator ==(Size size1, Size size2)
		{
			return (size1.Width == size2.Width && size1.Height == size2.Height);
		}

		public static bool operator !=(Size size1, Size size2)
		{
			return (size1.Width != size2.Width || size1.Height != size2.Height);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Size)) return false;
			Size size = (Size)obj;
			return (Width == size.Width && Height == size.Height);
		}

		public override int GetHashCode()
		{
			return Width ^ Height;
		}

		public override string ToString()
		{
			return String.Format("Width={0} Height={1}", Width, Height);
		}


	}
}