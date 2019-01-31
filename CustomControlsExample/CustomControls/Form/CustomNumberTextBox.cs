using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;

namespace CustomControls.Form
{
    // Change the class which is inherited from TextBox:
    public partial class CustomNumberTextBox : TextBox
    {
        #region properties

        // Create the custom properties for the text box:
        private bool isSigned = true;

        private bool isDecimal = true;

        private bool isShowToolsTips = true;

        private int maxLenInt = 5;

        private int maxLenDec = 2;

        private bool showComma = true;

        private string toolTipText;
        // Where this is used will be show a little later
        private string placeHolder;
             
        // Set to true to accept negative numbers
        public bool IsSigned { set { isSigned = value; } }
        // Set to true to accep decimal numbers
        public bool IsDecimal { set { isDecimal = value; } }
        // Set the text of the place holder
        public string PlaceHolder { set { placeHolder = value; } }
       
        public bool ShowErrorToolTips { set { isShowToolsTips = value; } }
        
        public int MaxLenInt {  set { maxLenInt = value; } }

        public int MaxLenDec {  set{ maxLenDec = value; } }

        public bool ShowComma { set { showComma = value; } }
              
        public bool Editabled
        {
            set
            {
                if (!value)
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml(CONST_COLOR.disableColor);
                else
                    this.BackColor  = System.Drawing.ColorTranslator.FromHtml(CONST_COLOR.enableColor);

                this.ReadOnly = !value;
            }

            get
            {
                return !this.ReadOnly;
            }
        }

         public override string Text
        {
            set
            {
                base.Text = value;
                this.Lost_Focus(this,EventArgs.Empty);
            }
            get
            {
                return base.Text;
            }
        }

        #endregion

        #region constructor
        public CustomNumberTextBox()
        {
            InitializeComponent();
            this.TextAlign = HorizontalAlignment.Right;
            this.TextChanged += new EventHandler(this.PlaceHolder_Toggle);           
            this.FontChanged += new EventHandler(this.PlaceHolder_Toggle);
            this.GotFocus += new EventHandler(this.Got_Focus);
            this.LostFocus += new EventHandler(this.Lost_Focus);
            this.Click += new EventHandler(this.Got_Focus);
        }

        #endregion

        #region events
        // The function to enter only numbers:

        // By default this is active.
        // Allow only numbers: 0123456789
        private void Numbers(KeyPressEventArgs e)
        {
            // If the input is different from control key and digit key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Show the tool tip
                ShowMessage(toolTipText);
                // Set Handled method to true to cancel the button press.
                e.Handled = true;
            }
        }

        // The function to enter only signed numbers:

        // Allow numbers: 0123456789
        // and negative sign
        private void SignedNumbers(KeyPressEventArgs e)
        {
            // Get negative sign according your computer settings
            char negativeSign = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NegativeSign);
            if (e.KeyChar == negativeSign) // If the negative sign key is pressed
            {
                if ((this.SelectionLength > 0) && this.Text.Contains(negativeSign)) // When the whole number is selected and contains negative sign allow to enter negative sign
                    e.Handled = false;
                else if ((this.Text.Length > 0) && (this.SelectionStart != 0)) // Forbids entering negative sign when not in the very beginning
                    e.Handled = true;
                else if (this.Text.Contains(negativeSign)) // If text box contains decimal separator, then
                    e.Handled = true;  // cancel the key press
                else
                    e.Handled = false; // Allows entering negative sign in the very beginning
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) // If the input is different from control key and digit key
            {
                ShowMessage(toolTipText); // Show tool tip
                e.Handled = true; // Cancel the key press
            }            
        }

       // The function to enter only decimal numbers:

        // Allows numbers: 0123456789
        // and decimal separator
        private void DecimalNumbers(KeyPressEventArgs e)
        {
            // Get decimal separator according your computer settings
            char decimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (e.KeyChar == decimalSeparator) // If the decimal separator key is pressed
            {
                // When the whore number is selected and contains decimal separator
                // allow to enter decimal separator
                if ((this.SelectionLength > 0) && this.Text.Contains(decimalSeparator))
                {
                    this.Text = "0";
                    e.Handled = false;
                    this.SelectionStart = this.TextLength;
                }
                else if (this.Text.Length == 0)
                {
                    // If decimal separator key is pressed in the very beginning
                    // then a zero is inserted before it
                    this.Text = "0" + decimalSeparator;
                    e.Handled = true;
                    this.SelectionStart = this.TextLength;
                }
                // If text box contains decimal separator, then
                else if (this.Text.Contains(decimalSeparator))
                    e.Handled = true;     // cancel the key press
            }
            // If the input is different from control key and digit key
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                ShowMessage(toolTipText); // Show tooltip
                e.Handled = true; // Cancel the key press
            }
        }

        // The function to enter both decimal and signed numbers:

        // Allows numbers: 0123456789, decimal separator and negative sign
        private void DecimalSignedNumbers(KeyPressEventArgs e)
        {
            char decimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.
                                       NumberFormat.NumberDecimalSeparator);
            char negativeSign = Convert.ToChar(CultureInfo.CurrentCulture.
                           NumberFormat.NegativeSign);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                 && (e.KeyChar != decimalSeparator) && (e.KeyChar != negativeSign))
            {
                ShowMessage(toolTipText);
                e.Handled = true;
            }
            if (e.KeyChar == decimalSeparator)
            {
                if ((this.SelectionLength > 0) &&
                    this.Text.Contains(decimalSeparator))
                {
                    this.Text = "0";
                    e.Handled = true;
                    this.SelectionStart = this.TextLength;
                }
                else if (this.Text.Length == 0)
                {
                    this.Text = "0" + decimalSeparator;
                    e.Handled = true;
                    this.SelectionStart = this.TextLength;
                }
                else if (this.Text.Contains(decimalSeparator))
                    e.Handled = true;
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && this.Text.IndexOf(e.KeyChar.ToString())!=-1)
            {   
                ShowMessage(toolTipText);
                e.Handled = true;
            }
            if (e.KeyChar == negativeSign)
            {
                if ((this.SelectionLength > 0) && this.Text.Contains(negativeSign))
                    e.Handled = false;
                else if ((this.Text.Length > 0) && (this.SelectionStart != 0))
                    e.Handled = true;
                else if (this.Text.Contains(negativeSign))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (!this.isDecimal && e.KeyChar != decimalSeparator ))
            {
                ShowMessage(toolTipText);
                e.Handled = true;
            }
            
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            // Check what numbers is accepting the text box
            // and set tool tip message according that
            if (isSigned && isDecimal)
            {
                toolTipText = "Input field can only contain the following characters:"
                        + "\n- Numbers: 0123456789"
                        + "\n- Decimal Separator"
                        + "\n- Negative Sign";
                DecimalSignedNumbers(e);

            }
            else if (isDecimal)
            {
                toolTipText = "Input field can only contain the following characters:"
                        + "\n- Numbers: 0123456789"
                        + "\n- Decimal Separator";
                DecimalNumbers(e);
            }
            else if (isSigned)
            {
                toolTipText = "Input field can only contain the following characters:"
                        + "\n- Numbers: 0123456789"
                        + "\n- Negative sign";
                SignedNumbers(e);
            }
            else
            {
                toolTipText = "Input field can only contain the following characters:"
                        + "\n- Numbers: 0123456789";
                Numbers(e);
            }     
        }
        #endregion


        #region method
        // ToolBox control is initialized by this function.
        // Function to set parameters of the tool tip and to show it.
        void ShowMessage(string toolTipText)
        {
            // Parameters of the tool tip    
            int toolTipPosX = 0;
            int toolTipPosY = this.Height;
            int toolTipDuration = 3000;
            ToolTip toolTip = new ToolTip();
            // Set icon for the tool tip            
            toolTip.ToolTipIcon = ToolTipIcon.Warning;
            // Show the tool tip
            if (isShowToolsTips)
                toolTip.Show(toolTipText, this, toolTipPosX, toolTipPosY, toolTipDuration);
            else
                MessageBox.Show(this, toolTipText,"Validate Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SolidBrush myBrushDrawText = new SolidBrush(Color.DarkGray); // Set brush color
            Font drawFont = new Font("Microsoft Sans Serif", 8.25F); // Set font family and font size
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Set quality of the graphic
                                                                // Draw the placeholder text
            e.Graphics.DrawString(placeHolder, drawFont, myBrushDrawText, new Point(0, 0));
        }

        // Function to toggle the placeholder on/off

        private void Lost_Focus(object sender,EventArgs e)
        {
            PlaceHolder_Toggle(sender, e);
            if (showComma)
                AddComma();

            if (this.Text.ToString().StartsWith("-"))            
                this.ForeColor = System.Drawing.Color.Red;
            else
                this.ForeColor = System.Drawing.Color.Black;            
            if(checkIntegerLenth())
            {            
                ShowMessage("Too many integer " + maxLenInt + " digits the system allowed.");
                this.Focus();
            }

            if (this.Text.Equals("-"))
                this.Text = String.Empty;
        }

        private bool checkIntegerLenth()
        {          
            string str = this.Text;            
            if (str.IndexOf('.') != -1)
                str = str.Substring(0, str.IndexOf('.'));
            if (str.StartsWith("-"))
                str =str.Substring(1, str.Length - 1);
            if(str.Length!=0)
                str = double.Parse(str).ToString();
            return str.Length > maxLenInt;
        }

        private void Got_Focus(object sender,EventArgs e)
        {
            PlaceHolder_Toggle(sender, e);
            //RemoveComman();
        }

        private void RemoveComman()
        {
            double val = 0;
            double.TryParse(base.Text, out val);
            base.Text = val.ToString();
        }

        private void AddComma()
        {
            double val;
            string tformat = (!isDecimal ? "#": "0");
            string dec = tformat.PadRight(maxLenDec,tformat.ElementAt(0));
            if (double.TryParse(base.Text, out val))
                if (isDecimal)
                    base.Text = val.ToString("#,##0." + dec);
                else
                    base.Text = val.ToString("#,##0");
            if (!isSigned && base.Text.StartsWith("-"))
                base.Text= base.Text.Substring(1, base.Text.Length - 1);

        }


        private void PlaceHolder_Toggle(object sender, EventArgs e)
        {
            if (base.Text.Length <= 0)
                this.SetStyle(ControlStyles.UserPaint, true); // Placeholder - on
            else
                this.SetStyle(ControlStyles.UserPaint, false); // Placeholder - off
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            PlaceHolder_Toggle(null, null);
        }
        #endregion
    }
}
