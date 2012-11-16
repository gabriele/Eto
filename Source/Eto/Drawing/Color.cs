using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Eto.Drawing
{
	/// <summary>
	/// Represents a color with RGBA (Red, Green, Blue, and Alpha) components
	/// </summary>
	[TypeConverter (typeof (ColorConverter))]
	public struct Color : IEquatable<Color>
	{
		// static members for mapping color names from the Colors class
		static Dictionary<string, Color> colormap;
		static object colormaplock = new object ();

		#region Obsolete

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete("Use Colors.Black")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.White")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.Gray")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.DarkGray")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.Red")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.Lime")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.Blue")]

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("User Colors.Transparent")]
		public static readonly Color Transparent = new Color (0, 0, 0, 0);

		/// <summary>
		/// An empty color with zero for all components
		/// </summary>
		#pragma warning disable 618
		[Obsolete("Use nullable values instead of empty color structs")]
		public static readonly Color Empty = new Color { IsEmpty = true };
		#pragma warning restore 618

		/// <summary>
		/// Obsolete, do not use.
		/// </summary>
		[Obsolete ("Use ColorCMYK.ToColor() or implicit conversion")]
		public Color (ColorCMYK cmyk)
			: this (cmyk.ToColor ())
		{
		}

		/// <summary>
		/// Obsolete, do not use.
		/// </summary>
		[Obsolete ("Use ColorHSL.ToColor() or implicit conversion")]
		public Color (ColorHSL hsl)
			: this (hsl.ToColor ())
		{
		}

		/// <summary>
		/// Obsolete, do not use.
		/// </summary>
		[Obsolete ("Use ColorHSB.ToColor() or implicit conversion")]
		public Color (ColorHSB hsb)
			: this (hsb.ToColor ())
		{
		}

		/// <summary>
		/// Obsolete. Do not use.
		/// </summary>
		[Obsolete ("Use nullable values instead")]
		public bool IsEmpty
		{
			get;
			private set;
		}

		#endregion

		/// <summary>
		/// Gets or sets the alpha/opacity (0-1)
		/// </summary>
		public float A { get; set; }
		
		/// <summary>
		/// Gets or sets the red component (0-1)
		/// </summary>
		public float R { get; set; }

		/// <summary>
		/// Gets or sets the green (0-1)
		/// </summary>
		public float G { get; set; }

		/// <summary>
		/// Gets or sets the blue (0-1)
		/// </summary>
		public float B { get; set; }

        public static readonly Color InactiveCaption = new Color (191, 205, 219);
        public static readonly Color InactiveCaptionText = new Color (67, 78, 84);
        public static readonly Color Info = new Color (255, 255, 225);
        public static readonly Color InfoText = new Color (0, 0, 0);
        public static readonly Color Menu = new Color (240, 240, 240);
        public static readonly Color MenuText = new Color (0, 0, 0);
        public static readonly Color ScrollBar = new Color (200, 200, 200);
        public static readonly Color Window = new Color (255, 255, 255);
        public static readonly Color WindowFrame = new Color (100, 100, 100);
        public static readonly Color WindowText = new Color (0, 0, 0);
        public static readonly Color AliceBlue = new Color (240, 248, 255);
        public static readonly Color AntiqueWhite = new Color (250, 235, 215);
        public static readonly Color Aqua = new Color (0, 255, 255);
        public static readonly Color Aquamarine = new Color (127, 255, 212);
        public static readonly Color Azure = new Color (240, 255, 255);
        public static readonly Color Beige = new Color (245, 245, 220);
        public static readonly Color Bisque = new Color (255, 228, 196);
        public static readonly Color Black = new Color (0, 0, 0);
        public static readonly Color BlanchedAlmond = new Color (255, 235, 205);
        public static readonly Color Blue = new Color (0, 0, 255);
        public static readonly Color BlueViolet = new Color (138, 43, 226);
        public static readonly Color Brown = new Color (165, 42, 42);
        public static readonly Color BurlyWood = new Color (222, 184, 135);
        public static readonly Color CadetBlue = new Color (95, 158, 160);
        public static readonly Color Chartreuse = new Color (127, 255, 0);
        public static readonly Color Chocolate = new Color (210, 105, 30);
        public static readonly Color Coral = new Color (255, 127, 80);
        public static readonly Color CornflowerBlue = new Color (100, 149, 237);
        public static readonly Color Cornsilk = new Color (255, 248, 220);
        public static readonly Color Crimson = new Color (220, 20, 60);
        public static readonly Color Cyan = new Color (0, 255, 255);
        public static readonly Color DarkBlue = new Color (0, 0, 139);
        public static readonly Color DarkCyan = new Color (0, 139, 139);
        public static readonly Color DarkGoldenrod = new Color (184, 134, 11);
        public static readonly Color DarkGray = new Color (169, 169, 169);
        public static readonly Color DarkGreen = new Color (0, 100, 0);
        public static readonly Color DarkKhaki = new Color (189, 183, 107);
        public static readonly Color DarkMagenta = new Color (139, 0, 139);
        public static readonly Color DarkOliveGreen = new Color (85, 107, 47);
        public static readonly Color DarkOrange = new Color (255, 140, 0);
        public static readonly Color DarkOrchid = new Color (153, 50, 204);
        public static readonly Color DarkRed = new Color (139, 0, 0);
        public static readonly Color DarkSalmon = new Color (233, 150, 122);
        public static readonly Color DarkSeaGreen = new Color (143, 188, 139);
        public static readonly Color DarkSlateBlue = new Color (72, 61, 139);
        public static readonly Color DarkSlateGray = new Color (47, 79, 79);
        public static readonly Color DarkTurquoise = new Color (0, 206, 209);
        public static readonly Color DarkViolet = new Color (148, 0, 211);
        public static readonly Color DeepPink = new Color (255, 20, 147);
        public static readonly Color DeepSkyBlue = new Color (0, 191, 255);
        public static readonly Color DimGray = new Color (105, 105, 105);
        public static readonly Color DodgerBlue = new Color (30, 144, 255);
        public static readonly Color Firebrick = new Color (178, 34, 34);
        public static readonly Color FloralWhite = new Color (255, 250, 240);
        public static readonly Color ForestGreen = new Color (34, 139, 34);
        public static readonly Color Fuchsia = new Color (255, 0, 255);
        public static readonly Color Gainsboro = new Color (220, 220, 220);
        public static readonly Color GhostWhite = new Color (248, 248, 255);
        public static readonly Color Gold = new Color (255, 215, 0);
        public static readonly Color Goldenrod = new Color (218, 165, 32);
        public static readonly Color Gray = new Color (128, 128, 128);
        public static readonly Color Green = new Color (0, 128, 0);
        public static readonly Color GreenYellow = new Color (173, 255, 47);
        public static readonly Color Honeydew = new Color (240, 255, 240);
        public static readonly Color HotPink = new Color (255, 105, 180);
        public static readonly Color IndianRed = new Color (205, 92, 92);
        public static readonly Color Indigo = new Color (75, 0, 130);
        public static readonly Color Ivory = new Color (255, 255, 240);
        public static readonly Color Khaki = new Color (240, 230, 140);
        public static readonly Color Lavender = new Color (230, 230, 250);
        public static readonly Color LavenderBlush = new Color (255, 240, 245);
        public static readonly Color LawnGreen = new Color (124, 252, 0);
        public static readonly Color LemonChiffon = new Color (255, 250, 205);
        public static readonly Color LightBlue = new Color (173, 216, 230);
        public static readonly Color LightCoral = new Color (240, 128, 128);
        public static readonly Color LightCyan = new Color (224, 255, 255);
        public static readonly Color LightGoldenrodYellow = new Color (250, 250, 210);
        public static readonly Color LightGray = new Color (211, 211, 211);
        public static readonly Color LightGreen = new Color (144, 238, 144);
        public static readonly Color LightPink = new Color (255, 182, 193);
        public static readonly Color LightSalmon = new Color (255, 160, 122);
        public static readonly Color LightSeaGreen = new Color (32, 178, 170);
        public static readonly Color LightSkyBlue = new Color (135, 206, 250);
        public static readonly Color LightSlateGray = new Color (119, 136, 153);
        public static readonly Color LightSteelBlue = new Color (176, 196, 222);
        public static readonly Color LightYellow = new Color (255, 255, 224);
        public static readonly Color Lime = new Color (0, 255, 0);
        public static readonly Color LimeGreen = new Color (50, 205, 50);
        public static readonly Color Linen = new Color (250, 240, 230);
        public static readonly Color Magenta = new Color (255, 0, 255);
        public static readonly Color Maroon = new Color (128, 0, 0);
        public static readonly Color MediumAquamarine = new Color (102, 205, 170);
        public static readonly Color MediumBlue = new Color (0, 0, 205);
        public static readonly Color MediumOrchid = new Color (186, 85, 211);
        public static readonly Color MediumPurple = new Color (147, 112, 219);
        public static readonly Color MediumSeaGreen = new Color (60, 179, 113);
        public static readonly Color MediumSlateBlue = new Color (123, 104, 238);
        public static readonly Color MediumSpringGreen = new Color (0, 250, 154);
        public static readonly Color MediumTurquoise = new Color (72, 209, 204);
        public static readonly Color MediumVioletRed = new Color (199, 21, 133);
        public static readonly Color MidnightBlue = new Color (25, 25, 112);
        public static readonly Color MintCream = new Color (245, 255, 250);
        public static readonly Color MistyRose = new Color (255, 228, 225);
        public static readonly Color Moccasin = new Color (255, 228, 181);
        public static readonly Color NavajoWhite = new Color (255, 222, 173);
        public static readonly Color Navy = new Color (0, 0, 128);
        public static readonly Color OldLace = new Color (253, 245, 230);
        public static readonly Color Olive = new Color (128, 128, 0);
        public static readonly Color OliveDrab = new Color (107, 142, 35);
        public static readonly Color Orange = new Color (255, 165, 0);
        public static readonly Color OrangeRed = new Color (255, 69, 0);
        public static readonly Color Orchid = new Color (218, 112, 214);
        public static readonly Color PaleGoldenrod = new Color (238, 232, 170);
        public static readonly Color PaleGreen = new Color (152, 251, 152);
        public static readonly Color PaleTurquoise = new Color (175, 238, 238);
        public static readonly Color PaleVioletRed = new Color (219, 112, 147);
        public static readonly Color PapayaWhip = new Color (255, 239, 213);
        public static readonly Color PeachPuff = new Color (255, 218, 185);
        public static readonly Color Peru = new Color (205, 133, 63);
        public static readonly Color Pink = new Color (255, 192, 203);
        public static readonly Color Plum = new Color (221, 160, 221);
        public static readonly Color PowderBlue = new Color (176, 224, 230);
        public static readonly Color Purple = new Color (128, 0, 128);
        public static readonly Color Red = new Color (255, 0, 0);
        public static readonly Color RosyBrown = new Color (188, 143, 143);
        public static readonly Color RoyalBlue = new Color (65, 105, 225);
        public static readonly Color SaddleBrown = new Color (139, 69, 19);
        public static readonly Color Salmon = new Color (250, 128, 114);
        public static readonly Color SandyBrown = new Color (244, 164, 96);
        public static readonly Color SeaGreen = new Color (46, 139, 87);
        public static readonly Color SeaShell = new Color (255, 245, 238);
        public static readonly Color Sienna = new Color (160, 82, 45);
        public static readonly Color Silver = new Color (192, 192, 192);
        public static readonly Color SkyBlue = new Color (135, 206, 235);
        public static readonly Color SlateBlue = new Color (106, 90, 205);
        public static readonly Color SlateGray = new Color (112, 128, 144);
        public static readonly Color Snow = new Color (255, 250, 250);
        public static readonly Color SpringGreen = new Color (0, 255, 127);
        public static readonly Color SteelBlue = new Color (70, 130, 180);
        public static readonly Color Tan = new Color (210, 180, 140);
        public static readonly Color Teal = new Color (0, 128, 128);
        public static readonly Color Thistle = new Color (216, 191, 216);
        public static readonly Color Tomato = new Color (255, 99, 71);
        public static readonly Color Turquoise = new Color (64, 224, 208);
        public static readonly Color Violet = new Color (238, 130, 238);
        public static readonly Color Wheat = new Color (245, 222, 179);
        public static readonly Color White = new Color (255, 255, 255);
        public static readonly Color WhiteSmoke = new Color (245, 245, 245);
        public static readonly Color Yellow = new Color (255, 255, 0);
        public static readonly Color YellowGreen = new Color (154, 205, 50);
        public static readonly Color ButtonFace = new Color (240, 240, 240);
        public static readonly Color ButtonHighlight = new Color (255, 255, 255);
        public static readonly Color ButtonShadow = new Color (160, 160, 160);
        public static readonly Color GradientActiveCaption = new Color (185, 209, 234);
        public static readonly Color GradientInactiveCaption = new Color (215, 228, 242);
        public static readonly Color MenuBar = new Color (240, 240, 240);
        public static readonly Color MenuHighlight = new Color (51, 153, 255);

		/// <summary>
		/// Creates a Color from a 32-bit ARGB value
		/// </summary>
		/// <param name="argb">32-bit ARGB value with Alpha in the high byte</param>
		/// <returns>A new instance of the Color object with the specified color</returns>
        public static Color FromArgb(int r, int g, int b, int a = 255)
        {
            return new Color(alpha: a / 255f, red: r / 255f, green: g / 255f, blue: b / 255f);
        }

		public static Color FromArgb (uint argb)
		{
			return new Color (((argb >> 16) & 0xff) / 255f, ((argb >> 8) & 0xff) / 255f, (argb & 0xff) / 255f, ((argb >> 24) & 0xff) / 255f);
		}

		/// <summary>
		/// Creates a Color with a specified value for the Red, Green, and Blue components
		/// </summary>
		/// <param name="val">Value for each RGB component</param>
		/// <param name="alpha">Alpha value</param>
		/// <returns>A new instance of the Color object with the specified grayscale color</returns>
		public static Color FromGrayscale (float val, float alpha = 1f)
		{
			return new Color (val, val, val, alpha);
		}

		/// <summary>
		/// Calculates the distance of the two colors in the RGB scale
		/// </summary>
		/// This is useful for comparing two different color values to determine if they are similar.
		/// 
		/// Typically though, <see cref="ColorHSL.Distance"/> gives the best result instead of using the RGB method.
		/// <param name="value1">First color to compare</param>
		/// <param name="value2">Second color to compare with</param>
		/// <returns>The overall distance/difference between the two colours. A lower value indicates a closer match</returns>
		public static float Distance (Color value1, Color value2)
		{
			return (float)Math.Sqrt (Math.Pow (value1.R - value2.R, 2) + Math.Pow (value1.G - value2.G, 2) + Math.Pow (value1.B - value2.B, 2));
		}

		/// <summary>
		/// Initializes a new instance of the Color object with the specified red, green, blue, and alpha components
		/// </summary>
		/// <param name="red">Red component (0-1)</param>
		/// <param name="green">Green component (0-1)</param>
		/// <param name="blue">Blue component (0-1)</param>
		/// <param name="alpha">Alpha component (0-1)</param>
		public Color (float red, float green, float blue, float alpha = 1f)
			: this ()
		{
			this.R = red;
			this.G = green;
			this.B = blue;
			this.A = alpha;
		}

		/// <summary>
		/// Initializes a new instance of the Color object with the specified red, green, blue, and alpha components
		/// </summary>
		/// <param name="red">Red component (0-255)</param>
		/// <param name="green">Green component (0-255)</param>
		/// <param name="blue">Blue component (0-255)</param>
		/// <param name="alpha">Alpha component (0-255)</param>
		public Color (int red, int green, int blue, int alpha = 0xff)
			: this (red / 255f, green / 255f, blue / 255f, alpha / 255f)
		{
		}

		Color (Color val)
			: this ()
		{
			R = val.R;
			G = val.G;
			B = val.B;
			A = val.A;
		}

		/// <summary>
		/// Converts the specified string to a color
		/// </summary>
		/// <remarks>
		/// The string can be any of these formats:
		///		- #AARRGGBB or #RRGGBB  (where ARGB are hex values)
		///		- 0xAARRGGBB or 0xRRGGBB
		///		- [named] (where [named] is a name of one of the properties in <see cref="Colors"/>)
		///		- [uint]  (where [uint] is a base-10 ARGB value)
		///		- [red], [green], [blue] (where each component is a value from 0-255)
		///		- [alpha], [red], [green], [blue]  (where each component is a value from 0-255)
		///		
		/// If the string is null or empty, this will return <see cref="Colors.Transparent"/>
		/// </remarks>
		/// <param name="value">String value to parse</param>
		/// <param name="color">Color struct with the parsed value, or Transparent if value is invalid</param>
		/// <param name="culture">Culture to use to parse the values</param>
		/// <returns>True if the value was successfully parsed into a color, false otherwise</returns>
		public static bool TryParse (string value, out Color color, CultureInfo culture = null)
		{
			culture = culture ?? CultureInfo.CurrentCulture;
			value = value.Trim ();
			if (value.Length == 0) {
				color = Colors.Transparent;
				return true;
			}

			string listSeparator = culture.TextInfo.ListSeparator;
			if (value.IndexOf (listSeparator) == -1) {
				bool isArgb = value[0] == '#';
				int num = (!isArgb) ? 0 : 1;
				bool ixHex = false;
				if (value.Length > num + 1 && value[num] == '0') {
					ixHex = (value[num + 1] == 'x' || value[num + 1] == 'X');
					if (ixHex) {
						num += 2;
					}
				}
				if (isArgb || ixHex) {
					value = value.Substring (num);
					uint num2;
					if (!uint.TryParse (value, NumberStyles.HexNumber, null, out num2)) {
						color = Colors.Transparent;
						return false;
					}

					if (value.Length < 6 || (value.Length == 6 && isArgb && ixHex)) {
						num2 &= 0xFFFFFF;
					}
					else {
						if (num2 >> 24 == 0) num2 |= 0xFF000000;
					}
					color = Color.FromArgb (num2);
					return true;
				}
				if (colormap == null) {
					lock (colormaplock) {
						if (colormap == null) {
							var props = typeof (Colors).GetProperties (BindingFlags.Public | BindingFlags.Static);
							colormap = new Dictionary<string, Color> (StringComparer.OrdinalIgnoreCase);
							foreach (var val in props.Where (r => r.PropertyType == typeof (Color))) {
								var col = (Color)val.GetValue (null, null);
								colormap.Add (val.Name, col);
							}
						}
					}
				}
				if (colormap.TryGetValue (value, out color))
					return true;
			}
			string[] array = value.Split (listSeparator.ToCharArray ());
			uint[] array2 = new uint[array.Length];
			for (int i = 0; i < array2.Length; i++) {
				uint num;
				if (!uint.TryParse (array[i], out num)) {
					color = Colors.Transparent;
					return false;
				}
				array2[i] = num;
			}
			switch (array.Length) {
			case 1:
				color = Color.FromArgb (array2[0]);
				return true;
			case 3:
				color = new Color (array2[0], array2[1], array2[2]);
				return true;
			case 4:
				color = new Color (array2[0], array2[1], array2[2], array2[3]);
				return true;
			}
			color = Colors.Transparent;
			return false;
		}

		/// <summary>
		/// Tests if the specified object has the same value as this Color
		/// </summary>
		/// <param name="obj">Color to compare with</param>
		/// <returns>True if the specified object is a Color and has the same ARGB components as this color, false otherwise</returns>
        public int R_ { get { return (int)((R * 255) + 0.5); } }
        public int G_ { get { return (int)((G * 255) + 0.5); } }
        public int B_ { get { return (int)((B * 255) + 0.5); } }
        public int A_ { get { return (int)((A * 255) + 0.5); } }

		public override bool Equals (object obj)
		{
			return obj is Color && this == (Color)obj;
		}

		/// <summary>
		/// Gets the hash code for this Color
		/// </summary>
		/// <returns>Hash code for the color</returns>
		public override int GetHashCode ()
		{
			return R.GetHashCode () ^ G.GetHashCode () ^ B.GetHashCode () ^ A.GetHashCode ();
		}

		/// <summary>
		/// Compares two Color structs for equality
		/// </summary>
		/// <param name="color1">The first Color struct to compare</param>
		/// <param name="color2">The second Color struct to compare</param>
		/// <returns>True if both the Color structs have the same values for all ARGB components</returns>
		public static bool operator == (Color color1, Color color2)
		{
			return color1.B == color2.B && color1.R == color2.R && color1.G == color2.G && color1.A == color2.A;
		}

		/// <summary>
		/// Compares two Color structs for inequality
		/// </summary>
		/// <param name="color1">The first Color struct to compare</param>
		/// <param name="color2">The second Color struct to compare</param>
		/// <returns>True if the Color structs have a differing value for any of the ARGB components</returns>
		public static bool operator != (Color color1, Color color2)
		{
			return !(color1 == color2);
		}

		/// <summary>
		/// Inverts the RGB color values
		/// </summary>
		/// <remarks>
		/// This inverts the color components (other than the alpha component) by making them
		/// equal to the 1 minus the component's value.  This is useful for when you want to show
		/// a highlighted color but still show the variation in colors.
		/// </remarks>
		public void Invert ()
		{
			R = 1f - R;
			G = 1f - G;
			B = 1f - B;
		}

		/// <summary>
		/// Converts this color to a 32-bit ARGB value.
		/// </summary>
		/// <returns>The 32-bit ARGB value that corresponds to this color</returns>
		public uint ToArgb ()
		{
			return (
                (uint)(B * byte.MaxValue) | 
                (uint)(G * byte.MaxValue) << 8 | 
                (uint)(R * byte.MaxValue) << 16 | 
                (uint)(A * byte.MaxValue) << 24);
		}

		/// <summary>
		/// Converts this color to a hex representation
		/// </summary>
		/// <remarks>
		/// This will either return a hex value with 8 digits (two per component), or 6 digits (two per RGB) if the <paramref name="includeAlpha"/> is set to false.
		/// </remarks>
		/// <param name="includeAlpha">True to include the alpha component, false to exclude it</param>
		/// <returns>A hex representation of this color, with 8 digits if <paramref name="includeAlpha"/> is true, or 6 digits if false</returns>
		public string ToHex (bool includeAlpha = true)
		{
			if (includeAlpha)
				return string.Format ("#{0:X2}{1:X2}{2:X2}{3:X2}", (byte)(A * byte.MaxValue), (byte)(R * byte.MaxValue), (byte)(G * byte.MaxValue), (byte)(B * byte.MaxValue));
			else
				return string.Format ("#{0:X2}{1:X2}{2:X2}", (byte)(R * byte.MaxValue), (byte)(G * byte.MaxValue), (byte)(B * byte.MaxValue));
                (byte)(G * byte.MaxValue), 
                (byte)(B * byte.MaxValue));
		}

		/// <summary>
		/// Converts this object to a string
		/// </summary>
		/// <remarks>
		/// This just calls <see cref="ToHex"/>
		/// </remarks>
		/// <returns>A string representation of this object</returns>
		public override string ToString ()
		{
			return ToHex ();
        }

		/// <summary>
		/// Compares the specified color for equality
		/// </summary>
		/// <param name="other">Other color to determine equality</param>
		/// <returns>True if all components of the specified color are equal to this object</returns>
		public bool Equals (Color other)
		{
			return other == this;
		}
        #region Element Id Colors

        public static Color GetElementIdColor(int id)
        {
            var result = default(Color);

            // This algorithm is from the SVG# code base:
            // The counter is used to generate IDs in the range [0,2^24-1]
            // The 24 bits of the counter are interpreted as follows:
            // [red 7 bits | green 7 bits | blue 7 bits |shuffle term 3 bits]
            // The shuffle term is used to define how the remaining high
            // bit is set on each color. The colors are generated in the
            // range [0,127] (7 bits) instead of [0,255]. Then the shuffle term
            // is used to adjust them into the range [0,255].
            // This algorithm has the feature that consecutive ids generate
            // visually distinct colors.
            int shuffleTerm = id & 7;

            int r = 0x7f & (id >> 17);
            int g = 0x7f & (id >> 10);
            int b = 0x7f & (id >> 3);

            if ((shuffleTerm & 1) == 1)
                b |= 0x80;

            if ((shuffleTerm & 2) == 2)
                g |= 0x80;

            if ((shuffleTerm & 4) == 4)
                r |= 0x80;

            /*
            switch (shuffleTerm)
            {
                case 0: break;
                case 1: b |= 0x80; break;
                case 2: g |= 0x80; break;
                case 3: g |= 0x80; b |= 0x80; break;
                case 4: r |= 0x80; break;
                case 5: r |= 0x80; b |= 0x80; break;
                case 6: r |= 0x80; g |= 0x80; break;
                case 7: r |= 0x80; g |= 0x80; b |= 0x80; break;
            }*/

            result = new Color(r, g, b);

            return result;
        }

        public int ToElementId()
        {
            int result =
                (this.R_ & 0x7f) << 17 |
                (this.G_ & 0x7f) << 10 |
                (this.B_ & 0x7f) << 3 |
                ((this.R_ & 0x80) == 0x80 ? 4 : 0) |
                ((this.G_ & 0x80) == 0x80 ? 2 : 0) |
                ((this.B_ & 0x80) == 0x80 ? 1 : 0);

            return result;
        }

        #endregion

        #region Parse

        private static Regex ColorRegexArgb =
            new Regex(@"\#(\w\w)(\w\w)(\w\w)(\w\w)");

        private static Regex ColorRegexRgb =
            new Regex(@"\#(\w\w)(\w\w)(\w\w)");

        public static Color? Parse(string value)
        {
            Color? color = null;

            var matchArgb =
                ColorRegexArgb.Match(
                    value);

            if (matchArgb.Success)
            {
                color =
                    new Color(
                        Convert.ToInt32(matchArgb.Groups[2].Value, 16), // R
                        Convert.ToInt32(matchArgb.Groups[3].Value, 16), // G
                        Convert.ToInt32(matchArgb.Groups[4].Value, 16), // B
                        Convert.ToInt32(matchArgb.Groups[1].Value, 16)); // A
            }
            else
            {

                var match =
                    ColorRegexRgb.Match(
                        value);

                if (match.Success)
                {
                    color =
                        new Color(
                            Convert.ToInt32(match.Groups[1].Value, 16),
                            Convert.ToInt32(match.Groups[2].Value, 16),
                            Convert.ToInt32(match.Groups[3].Value, 16));
                }
            }

            return color;
        }

        #endregion
    }
}
