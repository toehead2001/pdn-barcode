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
    internal static class Code39
    {
        private static readonly Dictionary<char, string> code39;
        private const string charSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

        static Code39()
        {
            code39 = new Dictionary<char, string>(128)
            {
                { (char)0, "bwbwwwbwwwbwwwbwbbbwwwbwbwbwbbb" }, // NUL
                { (char)1, "bwwwbwwwbwwwbwbwbbbwbwbwwwbwbbb" }, // SOH
                { (char)2, "bwwwbwwwbwwwbwbwbwbbbwbwwwbwbbb" }, // STX
                { (char)3, "bwwwbwwwbwwwbwbwbbbwbbbwbwwwbwb" }, // ETX
                { (char)4, "bwwwbwwwbwwwbwbwbwbwbbbwwwbwbbb" }, // EOT
                { (char)5, "bwwwbwwwbwwwbwbwbbbwbwbbbwwwbwb" }, // ENQ
                { (char)6, "bwwwbwwwbwwwbwbwbwbbbwbbbwwwbwb" }, // ACK
                { (char)7, "bwwwbwwwbwwwbwbwbwbwbwwwbbbwbbb" }, // BEL
                { (char)8, "bwwwbwwwbwwwbwbwbbbwbwbwwwbbbwb" }, // BS
                { (char)9, "bwwwbwwwbwwwbwbwbwbbbwbwwwbbbwb" }, // HT
                { (char)10, "bwwwbwwwbwwwbwbwbwbwbbbwwwbbbwb" }, // LF
                { (char)11, "bwwwbwwwbwwwbwbwbbbwbwbwbwwwbbb" }, // VT
                { (char)12, "bwwwbwwwbwwwbwbwbwbbbwbwbwwwbbb" }, // FF
                { (char)13, "bwwwbwwwbwwwbwbwbbbwbbbwbwbwwwb" }, // CR
                { (char)14, "bwwwbwwwbwwwbwbwbwbwbbbwbwwwbbb" }, // SO
                { (char)15, "bwwwbwwwbwwwbwbwbbbwbwbbbwbwwwb" }, // SI
                { (char)16, "bwwwbwwwbwwwbwbwbwbbbwbbbwbwwwb" }, // DLE
                { (char)17, "bwwwbwwwbwwwbwbwbwbwbwbbbwwwbbb" }, // DC1
                { (char)18, "bwwwbwwwbwwwbwbwbbbwbwbwbbbwwwb" }, // DC2
                { (char)19, "bwwwbwwwbwwwbwbwbwbbbwbwbbbwwwb" }, // DC3
                { (char)20, "bwwwbwwwbwwwbwbwbwbwbbbwbbbwwwb" }, // DC4
                { (char)21, "bwwwbwwwbwwwbwbwbbbwwwbwbwbwbbb" }, // NAK
                { (char)22, "bwwwbwwwbwwwbwbwbwwwbbbwbwbwbbb" }, // SYN
                { (char)23, "bwwwbwwwbwwwbwbwbbbwwwbbbwbwbwb" }, // ETB
                { (char)24, "bwwwbwwwbwwwbwbwbwwwbwbbbwbwbbb" }, // CAN
                { (char)25, "bwwwbwwwbwwwbwbwbbbwwwbwbbbwbwb" }, // EM
                { (char)26, "bwwwbwwwbwwwbwbwbwwwbbbwbbbwbwb" }, // SUB
                { (char)27, "bwbwwwbwwwbwwwbwbbbwbwbwwwbwbbb" }, // ESC
                { (char)28, "bwbwwwbwwwbwwwbwbwbbbwbwwwbwbbb" }, // FS
                { (char)29, "bwbwwwbwwwbwwwbwbbbwbbbwbwwwbwb" }, // GS
                { (char)30, "bwbwwwbwwwbwwwbwbwbwbbbwwwbwbbb" }, // RS
                { (char)31, "bwbwwwbwwwbwwwbwbbbwbwbbbwwwbwb" }, // US
                { (char)32, "bwwwbbbwbwbbbwb" }, // [space]
                { (char)33, "bwwwbwwwbwbwwwbwbbbwbwbwwwbwbbb" }, // !
                { (char)34, "bwwwbwwwbwbwwwbwbwbbbwbwwwbwbbb" }, // "
                { (char)35, "bwwwbwwwbwbwwwbwbbbwbbbwbwwwbwb" }, // #
                { (char)36, "bwwwbwwwbwbwwwbwbwbwbbbwwwbwbbb" }, // $
                { (char)37, "bwwwbwwwbwbwwwbwbbbwbwbbbwwwbwb" }, // %
                { (char)38, "bwwwbwwwbwbwwwbwbwbbbwbbbwwwbwb" }, // &
                { (char)39, "bwwwbwwwbwbwwwbwbwbwbwwwbbbwbbb" }, // '
                { (char)40, "bwwwbwwwbwbwwwbwbbbwbwbwwwbbbwb" }, // (
                { (char)41, "bwwwbwwwbwbwwwbwbwbbbwbwwwbbbwb" }, // )
                { (char)42, "bwwwbwwwbwbwwwbwbwbwbbbwwwbbbwb" }, // *
                { (char)43, "bwwwbwwwbwbwwwbwbbbwbwbwbwwwbbb" }, // +
                { (char)44, "bwwwbwwwbwbwwwbwbwbbbwbwbwwwbbb" }, // ,
                { (char)45, "bwwwbwbwbbbwbbb" }, // -
                { (char)46, "bbbwwwbwbwbbbwb" }, // .
                { (char)47, "bwwwbwwwbwbwwwbwbbbwbwbbbwbwwwb" }, // /
                { (char)48, "bwbwwwbbbwbbbwb" }, // 0
                { (char)49, "bbbwbwwwbwbwbbb" }, // 1
                { (char)50, "bwbbbwwwbwbwbbb" }, // 2
                { (char)51, "bbbwbbbwwwbwbwb" }, // 3
                { (char)52, "bwbwwwbbbwbwbbb" }, // 4
                { (char)53, "bbbwbwwwbbbwbwb" }, // 5
                { (char)54, "bwbbbwwwbbbwbwb" }, // 6
                { (char)55, "bwbwwwbwbbbwbbb" }, // 7
                { (char)56, "bbbwbwwwbwbbbwb" }, // 8
                { (char)57, "bwbbbwwwbwbbbwb" }, // 9
                { (char)58, "bwwwbwwwbwbwwwbwbwwwbbbwbbbwbwb" }, // :
                { (char)59, "bwbwwwbwwwbwwwbwbwbbbwbbbwwwbwb" }, // ;
                { (char)60, "bwbwwwbwwwbwwwbwbwbwbwwwbbbwbbb" }, // <
                { (char)61, "bwbwwwbwwwbwwwbwbbbwbwbwwwbbbwb" }, // =
                { (char)62, "bwbwwwbwwwbwwwbwbwbbbwbwwwbbbwb" }, // >
                { (char)63, "bwbwwwbwwwbwwwbwbwbwbbbwwwbbbwb" }, // ?
                { (char)64, "bwbwwwbwwwbwwwbwbwwwbbbwbwbwbbb" }, // @
                { (char)65, "bbbwbwbwwwbwbbb" }, // A
                { (char)66, "bwbbbwbwwwbwbbb" }, // B
                { (char)67, "bbbwbbbwbwwwbwb" }, // C
                { (char)68, "bwbwbbbwwwbwbbb" }, // D
                { (char)69, "bbbwbwbbbwwwbwb" }, // E
                { (char)70, "bwbbbwbbbwwwbwb" }, // F
                { (char)71, "bwbwbwwwbbbwbbb" }, // G
                { (char)72, "bbbwbwbwwwbbbwb" }, // H
                { (char)73, "bwbbbwbwwwbbbwb" }, // I
                { (char)74, "bwbwbbbwwwbbbwb" }, // J
                { (char)75, "bbbwbwbwbwwwbbb" }, // K
                { (char)76, "bwbbbwbwbwwwbbb" }, // L
                { (char)77, "bbbwbbbwbwbwwwb" }, // M
                { (char)78, "bwbwbbbwbwwwbbb" }, // N
                { (char)79, "bbbwbwbbbwbwwwb" }, // O
                { (char)80, "bwbbbwbbbwbwwwb" }, // P
                { (char)81, "bwbwbwbbbwwwbbb" }, // Q
                { (char)82, "bbbwbwbwbbbwwwb" }, // R
                { (char)83, "bwbbbwbwbbbwwwb" }, // S
                { (char)84, "bwbwbbbwbbbwwwb" }, // T
                { (char)85, "bbbwwwbwbwbwbbb" }, // U
                { (char)86, "bwwwbbbwbwbwbbb" }, // V
                { (char)87, "bbbwwwbbbwbwbwb" }, // W
                { (char)88, "bwwwbwbbbwbwbbb" }, // X
                { (char)89, "bbbwwwbwbbbwbwb" }, // Y
                { (char)90, "bwwwbbbwbbbwbwb" }, // Z
                { (char)91, "bwbwwwbwwwbwwwbwbbbwbwbwbwwwbbb" }, // [
                { (char)92, "bwbwwwbwwwbwwwbwbwbbbwbwbwwwbbb" }, // \
                { (char)93, "bwbwwwbwwwbwwwbwbbbwbbbwbwbwwwb" }, // ]
                { (char)94, "bwbwwwbwwwbwwwbwbwbwbbbwbwwwbbb" }, // ^
                { (char)95, "bwbwwwbwwwbwwwbwbbbwbwbbbwbwwwb" }, // _
                { (char)96, "bwbwwwbwwwbwwwbwbbbwwwbbbwbwbwb" }, // `
                { (char)97, "bwwwbwbwwwbwwwbwbbbwbwbwwwbwbbb" }, // a
                { (char)98, "bwwwbwbwwwbwwwbwbwbbbwbwwwbwbbb" }, // b
                { (char)99, "bwwwbwbwwwbwwwbwbbbwbbbwbwwwbwb" }, // c
                { (char)100, "bwwwbwbwwwbwwwbwbwbwbbbwwwbwbbb" }, // d
                { (char)101, "bwwwbwbwwwbwwwbwbbbwbwbbbwwwbwb" }, // e
                { (char)102, "bwwwbwbwwwbwwwbwbwbbbwbbbwwwbwb" }, // f
                { (char)103, "bwwwbwbwwwbwwwbwbwbwbwwwbbbwbbb" }, // g
                { (char)104, "bwwwbwbwwwbwwwbwbbbwbwbwwwbbbwb" }, // h
                { (char)105, "bwwwbwbwwwbwwwbwbwbbbwbwwwbbbwb" }, // i
                { (char)106, "bwwwbwbwwwbwwwbwbwbwbbbwwwbbbwb" }, // j
                { (char)107, "bwwwbwbwwwbwwwbwbbbwbwbwbwwwbbb" }, // k
                { (char)108, "bwwwbwbwwwbwwwbwbwbbbwbwbwwwbbb" }, // l
                { (char)109, "bwwwbwbwwwbwwwbwbbbwbbbwbwbwwwb" }, // m
                { (char)110, "bwwwbwbwwwbwwwbwbwbwbbbwbwwwbbb" }, // n
                { (char)111, "bwwwbwbwwwbwwwbwbbbwbwbbbwbwwwb" }, // o
                { (char)112, "bwwwbwbwwwbwwwbwbwbbbwbbbwbwwwb" }, // p
                { (char)113, "bwwwbwbwwwbwwwbwbwbwbwbbbwwwbbb" }, // q
                { (char)114, "bwwwbwbwwwbwwwbwbbbwbwbwbbbwwwb" }, // r
                { (char)115, "bwwwbwbwwwbwwwbwbwbbbwbwbbbwwwb" }, // s
                { (char)116, "bwwwbwbwwwbwwwbwbwbwbbbwbbbwwwb" }, // t
                { (char)117, "bwwwbwbwwwbwwwbwbbbwwwbwbwbwbbb" }, // u
                { (char)118, "bwwwbwbwwwbwwwbwbwwwbbbwbwbwbbb" }, // v
                { (char)119, "bwwwbwbwwwbwwwbwbbbwwwbbbwbwbwb" }, // w
                { (char)120, "bwwwbwbwwwbwwwbwbwwwbwbbbwbwbbb" }, // x
                { (char)121, "bwwwbwbwwwbwwwbwbbbwwwbwbbbwbwb" }, // y
                { (char)122, "bwwwbwbwwwbwwwbwbwwwbbbwbbbwbwb" }, // z
                { (char)123, "bwbwwwbwwwbwwwbwbwbbbwbbbwbwwwb" }, // {
                { (char)124, "bwbwwwbwwwbwwwbwbwbwbwbbbwwwbbb" }, // |
                { (char)125, "bwbwwwbwwwbwwwbwbbbwbwbwbbbwwwb" }, // }
                { (char)126, "bwbwwwbwwwbwwwbwbwbbbwbwbbbwwwb" }, // ~
                { (char)127, "bwbwwwbwwwbwwwbwbwbwbbbwbbbwwwb" } // DEL
            };
        }

        internal static BarcodeSurface CreateCode39(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            if (!ValidateCode39(text))
            {
                BarcodeSurface barcode = new BarcodeSurface(rect);
                return barcode;
            }
            text = text.ToUpperInvariant();
            return Create(rect, source, text, primaryColor, secondaryColor);
        }

        internal static BarcodeSurface CreateCode39mod43(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            if (!ValidateCode39mod43(text))
            {
                BarcodeSurface barcode = new BarcodeSurface(rect);
                return barcode;
            }
            text = text.ToUpperInvariant();
            text = text + Mod43(text);
            return Create(rect, source, text, primaryColor, secondaryColor);
        }

        internal static BarcodeSurface CreateFullAsciiCode39(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
        {
            if (!ValidateFullAsciiCode39(text))
            {
                BarcodeSurface barcode = new BarcodeSurface(rect);
                return barcode;
            }
            return Create(rect, source, text, primaryColor, secondaryColor);
        }

        private static BarcodeSurface Create(Rectangle rect, Surface source, string text, ColorBgra primaryColor, ColorBgra secondaryColor)
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

        private static char Mod43(string text)
        {
            int total = 0;
            for (int lcv = 0; lcv < text.Length; lcv++)
            {
                total = total + charSet.IndexOf(text.Substring(lcv, 1));
            }
            return charSet[total % 43];
        }

        private static string Encode(string text)
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

        internal static bool ValidateCode39(string text)
        {
            return Regex.Match(text.ToUpperInvariant(), "^[A-Z0-9-\\.\\$/\\+%\\s]+$").Success;
        }

        internal static bool ValidateCode39mod43(string text)
        {
            return ValidateCode39(text);
        }

        internal static bool ValidateFullAsciiCode39(string text)
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
    }
}