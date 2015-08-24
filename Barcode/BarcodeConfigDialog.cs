/////////////////////////////////////////////////////////////////////////////////
// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
//
// Version: 1.2.0 by toe_head2001
// Release Date: 23 February 2015
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PaintDotNet.Effects;

namespace Barcode
{
    public class BarcodeConfigDialog : PaintDotNet.Effects.EffectConfigDialog
    {
        private Button buttonOK;
        private GroupBox groupBoxText;
        private TextBox textBoxText;
        private GroupBox groupBoxEncoding;
        private ComboBox comboEncoding;
        private Label labelVersion;
        private Button buttonCancel;

        public BarcodeConfigDialog()
        {
            InitializeComponent();
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new BarcodeConfigToken("", Barcode.CODE_39);
        }

        protected override void InitTokenFromDialog()
        {
            ((BarcodeConfigToken)EffectToken).TextToEncode = textBoxText.Text;
            ((BarcodeConfigToken)EffectToken).EncodingType = comboEncoding.SelectedIndex;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            BarcodeConfigToken token = (BarcodeConfigToken)effectToken;
            textBoxText.Text = token.TextToEncode;
            comboEncoding.SelectedIndex = token.EncodingType;
        }

        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxText = new System.Windows.Forms.GroupBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.groupBoxEncoding = new System.Windows.Forms.GroupBox();
            this.comboEncoding = new System.Windows.Forms.ComboBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.groupBoxText.SuspendLayout();
            this.groupBoxEncoding.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(182, 133);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(101, 133);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxText
            // 
            this.groupBoxText.Controls.Add(this.textBoxText);
            this.groupBoxText.Location = new System.Drawing.Point(13, 13);
            this.groupBoxText.Name = "groupBoxText";
            this.groupBoxText.Size = new System.Drawing.Size(244, 50);
            this.groupBoxText.TabIndex = 4;
            this.groupBoxText.TabStop = false;
            this.groupBoxText.Text = "Text To Encode";
            // 
            // textBoxText
            // 
            this.textBoxText.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxText.Location = new System.Drawing.Point(6, 19);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(232, 20);
            this.textBoxText.TabIndex = 0;
            this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
            // 
            // groupBoxEncoding
            // 
            this.groupBoxEncoding.Controls.Add(this.comboEncoding);
            this.groupBoxEncoding.Location = new System.Drawing.Point(13, 69);
            this.groupBoxEncoding.Name = "groupBoxEncoding";
            this.groupBoxEncoding.Size = new System.Drawing.Size(244, 50);
            this.groupBoxEncoding.TabIndex = 5;
            this.groupBoxEncoding.TabStop = false;
            this.groupBoxEncoding.Text = "Encoding Method";
            // 
            // comboEncoding
            // 
            this.comboEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEncoding.FormattingEnabled = true;
            this.comboEncoding.Items.AddRange(new object[] {
            "Code 39",
            "Code 39 mod 43",
            "Full ASCII Code 39",
            "POSTNET",
			"UPC-A"});
            this.comboEncoding.Location = new System.Drawing.Point(7, 20);
            this.comboEncoding.Name = "comboEncoding";
            this.comboEncoding.Size = new System.Drawing.Size(231, 21);
            this.comboEncoding.TabIndex = 0;
            this.comboEncoding.SelectedIndexChanged += new System.EventHandler(this.comboEncoding_SelectedIndexChanged);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Location = new System.Drawing.Point(13, 143);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(37, 13);
            this.labelVersion.TabIndex = 6;
            this.labelVersion.Text = "v1.2.0";
            // 
            // BarcodeConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(269, 168);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.groupBoxEncoding);
            this.Controls.Add(this.groupBoxText);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "BarcodeConfigDialog";
            this.Text = "Barcode";
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.groupBoxText, 0);
            this.Controls.SetChildIndex(this.groupBoxEncoding, 0);
            this.Controls.SetChildIndex(this.labelVersion, 0);
            this.groupBoxText.ResumeLayout(false);
            this.groupBoxText.PerformLayout();
            this.groupBoxEncoding.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            // Place text cursur in the text field
            this.ActiveControl = textBoxText;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            FinishTokenUpdate();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            if (Barcode.ValidateText(this.textBoxText.Text, this.comboEncoding.SelectedIndex))
            {
                this.textBoxText.BackColor = System.Drawing.SystemColors.Window;
                buttonOK.Enabled = true;
                FinishTokenUpdate();
            }
            else
            {
                this.textBoxText.BackColor = System.Drawing.Color.LightPink;
                buttonOK.Enabled = false;
            }
        }

        private void comboEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Barcode.ValidateText(this.textBoxText.Text, this.comboEncoding.SelectedIndex))
            {
                this.textBoxText.BackColor = System.Drawing.SystemColors.Window;
                buttonOK.Enabled = true;
                FinishTokenUpdate();
            }
            else
            {
                this.textBoxText.BackColor = System.Drawing.Color.LightPink;
                buttonOK.Enabled = false;
            }
        }
    }
}