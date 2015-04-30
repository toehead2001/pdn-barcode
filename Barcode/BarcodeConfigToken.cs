/////////////////////////////////////////////////////////////////////////////////
// Paint.NET Effect Plugin Name: Barcode                                       //
// Author: Michael J. Sepcot                                                   //
// Version: 1.1.1                                                              //
// Release Date: 19 March 2007                                                 //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace Barcode
{
    public class BarcodeConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        private String textToEncode;
        public String TextToEncode
        {
            get { return textToEncode; }
            set { this.textToEncode = value; }
        }
        
        private int encodingType;
        public int EncodingType
        {
            get { return encodingType; }
            set { this.encodingType = value; }
        }

        public BarcodeConfigToken()
            : base()
        {
            textToEncode = "";
            encodingType = Barcode.CODE_39;
        }

        public BarcodeConfigToken(String text, int encoding)
            : base()
        {
            this.textToEncode = text;
            this.encodingType = encoding;
        }

        protected BarcodeConfigToken(BarcodeConfigToken copyMe)
            : base(copyMe)
        {
            this.textToEncode = copyMe.textToEncode;
            this.encodingType = copyMe.encodingType;
        }

        public override object Clone()
        {
            return new BarcodeConfigToken(this);
        }
    }
}