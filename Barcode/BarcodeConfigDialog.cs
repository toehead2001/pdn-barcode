// Paint.NET Effect Plugin Name: Barcode
// Author: Michael J. Sepcot
// Addional Modifications: toe_head201

using System;
using System.Windows.Forms;
using PaintDotNet.Effects;

namespace Barcode
{
    public class BarcodeConfigDialog : EffectConfigDialog
    {
        private Button buttonOK;
        private TextBox textBoxText;
        private ComboBox comboEncoding;
        private Button buttonCancel;
        private CheckBox checkBoxBW;
        private Label labelDivider;
        private Label labelText;
        private Label labelTextLine;
        private Label labelMethod;
        private Label labelMethodLine;

        public BarcodeConfigDialog()
        {
            InitializeComponent();
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

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            BarcodeConfigToken token = (BarcodeConfigToken)effectToken;
            textBoxText.Text = token.TextToEncode;
            comboEncoding.SelectedIndex = token.EncodingType;
            checkBoxBW.Checked = token.ColorsBW;
        }

        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.comboEncoding = new System.Windows.Forms.ComboBox();
            this.checkBoxBW = new System.Windows.Forms.CheckBox();
            this.labelDivider = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.labelTextLine = new System.Windows.Forms.Label();
            this.labelMethod = new System.Windows.Forms.Label();
            this.labelMethodLine = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(254, 141);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(83, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(162, 141);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(83, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxText
            // 
            this.textBoxText.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxText.Location = new System.Drawing.Point(7, 27);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(331, 23);
            this.textBoxText.TabIndex = 0;
            this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
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
            this.comboEncoding.Location = new System.Drawing.Point(7, 76);
            this.comboEncoding.Name = "comboEncoding";
            this.comboEncoding.Size = new System.Drawing.Size(127, 23);
            this.comboEncoding.TabIndex = 1;
            this.comboEncoding.SelectedIndexChanged += new System.EventHandler(this.comboEncoding_SelectedIndexChanged);
            // 
            // checkBoxBW
            // 
            this.checkBoxBW.AutoSize = true;
            this.checkBoxBW.Location = new System.Drawing.Point(7, 109);
            this.checkBoxBW.Name = "checkBoxBW";
            this.checkBoxBW.Size = new System.Drawing.Size(111, 19);
            this.checkBoxBW.TabIndex = 3;
            this.checkBoxBW.Text = "Black and White";
            this.checkBoxBW.UseVisualStyleBackColor = true;
            this.checkBoxBW.CheckedChanged += new System.EventHandler(this.checkBoxBW_CheckedStateChanged);
            // 
            // labelDivider
            // 
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Location = new System.Drawing.Point(7, 133);
            this.labelDivider.Name = "labelDivider";
            this.labelDivider.Size = new System.Drawing.Size(332, 2);
            this.labelDivider.TabIndex = 0;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(6, 8);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(88, 15);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Text To Encode";
            // 
            // labelTextLine
            // 
            this.labelTextLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTextLine.Location = new System.Drawing.Point(7, 14);
            this.labelTextLine.Name = "labelTextLine";
            this.labelTextLine.Size = new System.Drawing.Size(332, 2);
            this.labelTextLine.TabIndex = 0;
            // 
            // labelMethod
            // 
            this.labelMethod.AutoSize = true;
            this.labelMethod.Location = new System.Drawing.Point(6, 57);
            this.labelMethod.Name = "labelMethod";
            this.labelMethod.Size = new System.Drawing.Size(102, 15);
            this.labelMethod.TabIndex = 0;
            this.labelMethod.Text = "Encoding Method";
            // 
            // labelMethodLine
            // 
            this.labelMethodLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMethodLine.Location = new System.Drawing.Point(7, 63);
            this.labelMethodLine.Name = "labelMethodLine";
            this.labelMethodLine.Size = new System.Drawing.Size(332, 2);
            this.labelMethodLine.TabIndex = 0;
            // 
            // BarcodeConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(345, 174);
            this.Controls.Add(this.checkBoxBW);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboEncoding);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelTextLine);
            this.Controls.Add(this.labelMethod);
            this.Controls.Add(this.labelMethodLine);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "BarcodeConfigDialog";
            this.Text = "Barcode";
            this.ActiveControl = textBoxText;
            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonOK;
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private void checkBoxBW_CheckedStateChanged(object sender, EventArgs e)
        {
            if (Barcode.ValidateText(this.textBoxText.Text, this.comboEncoding.SelectedIndex))
            {
                FinishTokenUpdate();
            }
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            if (Barcode.ValidateText(textBoxText.Text, comboEncoding.SelectedIndex))
            {
                textBoxText.BackColor = System.Drawing.SystemColors.Window;
                buttonOK.Enabled = true;
                FinishTokenUpdate();
            }
            else
            {
                textBoxText.BackColor = System.Drawing.Color.LightPink;
                buttonOK.Enabled = false;
            }
        }

        private void comboEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Barcode.ValidateText(textBoxText.Text, comboEncoding.SelectedIndex))
            {
                textBoxText.BackColor = System.Drawing.SystemColors.Window;
                buttonOK.Enabled = true;
                FinishTokenUpdate();
            }
            else
            {
                textBoxText.BackColor = System.Drawing.Color.LightPink;
                buttonOK.Enabled = false;
            }
        }
    }
}