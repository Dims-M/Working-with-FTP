using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class Form1 :  MaterialForm  // Form
    {
        public Form1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; // тема. 2 шт
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey500, Primary.BlueGrey900,Primary.BlueGrey500,Accent.LightBlue200,TextShade.WHITE);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Событие возникает при перетаскивания обьекта в область дропа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {

        }

        /// <summary>
        /// Событие при завершении бропа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {

        }

        /// <summary>
        ///Событие при выходе из зоны дропа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_DragLeave(object sender, EventArgs e)
        {

        }
    }
}
