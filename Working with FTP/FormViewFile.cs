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
            materialLabel3.Text += Form1.ShoySze();
            materialLabel4.Text += Form1.ShoyCount(); // Получени количество файлов
        }

        private void MaterialFlatButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
