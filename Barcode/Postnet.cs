// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using PaintDotNet;

namespace Barcode
{
	public class Postnet
	{
		private Dictionary<char, string> postnet = new Dictionary<char, string>(10);
		
		public Postnet()
		{
			BuildPostnet();
		}
		
		public BarcodeSurface Create(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
		{
			BarcodeSurface barcode = new BarcodeSurface(rect);
			string encodedText = Encode(text);
			
			int barWidth = (int)Math.Floor((double)barcode.Width / encodedText.Length);
			int halfBarHeight = (int)Math.Floor((double)barcode.Height / 2.0);
			
			int currentHeight = 0;
			for (int y = rect.Top; y < rect.Bottom; y++)
			{
				int loc = 0;
				int step = 0;
				for (int x = rect.Left; x < rect.Right; x++)
				{
					if (loc < encodedText.Length && barWidth > 0 && halfBarHeight > 0)
					{
                        if (encodedText[loc] == 'w' || (encodedText[loc] == 'b' && currentHeight <= halfBarHeight))
						{
							barcode[x, y] = secondaryColor;
						}
						else if (encodedText[loc] == 'B' || (encodedText[loc] == 'b' && currentHeight > halfBarHeight))
						{
							barcode[x, y] = primaryColor;
						}
						else
						{
							barcode[x,y] = source[x,y];
						}
						step++;
						if (step % barWidth == 0) loc++;
					}
					else
					{
						barcode[x, y] = source[x, y];
					}
				}
				currentHeight++;
			}

			return barcode;
		}
		
        public static bool Validate(string text)
        {
			return Regex.Match(text, "(^(\\d){5}$)|(^(\\d){6}$)|(^(\\d){9}$)|(^(\\d){11}$)").Success;
        }
		
		public string Encode(string text)
		{
			string encoded = "";
			if (Validate(text) )
			{
				text =  text + CheckDigit(text);
				encoded += "Bw";
				for (int lcv = 0; lcv < text.Length; lcv++)
				{
					encoded += postnet[text[lcv]];
				}
				encoded += "B";
			}
			return encoded;
		}
		
		public static string CheckDigit(string text)
		{
			int total = 0;
			for ( int lcv = 0; lcv < text.Length; lcv++ )
			{
				total += Convert.ToInt32(text.Substring(lcv,1));
			}
			int checkDigit = (10 - (total % 10));
			return Convert.ToString(checkDigit);
		}
		
		private void BuildPostnet()
		{
			postnet.Add('0', "BwBwbwbwbw");
			postnet.Add('1', "bwbwbwBwBw");
			postnet.Add('2', "bwbwBwbwBw");
			postnet.Add('3', "bwbwBwBwbw");
			postnet.Add('4', "bwBwbwbwBw");
			postnet.Add('5', "bwBwbwBwbw");
			postnet.Add('6', "bwBwBwbwbw");
			postnet.Add('7', "BwbwbwbwBw");
			postnet.Add('8', "BwbwbwBwbw");
			postnet.Add('9', "BwbwBwbwbw");
		}

	}
}