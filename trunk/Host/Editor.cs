using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sketchpad.UI.Controls
{
    public partial class Editor : Form
    {
        private string _name;
        new string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Editor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _name = textBox1.Text;
        }
    }
}
