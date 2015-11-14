// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using PaintDotNet;

namespace Barcode
{
    public class Code39
    {
        private Dictionary<char, string> code39 = new Dictionary<char, string>(128);
        
        public Code39()
        {
            BuildCode39FullAscii();
        }
        
        public BarcodeSurface CreateCode39(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            text = text.ToUpperInvariant();
            return Create(rect, source, text, primaryColor, secondaryColor);
        }
        
        public BarcodeSurface CreateCode39mod43(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            text = text.ToUpperInvariant();
            text = text + Mod43(text);
            return Create(rect, source, text, primaryColor, secondaryColor);
        }
        
        public BarcodeSurface CreateFullAsciiCode39(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            return Create(rect, source, text, primaryColor, secondaryColor);
        }
        
        private BarcodeSurface Create(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            BarcodeSurface barcode = new BarcodeSurface(rect);
            string encodedText = Encode(text);
            
            int barWidth = (int)Math.Floor((double)barcode.Width / encodedText.Length);
            
            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                int loc = 0;
                int step = 0;
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (loc < encodedText.Length && barWidth > 0)
                    {
                        if (encodedText[loc] == 'w')
                        {
                            barcode[x, y] = secondaryColor;
                        }
                        else if (encodedText[loc] == 'b')
                        {
                            barcode[x, y] = primaryColor;
                        }
                        else
                        {
                            barcode[x, y] = source[x, y];
                        }
                        step++;
                        if (step % barWidth == 0) loc++;
                    }
                    else
                    {
                        barcode[x, y] = source[x, y];
                    }
                }
            }
            
            return barcode;
        }
        
        private const string charSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";
        public static char Mod43(string text)
        {
            int total = 0;
            for (int lcv = 0; lcv < text.Length; lcv++)
            {
                total = total + charSet.IndexOf(text.Substring(lcv, 1)) - 1;
            }
            return charSet[total % 43 + 1];
        }
        
        public string Encode(string text)
        {
            string encoded = "";
            if (text.Length > 0)
            {
                encoded = "bwwwbwbbbwbbbwb" + "w";
                for (int lcv = 0; lcv < text.Length; lcv++)
                {
                    encoded += code39[text[lcv]] + "w";
                }
                encoded += "bwwwbwbbbwbbbwb";
            }			
            return encoded;
        }
        
        public static bool ValidateCode39(string text)
        {
            return Regex.Match(text.ToUpperInvariant(), "^[A-Z0-9-\\.\\$/\\+%\\s]+$").Success;
        }
        
        public static bool ValidateCode39mod43(string text)
        {
            return ValidateCode39(text);
        }
        
        public static bool ValidateFullAsciiCode39(string text)
        {
            bool passedInspection = true;
            
            if (text.Length == 0)
            {
                passedInspection = false;
            }
            else
            {
                for (int lcv = 0; lcv < text.Length; lcv++)
                {
                    if ((int)text[lcv] < 0 || (int)text[lcv] > 127)
                    {
                        passedInspection = false;
                        break;
                    }
                }
            }

            return passedInspection;
        }
        
        private void BuildCode39FullAscii()
        {
            code39.Add((char)0, "bwbwwwbwwwbwwwbwbbbwwwbwbwbwbbb"); // NUL
            code39.Add((char)1, "bwwwbwwwbwwwbwbwbbbwbwbwwwbwbbb"); // SOH
            code39.Add((char)2, "bwwwbwwwbwwwbwbwbwbbbwbwwwbwbbb"); // STX
            code39.Add((char)3, "bwwwbwwwbwwwbwbwbbbwbbbwbwwwbwb"); // ETX
            code39.Add((char)4, "bwwwbwwwbwwwbwbwbwbwbbbwwwbwbbb"); // EOT
            code39.Add((char)5, "bwwwbwwwbwwwbwbwbbbwbwbbbwwwbwb"); // ENQ
            code39.Add((char)6, "bwwwbwwwbwwwbwbwbwbbbwbbbwwwbwb"); // ACK
            code39.Add((char)7, "bwwwbwwwbwwwbwbwbwbwbwwwbbbwbbb"); // BEL
            code39.Add((char)8, "bwwwbwwwbwwwbwbwbbbwbwbwwwbbbwb"); // BS
            code39.Add((char)9, "bwwwbwwwbwwwbwbwbwbbbwbwwwbbbwb"); // HT
            code39.Add((char)10, "bwwwbwwwbwwwbwbwbwbwbbbwwwbbbwb"); // LF
            code39.Add((char)11, "bwwwbwwwbwwwbwbwbbbwbwbwbwwwbbb"); // VT
            code39.Add((char)12, "bwwwbwwwbwwwbwbwbwbbbwbwbwwwbbb"); // FF
            code39.Add((char)13, "bwwwbwwwbwwwbwbwbbbwbbbwbwbwwwb"); // CR
            code39.Add((char)14, "bwwwbwwwbwwwbwbwbwbwbbbwbwwwbbb"); // SO
            code39.Add((char)15, "bwwwbwwwbwwwbwbwbbbwbwbbbwbwwwb"); // SI
            code39.Add((char)16, "bwwwbwwwbwwwbwbwbwbbbwbbbwbwwwb"); // DLE
            code39.Add((char)17, "bwwwbwwwbwwwbwbwbwbwbwbbbwwwbbb"); // DC1
            code39.Add((char)18, "bwwwbwwwbwwwbwbwbbbwbwbwbbbwwwb"); // DC2
            code39.Add((char)19, "bwwwbwwwbwwwbwbwbwbbbwbwbbbwwwb"); // DC3
            code39.Add((char)20, "bwwwbwwwbwwwbwbwbwbwbbbwbbbwwwb"); // DC4
            code39.Add((char)21, "bwwwbwwwbwwwbwbwbbbwwwbwbwbwbbb"); // NAK
            code39.Add((char)22, "bwwwbwwwbwwwbwbwbwwwbbbwbwbwbbb"); // SYN
            code39.Add((char)23, "bwwwbwwwbwwwbwbwbbbwwwbbbwbwbwb"); // ETB
            code39.Add((char)24, "bwwwbwwwbwwwbwbwbwwwbwbbbwbwbbb"); // CAN
            code39.Add((char)25, "bwwwbwwwbwwwbwbwbbbwwwbwbbbwbwb"); // EM
            code39.Add((char)26, "bwwwbwwwbwwwbwbwbwwwbbbwbbbwbwb"); // SUB
            code39.Add((char)27, "bwbwwwbwwwbwwwbwbbbwbwbwwwbwbbb"); // ESC
            code39.Add((char)28, "bwbwwwbwwwbwwwbwbwbbbwbwwwbwbbb"); // FS
            code39.Add((char)29, "bwbwwwbwwwbwwwbwbbbwbbbwbwwwbwb"); // GS
            code39.Add((char)30, "bwbwwwbwwwbwwwbwbwbwbbbwwwbwbbb"); // RS
            code39.Add((char)31, "bwbwwwbwwwbwwwbwbbbwbwbbbwwwbwb"); // US
            code39.Add((char)32, "bwwwbbbwbwbbbwb"); // [space]
            code39.Add((char)33, "bwwwbwwwbwbwwwbwbbbwbwbwwwbwbbb"); // !
            code39.Add((char)34, "bwwwbwwwbwbwwwbwbwbbbwbwwwbwbbb"); // "
            code39.Add((char)35, "bwwwbwwwbwbwwwbwbbbwbbbwbwwwbwb"); // #
            code39.Add((char)36, "bwwwbwwwbwbwwwbwbwbwbbbwwwbwbbb"); // $
            code39.Add((char)37, "bwwwbwwwbwbwwwbwbbbwbwbbbwwwbwb"); // %
            code39.Add((char)38, "bwwwbwwwbwbwwwbwbwbbbwbbbwwwbwb"); // &
            code39.Add((char)39, "bwwwbwwwbwbwwwbwbwbwbwwwbbbwbbb"); // '
            code39.Add((char)40, "bwwwbwwwbwbwwwbwbbbwbwbwwwbbbwb"); // (
            code39.Add((char)41, "bwwwbwwwbwbwwwbwbwbbbwbwwwbbbwb"); // )
            code39.Add((char)42, "bwwwbwwwbwbwwwbwbwbwbbbwwwbbbwb"); // *
            code39.Add((char)43, "bwwwbwwwbwbwwwbwbbbwbwbwbwwwbbb"); // +
            code39.Add((char)44, "bwwwbwwwbwbwwwbwbwbbbwbwbwwwbbb"); // ,
            code39.Add((char)45, "bwwwbwbwbbbwbbb"); // -
            code39.Add((char)46, "bbbwwwbwbwbbbwb"); // .
            code39.Add((char)47, "bwwwbwwwbwbwwwbwbbbwbwbbbwbwwwb"); // /
            code39.Add((char)48, "bwbwwwbbbwbbbwb"); // 0
            code39.Add((char)49, "bbbwbwwwbwbwbbb"); // 1
            code39.Add((char)50, "bwbbbwwwbwbwbbb"); // 2
            code39.Add((char)51, "bbbwbbbwwwbwbwb"); // 3
            code39.Add((char)52, "bwbwwwbbbwbwbbb"); // 4
            code39.Add((char)53, "bbbwbwwwbbbwbwb"); // 5
            code39.Add((char)54, "bwbbbwwwbbbwbwb"); // 6
            code39.Add((char)55, "bwbwwwbwbbbwbbb"); // 7
            code39.Add((char)56, "bbbwbwwwbwbbbwb"); // 8
            code39.Add((char)57, "bwbbbwwwbwbbbwb"); // 9
            code39.Add((char)58, "bwwwbwwwbwbwwwbwbwwwbbbwbbbwbwb"); // :
            code39.Add((char)59, "bwbwwwbwwwbwwwbwbwbbbwbbbwwwbwb"); // ;
            code39.Add((char)60, "bwbwwwbwwwbwwwbwbwbwbwwwbbbwbbb"); // <
            code39.Add((char)61, "bwbwwwbwwwbwwwbwbbbwbwbwwwbbbwb"); // =
            code39.Add((char)62, "bwbwwwbwwwbwwwbwbwbbbwbwwwbbbwb"); // >
            code39.Add((char)63, "bwbwwwbwwwbwwwbwbwbwbbbwwwbbbwb"); // ?
            code39.Add((char)64, "bwbwwwbwwwbwwwbwbwwwbbbwbwbwbbb"); // @
            code39.Add((char)65, "bbbwbwbwwwbwbbb"); // A
            code39.Add((char)66, "bwbbbwbwwwbwbbb"); // B
            code39.Add((char)67, "bbbwbbbwbwwwbwb"); // C
            code39.Add((char)68, "bwbwbbbwwwbwbbb"); // D
            code39.Add((char)69, "bbbwbwbbbwwwbwb"); // E
            code39.Add((char)70, "bwbbbwbbbwwwbwb"); // F
            code39.Add((char)71, "bwbwbwwwbbbwbbb"); // G
            code39.Add((char)72, "bbbwbwbwwwbbbwb"); // H
            code39.Add((char)73, "bwbbbwbwwwbbbwb"); // I
            code39.Add((char)74, "bwbwbbbwwwbbbwb"); // J
            code39.Add((char)75, "bbbwbwbwbwwwbbb"); // K
            code39.Add((char)76, "bwbbbwbwbwwwbbb"); // L
            code39.Add((char)77, "bbbwbbbwbwbwwwb"); // M
            code39.Add((char)78, "bwbwbbbwbwwwbbb"); // N
            code39.Add((char)79, "bbbwbwbbbwbwwwb"); // O
            code39.Add((char)80, "bwbbbwbbbwbwwwb"); // P
            code39.Add((char)81, "bwbwbwbbbwwwbbb"); // Q
            code39.Add((char)82, "bbbwbwbwbbbwwwb"); // R
            code39.Add((char)83, "bwbbbwbwbbbwwwb"); // S
            code39.Add((char)84, "bwbwbbbwbbbwwwb"); // T
            code39.Add((char)85, "bbbwwwbwbwbwbbb"); // U
            code39.Add((char)86, "bwwwbbbwbwbwbbb"); // V
            code39.Add((char)87, "bbbwwwbbbwbwbwb"); // W
            code39.Add((char)88, "bwwwbwbbbwbwbbb"); // X
            code39.Add((char)89, "bbbwwwbwbbbwbwb"); // Y
            code39.Add((char)90, "bwwwbbbwbbbwbwb"); // Z
            code39.Add((char)91, "bwbwwwbwwwbwwwbwbbbwbwbwbwwwbbb"); // [
            code39.Add((char)92, "bwbwwwbwwwbwwwbwbwbbbwbwbwwwbbb"); // \
            code39.Add((char)93, "bwbwwwbwwwbwwwbwbbbwbbbwbwbwwwb"); // ]
            code39.Add((char)94, "bwbwwwbwwwbwwwbwbwbwbbbwbwwwbbb"); // ^
            code39.Add((char)95, "bwbwwwbwwwbwwwbwbbbwbwbbbwbwwwb"); // _
            code39.Add((char)96, "bwbwwwbwwwbwwwbwbbbwwwbbbwbwbwb"); // `
            code39.Add((char)97, "bwwwbwbwwwbwwwbwbbbwbwbwwwbwbbb"); // a
            code39.Add((char)98, "bwwwbwbwwwbwwwbwbwbbbwbwwwbwbbb"); // b
            code39.Add((char)99, "bwwwbwbwwwbwwwbwbbbwbbbwbwwwbwb"); // c
            code39.Add((char)100, "bwwwbwbwwwbwwwbwbwbwbbbwwwbwbbb"); // d
            code39.Add((char)101, "bwwwbwbwwwbwwwbwbbbwbwbbbwwwbwb"); // e
            code39.Add((char)102, "bwwwbwbwwwbwwwbwbwbbbwbbbwwwbwb"); // f
            code39.Add((char)103, "bwwwbwbwwwbwwwbwbwbwbwwwbbbwbbb"); // g
            code39.Add((char)104, "bwwwbwbwwwbwwwbwbbbwbwbwwwbbbwb"); // h
            code39.Add((char)105, "bwwwbwbwwwbwwwbwbwbbbwbwwwbbbwb"); // i
            code39.Add((char)106, "bwwwbwbwwwbwwwbwbwbwbbbwwwbbbwb"); // j
            code39.Add((char)107, "bwwwbwbwwwbwwwbwbbbwbwbwbwwwbbb"); // k
            code39.Add((char)108, "bwwwbwbwwwbwwwbwbwbbbwbwbwwwbbb"); // l
            code39.Add((char)109, "bwwwbwbwwwbwwwbwbbbwbbbwbwbwwwb"); // m
            code39.Add((char)110, "bwwwbwbwwwbwwwbwbwbwbbbwbwwwbbb"); // n
            code39.Add((char)111, "bwwwbwbwwwbwwwbwbbbwbwbbbwbwwwb"); // o
            code39.Add((char)112, "bwwwbwbwwwbwwwbwbwbbbwbbbwbwwwb"); // p
            code39.Add((char)113, "bwwwbwbwwwbwwwbwbwbwbwbbbwwwbbb"); // q
            code39.Add((char)114, "bwwwbwbwwwbwwwbwbbbwbwbwbbbwwwb"); // r
            code39.Add((char)115, "bwwwbwbwwwbwwwbwbwbbbwbwbbbwwwb"); // s
            code39.Add((char)116, "bwwwbwbwwwbwwwbwbwbwbbbwbbbwwwb"); // t
            code39.Add((char)117, "bwwwbwbwwwbwwwbwbbbwwwbwbwbwbbb"); // u
            code39.Add((char)118, "bwwwbwbwwwbwwwbwbwwwbbbwbwbwbbb"); // v
            code39.Add((char)119, "bwwwbwbwwwbwwwbwbbbwwwbbbwbwbwb"); // w
            code39.Add((char)120, "bwwwbwbwwwbwwwbwbwwwbwbbbwbwbbb"); // x
            code39.Add((char)121, "bwwwbwbwwwbwwwbwbbbwwwbwbbbwbwb"); // y
            code39.Add((char)122, "bwwwbwbwwwbwwwbwbwwwbbbwbbbwbwb"); // z
            code39.Add((char)123, "bwbwwwbwwwbwwwbwbwbbbwbbbwbwwwb"); // {
            code39.Add((char)124, "bwbwwwbwwwbwwwbwbwbwbwbbbwwwbbb"); // |
            code39.Add((char)125, "bwbwwwbwwwbwwwbwbbbwbwbwbbbwwwb"); // }
            code39.Add((char)126, "bwbwwwbwwwbwwwbwbwbbbwbwbbbwwwb"); // ~
            code39.Add((char)127, "bwbwwwbwwwbwwwbwbwbwbbbwbbbwwwb"); // DEL
        }
    }
}