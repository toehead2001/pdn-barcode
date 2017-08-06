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
        public string Author => ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
        public string Copyright => ((AssemblyDescriptionAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
        public string DisplayName => ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
        public Version Version => base.GetType().Assembly.GetName().Version;
        public Uri WebsiteUri => new Uri("http://www.getpaint.net/redirect/plugins.html");
    }

    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Barcode")]

    public class Barcode : Effect<BarcodeConfigToken>
    {
        private const string StaticName = "Barcode";
        private static readonly Bitmap StaticIcon = new Bitmap(typeof(Barcode), "BarcodeIcon.png");

        public Barcode()
            : base(StaticName, StaticIcon, SubmenuNames.Render, EffectFlags.Configurable)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new BarcodeConfigDialog();
        }

        protected override void OnSetRenderInfo(BarcodeConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            string toEncode = newToken.TextToEncode;
            int encoding = newToken.EncodingType;
            bool colorsBW = newToken.ColorsBW;

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

            switch (encoding)
            {
                case CODE_39:
                    barcode = Code39.CreateCode39(selection, srcArgs.Surface, toEncode, primary, secondary);
                    break;
                case CODE_39_MOD_43:
                    barcode = Code39.CreateCode39mod43(selection, srcArgs.Surface, toEncode, primary, secondary);
                    break;
                case FULL_ASCII_CODE_39:
                    barcode = Code39.CreateFullAsciiCode39(selection, srcArgs.Surface, toEncode, primary, secondary);
                    break;
                case POSTNET:
                    barcode = Postnet.Create(selection, srcArgs.Surface, toEncode, primary, secondary);
                    break;
                case UPCA:
                    barcode = UPCa.Create(selection, srcArgs.Surface, toEncode, primary, secondary);
                    break;
            }
        }

        protected override void OnRender(Rectangle[] renderRects, int startIndex, int length)
        {
            if (length == 0) return;
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Render(DstArgs.Surface, SrcArgs.Surface, renderRects[i]);
            }
        }

        // Note: The value of these constants match the index of the drop down box items in BarcodeConfigDialog
        internal const int CODE_39 = 0;
        internal const int CODE_39_MOD_43 = 1;
        internal const int FULL_ASCII_CODE_39 = 2;
        internal const int POSTNET = 3;
        internal const int UPCA = 4;

        private BarcodeSurface barcode;

        void Render(Surface dst, Surface src, Rectangle rect)
        {
            for (int y = rect.Top; y < rect.Bottom; ++y)
            {
                if (IsCancelRequested) return;
                for (int x = rect.Left; x < rect.Right; ++x)
                {
                    dst[x, y] = barcode[x, y];
                }
            }
        }

        internal static bool ValidateText(string text, int encoding)
        {
            switch (encoding)
            {
                case CODE_39:
                    return Code39.ValidateCode39(text);
                case CODE_39_MOD_43:
                    return Code39.ValidateCode39mod43(text);
                case FULL_ASCII_CODE_39:
                    return Code39.ValidateFullAsciiCode39(text);
                case POSTNET:
                    return Postnet.Validate(text);
                case UPCA:
                    return UPCa.Validate(text);
                default:
                    return false;
            }
        }
    }
}