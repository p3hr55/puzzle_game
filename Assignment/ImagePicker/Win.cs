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
    public partial class Win : Form
    {
        private static Win w; //Need to declare this from static context

        public Win()
        {
            InitializeComponent();
        }

        //To show this form in our main form
        public static void Show()
        {
            w = new Win();
            w.ShowDialog();
        }
    }
}
