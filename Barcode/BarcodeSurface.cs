// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

using System;
using System.Drawing;
using PaintDotNet;

namespace Barcode
{
    internal class BarcodeSurface
    {
        private readonly Rectangle rect;
        private readonly ColorBgra[,] surface;

        internal int Width => rect.Width;
        internal int Height => rect.Height;

        internal ColorBgra this[int x, int y]
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
                if (this.rect.Contains(x, y))
                {
                    surface[this.ConvertX(x), this.ConvertY(y)] = value;
                }
            }
        }

        internal BarcodeSurface(Rectangle rect)
        {
            this.rect = rect;
            surface = new ColorBgra[rect.Width, rect.Height];
        }

        private int ConvertX(int x)
        {
            return (x - rect.Left);
        }

        private int ConvertY(int y)
        {
            return (y - rect.Top);
        }
    }
}
