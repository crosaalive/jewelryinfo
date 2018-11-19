using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace stonemgr
{
    public partial class showAll : Form
    {
        public showAll()
        {
            InitializeComponent();
        }

        private void showAll_Load(object sender, EventArgs e)
        {
            FormCollection collection = Application.OpenForms;
            foreach (Form form in collection)
            {
               textBox1.Text += (form.Name.ToString()) + "\r\n";
               textBox1.Text += (form.Text.ToString())+"\r\n";
               //form.WindowState = showAll();
                //if (form.Visible == false)
                //    form.Visible = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
