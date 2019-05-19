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

        string[] files;


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Cвойства панели
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))  // получаем данные в форме кидаемого Файла с типом дроп
            {
                materialLabel2.Text = "Мышь отпусти!! Изверг!!";
                e.Effect = DragDropEffects.Copy; //Происходит копирование файла
            }
            
        }

        /// <summary>
        /// Момент отпускания файла в область переброса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            
            materialLabel2.Text = "Файл перетащен. Еще перетащить?";

           files = (string[])e.Data.GetData(DataFormats.FileDrop); // Передаем файл в с отрибутом дроп. Записываем в мссив строк с преодрозованием к массиву

            int count = 0;
            foreach (var item in files)
            {
                if (count != 3 )
                {
                    materialLabel1.Text += item;
                    count++;
                    materialLabel1.Text += "\t\n";
                }
                   
                else
                {
                    materialLabel1.Text += "Показаны только 3 первых файла";
                    break;
                }
            } 
        }

        
        private void Panel1_DragLeave(object sender, EventArgs e)
        {
          //  Перетащите файлы
           
            materialLabel2.Text = "КУда?? Бросай файлы";

        }

        //Кнопка очистить
        private void MaterialRaisedButton2_Click(object sender, EventArgs e)
        {
            materialLabel2.Text = "Перетащите файлы";
            materialLabel1.Text = "\t\n";
        }

        /// <summary>
        /// Кнопка выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaterialFlatButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка лабел 
        private void MaterialLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
