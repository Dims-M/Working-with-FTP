using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Working_with_FTP
{
    public partial class FormViewFile : Form
    {
        public FormViewFile()
        {
            InitializeComponent();
            materialLabel2.Text += Form1.ShoyList();
        }

        private void MaterialFlatButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
