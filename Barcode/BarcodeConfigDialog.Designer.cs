namespace Barcode
{
    partial class BarcodeConfigDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.comboEncoding = new System.Windows.Forms.ComboBox();
            this.checkBoxBW = new System.Windows.Forms.CheckBox();
            this.labelText = new System.Windows.Forms.Label();
            this.labelMethod = new System.Windows.Forms.Label();
            this.panelDivider = new System.Windows.Forms.Panel();
            this.panelTextLine = new System.Windows.Forms.Panel();
            this.panelMethodLine = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(254, 141);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(83, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(162, 141);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(83, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
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
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(6, 8);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(88, 15);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Text To Encode";
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
            // panelDivider
            // 
            this.panelDivider.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelDivider.Location = new System.Drawing.Point(7, 133);
            this.panelDivider.Name = "panelDivider";
            this.panelDivider.Size = new System.Drawing.Size(332, 1);
            this.panelDivider.TabIndex = 6;
            // 
            // panelTextLine
            // 
            this.panelTextLine.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelTextLine.Location = new System.Drawing.Point(7, 14);
            this.panelTextLine.Name = "panelTextLine";
            this.panelTextLine.Size = new System.Drawing.Size(332, 1);
            this.panelTextLine.TabIndex = 7;
            // 
            // panelMethodLine
            // 
            this.panelMethodLine.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelMethodLine.Location = new System.Drawing.Point(7, 63);
            this.panelMethodLine.Name = "panelMethodLine";
            this.panelMethodLine.Size = new System.Drawing.Size(332, 1);
            this.panelMethodLine.TabIndex = 8;
            // 
            // BarcodeConfigDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(345, 174);
            this.Controls.Add(this.panelDivider);
            this.Controls.Add(this.checkBoxBW);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboEncoding);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelMethod);
            this.Controls.Add(this.panelTextLine);
            this.Controls.Add(this.panelMethodLine);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BarcodeConfigDialog";
            this.Text = "Barcode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.ComboBox comboEncoding;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxBW;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelMethod;
        private System.Windows.Forms.Panel panelTextLine;
        private System.Windows.Forms.Panel panelMethodLine;
        private System.Windows.Forms.Panel panelDivider;
    }
}