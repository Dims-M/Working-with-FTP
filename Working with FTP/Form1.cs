using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        int count;
       static List<string> myList = new List<string>();


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

           // myList = new List<string>();
           count = 0;

            foreach (var item in files)
            {
                if (Directory.Exists(item)) // проверяем. Кинули нам папку или отдельный файл
                {
                    // materialLabel1.Text += "Является директорией, брошенной папкой )))\t\n";

                    myList.AddRange(Directory.GetFiles(item, "*.*",SearchOption.AllDirectories)); // добавляем в лист пути к файлам из папки.Будуд добавленны все файлы по маске bp dct gjlrfnjkjujd 
                   
                }

              else  if ((Directory.Exists(item))!= true) // проверяем. Кинули нам папку или отдельный файл
                {
                   // materialLabel1.Text += "Является файлом  )))\t\n";

                    myList.Add(item); //  добавление в лист

                 //   materialLabel1.Text += string.Join("\t\n",myList); // конвертация листа в стринг и вывод 
                    materialLabel1.Text += "\t\n";
                }

              //  materialLabel1.Text += string.Join("\t\n", myList); // конвертация листа в стринг и вывод 
                materialLabel1.Text = ShoyList(myList);
                #region Старые методы
                ////Вывод в лог
                //else if (count != 3 ) 
                //{
                //    // materialLabel1.Text += "Показаны только 3 первых файла";
                //    //break;
                //    materialLabel1.Text += item;
                //    count++;
                //    materialLabel1.Text += "\t\n";
                //}

                //else
                //{
                //    //materialLabel1.Text += item;
                //    //materialLabel1.Text += "\t\n";

                //    materialLabel1.Text += "Показаны только 3 первых файла";
                //    break;
                //}

                #endregion
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
            materialLabel2.Text = "Перетащите файлы или кликните";
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

        //Кнопка лабел **
        private void MaterialLabel1_Click(object sender, EventArgs e)
        {

        }

        //lДвойной клик на надписи**
        private void MaterialLabel2_Click(object sender, EventArgs e)
        {
           

        }

        //Клик на панели дропа.Выход окна выбора файла
        private void Panel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); //создание обьекта для работы с открытием файла
            fileDialog.Title = "Укажите файл для загрузки на FTP"; // заголовок окна

            fileDialog.InitialDirectory = @"C:\Users\Dmytriy"; // указываем откуда из какого источника открывать окно выбора 

            //Запуск формы выбора файла 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
               // materialLabel1.Text += fileDialog.FileName;
                myList.Add(fileDialog.FileName);

                materialLabel1.Text = ShoyList(myList);
               // materialLabel1.Text += string.Join("\t\n", myList); // конвертация листа в стринг и вывод 
                materialLabel1.Text += "\t\n";
            }

        }
        //Кнопка просмотра загруженных файлов
        private void MaterialRaisedButton3_Click(object sender, EventArgs e)
        {
            FormViewFile formViewFile = new FormViewFile();
            formViewFile.Show();
        }

        //Методы!!!!!!!!!!!!!!

      public static string ShoyList(List<string> myList, byte coontt = 0)
        {
            byte count = coontt; //по умолчанию
            string tempList = "";

            foreach (var item in myList)
            {
                if(count != 3)
                {
                    tempList += item;
                    tempList += "\t\n";
                    count++;
                }

                else
                {
                    tempList += "Показаны последние 3 файла!!! \t\n";
                    break;
                }
            }

            return tempList;

        }

        public static string ShoyList(byte i = 100)
        {
            return ShoyList(myList,i);
        }


    }
}
