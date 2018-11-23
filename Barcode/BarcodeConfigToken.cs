// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

namespace Barcode
{
    public class BarcodeConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        internal string TextToEncode { get; set; }
        internal int EncodingType { get; set; }
        internal bool ColorsBW { get; set; }

        internal BarcodeConfigToken()
        {
            TextToEncode = "";
            EncodingType = Barcode.CODE_39;
            ColorsBW = false;
        }

        protected BarcodeConfigToken(BarcodeConfigToken copyMe)
            : base(copyMe)
        {
            TextToEncode = copyMe.TextToEncode;
            EncodingType = copyMe.EncodingType;
            ColorsBW = copyMe.ColorsBW;
        }

        public override object Clone()
        {
            return new BarcodeConfigToken(this);
        }
    }
}
