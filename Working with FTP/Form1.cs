using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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


        //При загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            Graphics graphics = panel1.CreateGraphics(); //Обьект для раблты с графикой
            graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1); //Рисуем нужную фигуру. В данном случаее пряугольник
        }

        //Клик на пнел
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            ShoyGrafifDrod(); 
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


            #region Старые методы. 
            materialLabel2.Text = "Файл перетащен. Еще перетащить?";

            files = (string[])e.Data.GetData(DataFormats.FileDrop); // Передаем файл в с отрибутом дроп. Записываем в мссив строк с преодрозованием к массиву

            // myList = new List<string>();
            count = 0;

            foreach (var item in files)
            {
                if (Directory.Exists(item)) // проверяем. Кинули нам папку или отдельный файл
                {
                    // materialLabel1.Text += "Является директорией, брошенной папкой )))\t\n";

                    myList.AddRange(Directory.GetFiles(item, "*.*", SearchOption.AllDirectories)); // добавляем в лист пути к файлам из папки.Будуд добавленны все файлы по маске bp dct gjlrfnjkjujd 

                }

                else if ((Directory.Exists(item)) != true) // проверяем. Кинули нам папку или отдельный файл
                {
                    // materialLabel1.Text += "Является файлом  )))\t\n";

                    myList.Add(item); //  добавление в лист

                    //   materialLabel1.Text += string.Join("\t\n",myList); // конвертация листа в стринг и вывод 
                    materialLabel1.Text += "\t\n";
                }

                //  materialLabel1.Text += string.Join("\t\n", myList); // конвертация листа в стринг и вывод 
                materialLabel1.Text = ShoyList(myList);
                #endregion
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


        // НЕ РАБОТАЕТ
        public void PolucheniListaDate()
        {
        //    materialLabel2.Text = "Файл перетащен. Еще перетащить?";

        //    files = (string[])e.Data.GetData(DataFormats.FileDrop); // Передаем файл в с отрибутом дроп. Записываем в мссив строк с преодрозованием к массиву

        //    // myList = new List<string>();
        //    count = 0;

        //    foreach (var item in files)
        //    {
        //        if (Directory.Exists(item)) // проверяем. Кинули нам папку или отдельный файл
        //        {
        //            // materialLabel1.Text += "Является директорией, брошенной папкой )))\t\n";

        //            myList.AddRange(Directory.GetFiles(item, "*.*", SearchOption.AllDirectories)); // добавляем в лист пути к файлам из папки.Будуд добавленны все файлы по маске bp dct gjlrfnjkjujd 

        //        }

        //        else if ((Directory.Exists(item)) != true) // проверяем. Кинули нам папку или отдельный файл
        //        {
        //            // materialLabel1.Text += "Является файлом  )))\t\n";

        //            myList.Add(item); //  добавление в лист

        //            //   materialLabel1.Text += string.Join("\t\n",myList); // конвертация листа в стринг и вывод 
        //            materialLabel1.Text += "\t\n";
        //        }

        //        //  materialLabel1.Text += string.Join("\t\n", myList); // конвертация листа в стринг и вывод 
        //        materialLabel1.Text = ShoyList(myList);
        //    }
        }

            /// <summary>
            /// Очистка листа
            /// </summary>
            public static void DeleteFailSorse()
        {
            for (int i =0; i<myList.Count; i++)
            {
                myList.RemoveRange(0, myList.Count);
            }

           
        }
        /// <summary>
        /// Метод для вывода содержимого листа первые 3 значения
        /// </summary>
        /// <param name="myList"></param>
        /// <param name="coontt"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Вывод списка со значениям по умолчанию
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ShoyList(byte i = 100)
        {
            return ShoyList(myList,i);
        }

        /// <summary>
        /// Получение количества файлов
        /// </summary>
        /// <returns></returns>
        public static int ShoyCount()
        {
            return myList.Count;
        }
        /// <summary>
        /// Метод для получения размера файлов в мегобайт
        /// </summary>
        /// <returns></returns>
        public static int ShoySze()
        {
            //string path = @"C:\apache\hta.txt";
            //FileInfo fileInf = new FileInfo(path);
            //if (fileInf.Exists)
            //{
            //    Console.WriteLine("Имя файла: {0}", fileInf.Name);
            //    Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
            //    Console.WriteLine("Размер: {0}", fileInf.Length);
            //}

            string pathSorse;
            FileInfo fileInf2; // = new FileInfo(path);
            int sizeFail = 0;

            foreach (var tempItem in myList)
            {
                pathSorse = tempItem; // полученте пути.
                fileInf2 = new FileInfo(pathSorse); // обьект для работы с 
                sizeFail += (int)fileInf2.Length;

            }

           
           // return ((sizeFail / 8)/1024)/1024;
            return sizeFail / 1048576;
        }

        /// <summary>
        /// Статический пунктр для поле 
        /// </summary>
        public void ShoyGrafifDrod()
        {
            float[] dashes = {2,2,2,2 }; // указываем размер пунктира, растояние и так далее
            Pen pen = new Pen(Color.Black); // цвет пунктира
            pen.DashPattern = dashes; // патерн шаблон, маска
            Graphics graphics = panel1.CreateGraphics(); //Обьект для раблты с графикой
                                                         //  graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // зглаживание пунктиров

            //while (true)
            //{
            //    graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1); //Рисуем нужную фигуру. В данном случаее пряугольник

            //}
            graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1); //Рисуем нужную фигуру. В данном случаее пряугольник
        }

        /// <summary>
        /// Вывод онимащионной рамки.
        /// </summary>
        async public void ShoyGrafifDrod2()
        {
            {
                await Task.Run(async () =>
                {
                    Pen pen = new Pen(Color.Black, 2);
                    for (int i = 30; i > 2; i--, await Task.Delay(9)) // настройки скорости пунктиров
                    {
                        panel1.CreateGraphics().Clear(SystemColors.Control);
                        pen.DashPattern = new float[] { 2, i };
                        panel1.CreateGraphics().DrawRectangle(pen, 1, 1, panel1.Width - 2, panel1.Height - 2);
  
                    }

                });

            }
        }

        /// <summary>
        /// Работа с прогресс баром.
        /// </summary>
        async public void RabPrpgressBar()
        {
            int countItemListData =myList.Count; // Количесво элементов в листе
            string path = @"J:\Музыка\Videos\клип";
            string path1 = @"C:\Users\Dmytriy\Downloads\temp";
            string pathList = @"C:\Users\Dmytriy\Downloads\temp";

            // string[] files = Directory.GetFiles(pathList, "*"); // получаем имена и пути к файлам
            string[] files = new string[countItemListData+1];

            for (int i =0; i < countItemListData; i++ )
            {
              files[i] = myList[i];
            }

            progressBar1.Maximum = 0;
            progressBar1.Maximum = countItemListData; //указываем максимаьную длину прогресса бара от длины массива

            if (countItemListData !=0) 
            {
                progressBar1.Visible = true; //Видимость прогресс бара
            }
            

            for (int i =0; i< countItemListData; i++)
            {
                progressBar1.Value++;
                await Task.Delay(200); // остановка на заданное время
            }

            materialLabel1.Text = $"Количество элементо: {countItemListData}";
           // progressBar1.Value++;

        }

      public void  RabPrpgressBarDownloadFile() // запускаем прогрес бар
        {
           // materialLabel1.Text = $"Количество элементо: {10}";
            progressBar1.Visible = true; //Видимость прогресс бара
            progressBar1.Value++;
        }

        /// <summary>
        /// Работа с сетью WebClient скачка файла по прямой ссылки
        /// </summary>
        public void RabWebClient()
        {
          // string patchFile = @"ftp://tesftpmail.ucoz.net/ofd.txt";
            string patchFile2 = @"https://cloud.mail.ru/public/2jPT/5B3UCrdHT";
            string patchFile = @"https://download.virtualbox.org/virtualbox/6.0.8/VirtualBox-6.0.8-130520-Win.exe";

            //Получаем имя файла. 
            string[] splitpath = patchFile.Split('/'); //разбиваем по сплиту по слешам
            string name = splitpath[splitpath.Length - 1];// берем последниие данные из массива 

            //Место куда сохранить  загруженный файл
            string filaSave = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}";

            string fullNmame =filaSave+"/" + name; // совмещаем место сохранения и имя файла

            using (WebClient wc = new WebClient()) // обьект для рабоой с передачей и подключением с сетью
            {
                //Узнать размер файла который нужно скачать
                wc.OpenRead(patchFile);
                string size =(Convert.ToDouble(wc.ResponseHeaders["Content-Length"])/ 1048576).ToString("#.#"); // получаем количество загружены мегобайтов

                // событие на изменение загрузки. Передаем в прогрец бар 
                wc.DownloadProgressChanged += (s, e) => 
                {  
                    materialLabel1.Text = $"Происходит Скачивание файла:{fullNmame} \t\n" +
                   $"Скачалось из{size} по {e.ProgressPercentage}% ({((double)e.BytesReceived / 1048576).ToString("#.#")} МБ)"; // получаем количество загружены мегобайтов

                    progressBar1.Value = e.ProgressPercentage; };

                progressBar1.Visible = true; //Видимость прогресс бара
                progressBar1.Value++;

                //Скачиваем файл
                wc.DownloadFileAsync(new Uri (patchFile), fullNmame);

            }
        }
        

        public void PolochenoeNameUrlString(){

            Regex regex = new Regex("11");


        }


        //Кнопка ок
            private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            // ShoyGrafifDrod();
            ShoyGrafifDrod2(); // красивый курсор
           // RabPrpgressBar(); // запускаем прогрес бар

            RabWebClient(); //запуск скачки по прямой ссылки

        }
        //**
        private void Form1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
