/////////////////////////////////////////////////////////////////////////////////
// Paint.NET Effect Plugin Name: Barcode                                       
// Author: Michael J. Sepcot
//
// Version: 1.2.0 by toe_head2001
// Release Date: 23 February 2015
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Reflection;
using PaintDotNet;
using PaintDotNet.Effects;

namespace Barcode
{
    public class PluginSupportInfo : IPluginSupportInfo
    {
        public string Author
        {
            get
            {
                return ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }
        public string Copyright
        {
            get
            {
                return ((AssemblyDescriptionAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
            }
        }

        public string DisplayName
        {
            get
            {
                return ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            }
        }

        public Version Version
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }

        public Uri WebsiteUri
        {
            get
            {
                return new Uri("http://www.getpaint.net/redirect/plugins.html");
            }
        }
    }

    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Barcode")]

    public class Barcode
        : PaintDotNet.Effects.Effect
    {
        // Note: The value of these constants match the index of the drop down box items in BarcodeConfigDialog
        public const int CODE_39 = 0;
        public const int CODE_39_MOD_43 = 1;
        public const int FULL_ASCII_CODE_39 = 2;
		public const int POSTNET = 3;
		public const int UPCA = 4;

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

        public static string SubmenuName
        {
            get
            {
                return SubmenuNames.Render;
            }
        }

        public Barcode()
            : base(StaticName, StaticIcon, SubmenuName, EffectFlags.Configurable)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new BarcodeConfigDialog();
        }

		private Surface _upca = null;

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
			else if (encoding == Barcode.UPCA)
			{
				UPCa upca = new UPCa();
				Bitmap upcaBitmap;
				upcaBitmap = upca.CreateBarCode(selection, toEncode, primary, secondary);
				_upca = Surface.CopyFromBitmap(upcaBitmap);
			}

            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Rectangle rect = rois[i];
                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    for (int x = rect.Left; x < rect.Right; ++x)
                    {
                        dstArgs.Surface[x, y] = (encoding != Barcode.UPCA)?barcode[x, y]:_upca.GetBilinearSample((x - selection.Left), (y - selection.Top));
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
			else if (encoding == Barcode.UPCA)
			{
				return UPCa.Validate(text);
			}
			else
			{
                return false;
            }
        }
    }
}