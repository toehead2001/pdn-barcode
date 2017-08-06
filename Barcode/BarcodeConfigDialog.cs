// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

using PaintDotNet.Effects;
using System;
using System.Drawing;

namespace Barcode
{
    internal partial class BarcodeConfigDialog : EffectConfigDialog
    {
        public BarcodeConfigDialog()
        {
            InitializeComponent();
            ActiveControl = textBoxText;
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new BarcodeConfigToken("", Barcode.CODE_39, false);
        }

        protected override void InitTokenFromDialog()
        {
            ((BarcodeConfigToken)EffectToken).TextToEncode = textBoxText.Text;
            ((BarcodeConfigToken)EffectToken).EncodingType = comboEncoding.SelectedIndex;
            ((BarcodeConfigToken)EffectToken).ColorsBW = checkBoxBW.Checked;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectTokenCopy)
        {
            BarcodeConfigToken token = (BarcodeConfigToken)effectTokenCopy;
            textBoxText.Text = token.TextToEncode;
            comboEncoding.SelectedIndex = token.EncodingType;
            checkBoxBW.Checked = token.ColorsBW;
        }

        private void checkBoxBW_CheckedStateChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            textBoxText.BackColor = (Barcode.ValidateText(textBoxText.Text, comboEncoding.SelectedIndex)) ? SystemColors.Window : Color.LightPink;
            FinishTokenUpdate();
        }

        private void comboEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxText.BackColor = (Barcode.ValidateText(textBoxText.Text, comboEncoding.SelectedIndex)) ? SystemColors.Window : Color.LightPink;
            FinishTokenUpdate();
        }
    }
}