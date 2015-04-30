/////////////////////////////////////////////////////////////////////////////////
// Paint.NET Effect Plugin Name: Barcode                                       //
// Author: Michael J. Sepcot                                                   //
// Version: 1.1.1                                                              //
// Release Date: 19 March 2007                                                 //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Drawing;
using PaintDotNet;
using PaintDotNet.Effects;

namespace Barcode
{
    public class Barcode
        : PaintDotNet.Effects.Effect
    {
        // Note: The value of these constants match the index of the drop down box items in BarcodeConfigDialog
        public const int CODE_39 = 0;
        public const int CODE_39_MOD_43 = 1;
        public const int FULL_ASCII_CODE_39 = 2;
		public const int POSTNET = 3;

        public static string StaticName
        {
            get
            {
                return "Barcode";
            }
        }

        public static Bitmap StaticIcon
        {
            get
            {
                return new Bitmap(typeof(Barcode), "BarcodeIcon.png");
            }
        }

        public Barcode()
            : base(Barcode.StaticName, Barcode.StaticIcon, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new BarcodeConfigDialog();
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            // Set the text to encode and the encoding type
            String toEncode = ((BarcodeConfigToken)parameters).TextToEncode;
            int encoding = ((BarcodeConfigToken)parameters).EncodingType;
			
			BarcodeSurface barcode = null;
			Rectangle selection = this.EnvironmentParameters.GetSelection(srcArgs.Surface.Bounds).GetBoundsInt();
			ColorBgra primary = this.EnvironmentParameters.PrimaryColor;
			ColorBgra secondary = this.EnvironmentParameters.SecondaryColor;

			if (encoding == Barcode.CODE_39)
			{
				Code39 code39 = new Code39();
				barcode = code39.CreateCode39(selection, srcArgs.Surface, toEncode, primary, secondary);
			}
			else if (encoding == Barcode.CODE_39_MOD_43)
			{
				Code39 code39 = new Code39();
				barcode = code39.CreateCode39mod43(selection, srcArgs.Surface, toEncode, primary, secondary);
			}
			else if (encoding == Barcode.FULL_ASCII_CODE_39)
			{
				Code39 code39 = new Code39();
				barcode = code39.CreateFullAsciiCode39(selection, srcArgs.Surface, toEncode, primary, secondary);
			}
			else if (encoding == Barcode.POSTNET)
			{
				Postnet postnet = new Postnet();
				barcode = postnet.Create(selection, srcArgs.Surface, toEncode, primary, secondary);
			}

            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Rectangle rect = rois[i];
                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    for (int x = rect.Left; x < rect.Right; ++x)
                    {
                        dstArgs.Surface[x, y] = barcode[x,y];
                    }
                }
            }
        }

        public static bool ValidateText(String text, int encoding)
        {
            if (encoding == Barcode.CODE_39)
            {
                return Code39.ValidateCode39(text);
            }
			else if (encoding == Barcode.CODE_39_MOD_43)
			{
				return Code39.ValidateCode39mod43(text);
			}
            else if (encoding == Barcode.FULL_ASCII_CODE_39)
            {
				return Code39.ValidateFullAsciiCode39(text);
            }
			else if (encoding == Barcode.POSTNET)
			{
				return Postnet.Validate(text);
			}
            else
            {
                return false;
            }
        }
    }
}