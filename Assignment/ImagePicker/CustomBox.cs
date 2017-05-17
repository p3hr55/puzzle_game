using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagePicker
{
    public partial class CustomBox : Form
    {
        private static string selectedText;
        private static CustomBox box;
        private static DialogResult result = DialogResult.No;

        public CustomBox()
        {
            InitializeComponent();
        }

        //Weird way of returning from static context
        public static int Show()
        {
            box = new CustomBox();
            result = box.ShowDialog();
            char[] m = selectedText.ToCharArray(); //We are converting the text to a char array

            if (m[0] == 'G')
                return 15;

            return int.Parse(m[0].ToString()); //Returning the integer value of first char element
        }

        //Which radio button is currently pressed
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                selectedText = ((RadioButton)sender).Text;
        }

        //Exiting the form
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
