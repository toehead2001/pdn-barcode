// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

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

    public class Barcode : Effect<BarcodeConfigToken>
    {
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

        protected override void OnSetRenderInfo(BarcodeConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            toEncode = newToken.TextToEncode;
            encoding = newToken.EncodingType;
            colorsBW = newToken.ColorsBW;

            Rectangle selection = EnvironmentParameters.GetSelection(srcArgs.Surface.Bounds).GetBoundsInt();

            ColorBgra primary;
            ColorBgra secondary;

            if (colorsBW)
            {
                primary = Color.Black;
                secondary = Color.White;
            }
            else
            {
                primary = EnvironmentParameters.PrimaryColor;
                secondary = EnvironmentParameters.SecondaryColor;
            }


            if (encoding == CODE_39)
            {
                Code39 code39 = new Code39();
                barcode = code39.CreateCode39(selection, srcArgs.Surface, toEncode, primary, secondary);
            }
            else if (encoding == CODE_39_MOD_43)
            {
                Code39 code39 = new Code39();
                barcode = code39.CreateCode39mod43(selection, srcArgs.Surface, toEncode, primary, secondary);
            }
            else if (encoding == FULL_ASCII_CODE_39)
            {
                Code39 code39 = new Code39();
                barcode = code39.CreateFullAsciiCode39(selection, srcArgs.Surface, toEncode, primary, secondary);
            }
            else if (encoding == POSTNET)
            {
                Postnet postnet = new Postnet();
                barcode = postnet.Create(selection, srcArgs.Surface, toEncode, primary, secondary);
            }
            else if (encoding == UPCA)
            {
                UPCa upca = new UPCa();
                Bitmap upcaBitmap;
                upcaBitmap = upca.CreateBarCode(selection, toEncode, primary, secondary);
                _upca = Surface.CopyFromBitmap(upcaBitmap);
            }

        }

        protected override unsafe void OnRender(Rectangle[] rois, int startIndex, int length)
        {
            if (length == 0) return;
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Render(DstArgs.Surface, SrcArgs.Surface, rois[i]);
            }
        }


        // Note: The value of these constants match the index of the drop down box items in BarcodeConfigDialog
        public const int CODE_39 = 0;
        public const int CODE_39_MOD_43 = 1;
        public const int FULL_ASCII_CODE_39 = 2;
        public const int POSTNET = 3;
        public const int UPCA = 4;

        string toEncode;
        int encoding;
        bool colorsBW;

        private Surface _upca;
        private BarcodeSurface barcode;

        void Render(Surface dst, Surface src, Rectangle rect)
        {
            Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();

            for (int y = rect.Top; y < rect.Bottom; ++y)
            {
                for (int x = rect.Left; x < rect.Right; ++x)
                {
                    dst[x, y] = (encoding != UPCA) ?barcode[x, y]:_upca.GetBilinearSample((x - selection.Left), (y - selection.Top));
                }
            }
        }

        public static bool ValidateText(string text, int encoding)
        {
            if (encoding == CODE_39)
            {
                return Code39.ValidateCode39(text);
            }
            else if (encoding == CODE_39_MOD_43)
            {
                return Code39.ValidateCode39mod43(text);
            }
            else if (encoding == FULL_ASCII_CODE_39)
            {
                return Code39.ValidateFullAsciiCode39(text);
            }
            else if (encoding == POSTNET)
            {
                return Postnet.Validate(text);
            }
            else if (encoding == UPCA)
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