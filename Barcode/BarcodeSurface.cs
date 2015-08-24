// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

using System;
using System.Drawing;
using PaintDotNet;

namespace Barcode
{
	public class BarcodeSurface
	{
		private int width;
		private int height;
		private Rectangle rect;
		private ColorBgra[,] surface;
		
        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }
		
		public ColorBgra this[int x, int y]
		{
			get
			{
                if (rect.Contains(x, y))
                {
                    return surface[this.ConvertX(x), this.ConvertY(y)];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("(x,y)", new Point(x, y), "Coordinates out of bounds, bounds=" + rect.ToString());
                }
			}
			set
			{
				if (this.rect.Contains(x,y))
				{
					surface[ this.ConvertX(x), this.ConvertY(y) ] = value;
				}
			}
		}
		
		public BarcodeSurface(Rectangle rect)
		{
			this.rect = rect;
			width = rect.Width;
			height = rect.Height;
			surface = new ColorBgra[rect.Width, rect.Height];
		}
		
		private int ConvertX (int x)
		{
			return (x - rect.Left);
		}
		
		private int ConvertY (int y)
		{
			return (y - rect.Top);
		}
	}
}