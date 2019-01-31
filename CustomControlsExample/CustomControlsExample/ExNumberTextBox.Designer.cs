namespace CustomControlsExample
{
    partial class ExNumberTextBox
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
            this.button1 = new System.Windows.Forms.Button();
            this.customNumberTextBox2 = new CustomControls.Form.CustomNumberTextBox();
            this.customNumberTextBox1 = new CustomControls.Form.CustomNumberTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // customNumberTextBox2
            // 
            this.customNumberTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customNumberTextBox2.Editabled = true;
            this.customNumberTextBox2.ForeColor = System.Drawing.Color.Black;
            this.customNumberTextBox2.Location = new System.Drawing.Point(13, 41);
            this.customNumberTextBox2.Name = "customNumberTextBox2";
            this.customNumberTextBox2.Size = new System.Drawing.Size(196, 20);
            this.customNumberTextBox2.TabIndex = 1;
            this.customNumberTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // customNumberTextBox1
            // 
            this.customNumberTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customNumberTextBox1.Editabled = true;
            this.customNumberTextBox1.ForeColor = System.Drawing.Color.Black;
            this.customNumberTextBox1.Location = new System.Drawing.Point(13, 13);
            this.customNumberTextBox1.Name = "customNumberTextBox1";
            this.customNumberTextBox1.Size = new System.Drawing.Size(196, 20);
            this.customNumberTextBox1.TabIndex = 0;
            this.customNumberTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ExNumberTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 142);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.customNumberTextBox2);
            this.Controls.Add(this.customNumberTextBox1);
            this.Name = "ExNumberTextBox";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.Form.CustomNumberTextBox customNumberTextBox1;
        private CustomControls.Form.CustomNumberTextBox customNumberTextBox2;
        private System.Windows.Forms.Button button1;
    }
}

