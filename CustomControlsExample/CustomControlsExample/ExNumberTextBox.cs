using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControlsExample
{
    public partial class ExNumberTextBox : Form
    {
        public ExNumberTextBox()
        {
            InitializeComponent();
           // this.customNumberTextBox1.IsDecimal = true;
           // this.customNumberTextBox1.IsSigned = false;
           // this.customNumberTextBox1.ShowErrorToolTips = false;
            this.customNumberTextBox1.Text = "-1,000.00";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customNumberTextBox1.Editabled = false;
        }
    }
}
