// Barcode UPC-A
// Author: Bill Daugherty II
// Addional Modifications: toe_head201

using PaintDotNet;
using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Barcode
{
	class UPCa
	{
		private Bitmap newBitmap;
		private Graphics g;
		private int barCodeHeight;
		private int placeMarker = 0;
		private int imageWidth = 0;
		private float imageScale = 1;
		private string UPCABegin = "0000000000000101";
		private string[] UPCALeft = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
		private string UPCAMiddle = "01010";
		private string[] UPCARight = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
		private string UPCAEnd = "1010000000000000";

		public Bitmap CreateBarCode(Rectangle rect, string txt, ColorBgra primaryColor, ColorBgra secondaryColor)
		{
			if (!Validate(txt))
			{
				newBitmap = new Bitmap(1, 1);
				return newBitmap;
			}

			imageScale = rect.Width / 120;
			if (imageScale < 1 || rect.Height < (rect.Width * 1 / 6))
			{
				newBitmap = new Bitmap(1, 1);
				return newBitmap;
			}
			barCodeHeight = (int)(rect.Height / imageScale);
			imageWidth = 120;
			imageWidth = Convert.ToInt32(imageWidth * imageScale);
			newBitmap = new Bitmap((imageWidth), rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			g = Graphics.FromImage(newBitmap);
			g.ScaleTransform(imageScale, imageScale);
			Rectangle newRec = new Rectangle(0, 0, (imageWidth), rect.Height);
			g.FillRectangle(new SolidBrush(secondaryColor), newRec);
			placeMarker = 0;
			txt = txt.Substring(0, 11) + GetCheckSum(txt).ToString();
			int wholeSet = 0;
			for (wholeSet = 1; wholeSet <= Convert.ToInt32(txt.Length); wholeSet++)
			{
				int currentASCII = Convert.ToChar((txt.Substring(wholeSet - 1, 1))) - 48;
				if (wholeSet == 1)
				{
					DrawSet(UPCABegin, placeMarker, barCodeHeight, 0, primaryColor, secondaryColor);
					DrawSet(UPCALeft[currentASCII], placeMarker, barCodeHeight, 0, primaryColor, secondaryColor);
				}
				else if (wholeSet <= 5)
				{
					DrawSet(UPCALeft[currentASCII], placeMarker, barCodeHeight, 6, primaryColor, secondaryColor);
				}
				else if (wholeSet == 6)
				{
					DrawSet(UPCALeft[currentASCII], placeMarker, barCodeHeight, 6, primaryColor, secondaryColor);
					DrawSet(UPCAMiddle, placeMarker, barCodeHeight, 0, primaryColor, secondaryColor);
				}
				else if (wholeSet <= 11)
				{
					DrawSet(UPCARight[currentASCII], placeMarker, barCodeHeight, 6, primaryColor, secondaryColor);
				}
				else if (wholeSet == 12)
				{
					DrawSet(UPCARight[currentASCII], placeMarker, barCodeHeight, 0, primaryColor, secondaryColor);
					DrawSet(UPCAEnd, placeMarker, barCodeHeight, 0, primaryColor, secondaryColor);
				}
			}

			Font font = new Font("Courier New, Bold", 9);
			try
			{
				SolidBrush textBrush = new SolidBrush(primaryColor);
				float yPoint = barCodeHeight - 13;
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				g.DrawString(txt.Substring(0, 1), font, textBrush, new PointF(0, yPoint));
				g.DrawString(txt.Substring(1, 5), font, textBrush, new PointF(22, yPoint));
				g.DrawString(txt.Substring(6, 5), font, textBrush, new PointF(60, yPoint));
				g.DrawString(txt.Substring(11, 1), font, textBrush, new PointF(108, yPoint));
			}
			finally
			{
				font.Dispose();
			}
			return newBitmap;
		}

		public static bool Validate(string text)
		{
			return Regex.Match(text, "^[0-9]{12}$").Success;
		}

		public int GetCheckSum(string barCode)
		{
			string leftSideOfBarCode = barCode.Substring(0, 11);
			int total = 0;
			int currentDigit = 0;
			int i = 0;
			for (i = 0; i <= leftSideOfBarCode.Length - 1; i++)
			{
				currentDigit = Convert.ToInt32(leftSideOfBarCode.Substring(i, 1));
				if (((i - 1) % 2 == 0))
				{
					total += currentDigit * 1;
				}
				else
				{
					total += currentDigit * 3;
				}
			}
			int iCheckSum = (10 - (total % 10)) % 10;
			return iCheckSum;
		}

		private void DrawSet(string upcCode, int drawLocation, int barCodeHeight, int barHeight, ColorBgra primaryColor, ColorBgra secondaryColor)
		{
			int[] currentLetterArray = new int[upcCode.Length];
			placeMarker += upcCode.Length;
			barHeight = barCodeHeight - barHeight;
			int i = 0;
			for (i = 0; i <= upcCode.Length - 1; i++)
			{
				currentLetterArray[i] = Convert.ToInt16(upcCode.Substring(i, 1));
			}
			for (i = 0; i <= upcCode.Length - 1; i++)
			{
				if (currentLetterArray[i] == 0)
				{
					Pen barBrush = new Pen(secondaryColor);
					g.DrawLine(barBrush, i + (drawLocation), 0, i + (drawLocation), barHeight - 6);
				}
				else if (currentLetterArray[i] == 1)
				{
					Pen barBrush = new Pen(primaryColor);
					g.DrawLine(barBrush, i + (drawLocation), 0, i + (drawLocation), barHeight - 6);
				}
			}
		}

	}
}
