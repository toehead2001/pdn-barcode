/////////////////////////////////////////////////////////////////////////////////
// Paint.NET Effect Plugin Name: Barcode                                       //
// Author: Michael J. Sepcot                                                   //
// Version: 1.1.1                                                              //
// Release Date: 19 March 2007                                                 //
/////////////////////////////////////////////////////////////////////////////////

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
                return this.width;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }
		
		public ColorBgra this[int x, int y]
		{
			get
			{
                if (this.rect.Contains(x, y))
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
			this.width = rect.Width;
			this.height = rect.Height;
			this.surface = new ColorBgra[rect.Width, rect.Height];
		}
		
		private int ConvertX (int x)
		{
			return (x - this.rect.Left);
		}
		
		private int ConvertY (int y)
		{
			return (y - this.rect.Top);
		}
	}
}