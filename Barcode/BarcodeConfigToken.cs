// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201


namespace Barcode
{
    public class BarcodeConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        private string textToEncode;
        public string TextToEncode
        {
            get { return textToEncode; }
            set { textToEncode = value; }
        }

        private int encodingType;
        public int EncodingType
        {
            get { return encodingType; }
            set { encodingType = value; }
        }

        private bool colorsBW;
        public bool ColorsBW
        {
            get { return colorsBW; }
            set { colorsBW = value; }
        }

        public BarcodeConfigToken()
            : base()
        {
            textToEncode = "";
            encodingType = Barcode.CODE_39;
            colorsBW = false;
        }

        public BarcodeConfigToken(string text, int encoding, bool colors)
            : base()
        {
            textToEncode = text;
            encodingType = encoding;
            colorsBW = colors;
        }

        protected BarcodeConfigToken(BarcodeConfigToken copyMe)
            : base(copyMe)
        {
            textToEncode = copyMe.textToEncode;
            encodingType = copyMe.encodingType;
            colorsBW = copyMe.colorsBW;
        }

        public override object Clone()
        {
            return new BarcodeConfigToken(this);
        }
    }
}