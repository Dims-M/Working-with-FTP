using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentAce.Compression.Archiver;
using ComponentAce.Compression.ZipForge;

namespace util

{

    /// <summary>
    /// Методы чтения записи текстовых файлов. Отправка текс файлов на фтп
    /// </summary>
    public class Util
    {

        // обьект локер - Семофор для потоков. Блокирует достут множества потоков одновременно
        public static object loker = new object(); // обьект синхронизаций

        /// <summary>
        /// метод для заsрузки c указанием параметров
        /// </summary>
        /// <param name="Adress">Адресс имя фтп</param>
        /// <param name="filePath">Путь к нужному файлу для отправки</param>
        /// <param name="user">Имя пользователя от фтп</param>
        /// <param name="pass">Пароль от фтп</param>
        void UploadFile(string Adress, string filePath, string user, string pass)
        {

            try
            {
                // создание обьекта для запроса к фтп. С указанием адреса и пути к нужному файлу
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(Adress + "/" + Path.GetFileName(filePath));

                // выбор нужного метода(загрузка или отправка)
                req.Method = WebRequestMethods.Ftp.UploadFile;

                // обьект для работы с Фтп. В качестве параметров указываем пользователя и пароль фтп
                req.Credentials = new NetworkCredential(user, pass);

                //клиент должен инициировать подключение по порту данных. Значение по умолчанию
                req.UsePassive = true;

                // какой тип передачи данных
                req.UseBinary = true;

                // зарыть соединение после запроса
                req.KeepAlive = true;

                // создаем поток для работы с данными + передаем файл в поток
                Stream stream = File.OpenRead(filePath);

                // массив байтов c длинной потока
                byte[] buffer = new byte[stream.Length];

                // считования потока, офсет, длина массива
                stream.Read(buffer, 0, buffer.Length);

                // закрытия потока
                stream.Close();

                // поток для выгрузки на фтп
                Stream regstr = req.GetRequestStream();

                //запись в поток массива байтов
                regstr.Write(buffer, 0, buffer.Length);

                regstr.Close();

                string tempMessage = "файл ушел на фтп";
                // MessageBox.Show(tempMessage);
                Console.WriteLine(tempMessage);


            }
            catch (Exception ex)
            {
                //MessageBox.Show("Что то пошло не так " + ex);
                Console.WriteLine("Что то пошло не так " + ex);
                string tempErrorLog = $"Произошла ошибка" + ex;

            }
        }

        // метод загрузки Тестовой 
        /// <summary>
        /// Отправка файла на фтп
        /// </summary>
        public void Upload()
        {
            // запись пользователя винды
            string user = Environment.UserName;
            // запуск метода загрузки на фтп с параметрами. 
            //  UploadFile(textBox1.Text, "/C:/1.txt","etesftpmail","D51215045");
            // юкоз
            // UploadFile(@"ftp://tesftpmail.ucoz.net", @"C:\adb\test.txt", @"etesftpmail", @"D51215045");
            // beget.com
            //UploadFile(@"ftp://b91790o4.beget.tech", @"C:\adb\test.jpg", @"b91790o4", @"YhvvI89Y");
            //отправка бд битрейда
            // UploadFile(@"ftp://b91790o4.beget.tech", @"C:\BETRADE2\btrade.db3", @"b91790o4", @"YhvvI89Y");C:\BETRADE2\BTRADE2.7z
            UploadFile(@"ftp://kassa16.ru/Alar/Oktyabrskaya23/", @"C:\BETRADE2\BTRADE2.7z", @"Iskan", @"6350025");
        }


        public void arvihBD()
        {
            string adresFtp = @"ftp://kassa16.ru/Alar/Oktyabrskaya23/";
            string myLogin = @"iskan";
            string myPass = @"6350025";
            string pathBD = @"C:\BETRADE2\BTRADE2.7z";
            // string pathBD = @"C:\BETRADE2\btrade.db3";

            Console.WriteLine("Запуск архивирования БД товаров");

            UploadFile(adresFtp, pathBD, myLogin, myPass);
            Console.WriteLine("....*****....");
            Console.ReadKey();

        }

        /// <summary>
        /// Отправка 1 файла на сервак 
        /// </summary>
        /// <param name="_pathBD"></param>
        public void arvihBDSParams(string _pathBD)
        {
            string adresFtp = @"ftp://kassa16.ru/Alar/Oktyabrskaya23/";
            string myLogin = @"iskan";
            string myPass = @"6350025";
            string pathBD = _pathBD;
            // string pathBD = @"C:\BETRADE2\btrade.db3";

          //  Console.WriteLine("Запуск архивирования БД товаров");

            UploadFile(adresFtp, pathBD, myLogin, myPass);
           // Console.WriteLine("....*****....");
           // Console.ReadKey();
        }

        /// <summary>
        /// Отправка нескольких файлов с помощью Листа
        /// </summary>
        /// <param name="_pathBD"></param>
      async  public void ArvihBDSParamsMassif(List<string> _pathBD)
        {
            string adresFtp = @"ftp://kassa16.ru/Alar/Oktyabrskaya23/";
            string myLogin = @"iskan";
            string myPass = @"6350025";
            //string[] pathBD = _pathBD;
            // string pathBD = @"C:\BETRADE2\btrade.db3";

            //Console.WriteLine("Запуск архивирования БД товаров");

            foreach (var itemMass in _pathBD)
            {
                 UploadFile(adresFtp, itemMass.ToString(), myLogin, myPass);
            }

           // UploadFile(adresFtp, pathBD, myLogin, myPass);
           // Console.WriteLine("....*****....");
            //Console.ReadKey();
        }

        /// <summary>
        /// Получаем имя файл с разширением из строки 
        /// </summary>
        /// <param name="stringPath"></param>
        /// <returns></returns>
        public string GetNameIsFail(string stringPath)
        {
            //Получаем имя файла. 
             string[] splitpath = stringPath.Split(' '); //разбиваем по сплиту по слешам
             string name = splitpath[splitpath.Length - 1];// берем последниие данные из массива 
             return name;
        }

        //Получение списка файлов на FTP по умолчанию
        public List<string> Getting_List_Files()
        {
            string adresFtp = @"ftp://kassa16.ru/Alar/Oktyabrskaya23/";
            string myLogin = @"iskan";
            string myPass = @"6350025";

            //Создаем подключение к серверу 
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(adresFtp);

            //Устанавливаем лог и пароль
            request.Credentials = new NetworkCredential(myLogin, myPass);

            //Устанавливаем какой нужен метод ждя работы с фтп
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails; //Получаем список файлов с фтп

            //метод ответа // получаем ответ от сервера в виде объекта FtpWebResponse
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Поток для ответа с сервера // получаем поток ответа 
            Stream responseStream = response.GetResponseStream();

            //Чтение потока
            StreamReader reader = new StreamReader(responseStream);

            List<string> listDataFtp = new List<string>(); // хранение списка файлов

            string line;

            while((line = reader.ReadLine()) != null)
            {
                listDataFtp.Add(line); // добавляем в список.
            }

           // listDataFtp.Add(reader.ReadToEnd()); // добавляем в список.

            //Console.WriteLine(reader.ReadToEnd());

            //Закрываем соединеие и потоки
            reader.Close();
            responseStream.Close();
            response.Close();
            // Console.Read();

            return listDataFtp; //возращем лист.
        }

        /// <summary>
        /// Скачивание файлов с фтп
        /// </summary>
        public void DownloadFileFtp()
        {
            const string adresFtp = @"ftp://kassa16.ru/Alar/Oktyabrskaya23/";
            const string myLogin = @"iskan";
            const string myPass = @"6350025";

            string filaSave = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}"; // место для сохранения файлов

            //Получаем имя файла. 
            // string[] splitpath = patchFile.Split('/'); //разбиваем по сплиту по слешам
            // string name = splitpath[splitpath.Length - 1];// берем последниие данные из массива 

            // Создаем объект FtpWebRequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(adresFtp);

            //Устанавливаем лог и пароль
            request.Credentials = new NetworkCredential(myLogin, myPass);

            // устанавливаем метод на загрузку файлов
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // получаем ответ от сервера в виде объекта FtpWebResponse
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {

                // получаем поток ответа
                Stream responseStream = response.GetResponseStream();

                // сохраняем файл в дисковой системе
                // создаем поток для сохранения файла
                FileStream fs = new FileStream("newTest.txt", FileMode.Create);

                //Буфер для считываемых данных
                byte[] buffer = new byte[64];
                int size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, size);
                }

            }
        }

        public void DownloadFileFtpS_fTP(string namePath)
        {
             string adresFtp1 = @"ftp://kassa16.ru/Alar/Oktyabrskaya23/";
           string namePathFail = adresFtp1 + namePath;

            const string myLogin = @"iskan";
            const string myPass = @"6350025";

            string filaSave = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}"; // место для сохранения файлов

            //Получаем имя файла. 
            // string[] splitpath = patchFile.Split('/'); //разбиваем по сплиту по слешам
            // string name = splitpath[splitpath.Length - 1];// берем последниие данные из массива 

            // Создаем объект FtpWebRequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(namePathFail);

            //Устанавливаем лог и пароль
            request.Credentials = new NetworkCredential(myLogin, myPass);

            // устанавливаем метод на загрузку файлов
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // получаем ответ от сервера в виде объекта FtpWebResponse
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {

                // получаем поток ответа
                Stream responseStream = response.GetResponseStream();

                // сохраняем файл в дисковой системе
                // создаем поток для сохранения файла
                FileStream fs = new FileStream(filaSave +"\\"+ namePath, FileMode.Create);

                //Буфер для считываемых данных
                byte[] buffer = new byte[64];
                int size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, size);
                }

            }

        }

        //метод выбора нужного файла через окно выбора
        /// <summary>
        /// Метод выбора нужного файла. В консоле не работет. 
        /// </summary>
        void GetPath()
        {
            // создание обьекта для работы выбора нужного файла
            //  OpenFileDialog ofd = new OpenFileDialog();
            // маска выбора типа файлов
            //    ofd.Filter = " Выбрать все (*.*)|*.*|" + "(*.*) |*.*";
            //    ofd.Title = "Выберете необходимый файл";
            //    //  выбор начальной папки для запуска OpenFileDialog  метод StartupPath получает имя файла зупустившего этот метод 
            //    ofd.InitialDirectory = System.Windows.Forms.Application.StartupPath;

            //    try
            //    {
            //        // если нажата кнопка ОК
            //        if (ofd.ShowDialog() == DialogResult.OK)
            //        {
            //            StreamReader sr = new StreamReader(ofd.FileName);

            //            textBox3.Text = getFileName(sr);
            //        }

            //    }

            //    catch (Exception ex)
            //    {
            //        string tempErrorLogg = "Ошибка в методе выбора пути /r/n" + ex;
            //        MessageBox.Show(tempErrorLogg);
            //        label1.Text = tempErrorLogg;

            //    }

            //}

        }

        /// <summary>
        /// Запись файла с указанием пути нахождения файла
        /// </summary>
        /// <param name=" patch">Путь к файлу.</param>
        public static void ZapisFailaPatch(string patch, string text)
        {
            Console.WriteLine("Тестовой вывод");

            DateTime now = DateTime.Now; // получение тек времени
            string tempDateTime = now.ToString();

            Console.WriteLine($"Тестовой вывод \t\n{tempDateTime}\t\n{text}");

            using (var sw = new StreamWriter(patch, true, System.Text.Encoding.Default)) // создание обьект потока для записи файла
            {
                // sw.Write("\t\n");
                sw.Write($"{tempDateTime}" + "\t\n");
                sw.Write(text);
                sw.Write("\t\n");
                Console.WriteLine("Записанно в файл"); //проверочный вывод
            }
            // Console.WriteLine("Записанно в файл"); //проверочный вывод
            Console.ReadKey(true);

        }

        /// <summary>
        /// Запись в файл рядом с exe и передачей текста в метод
        /// </summary>
        /// <param name="text">Текст что нужно записать в лог по умолчанию</param>
        public static void ZapisFailaText(string text)
        {
            DateTime now = DateTime.Now; // получение тек времени
            string tempDateTime = now.ToString();
            using (var sw = new StreamWriter("Log.txt", true, Encoding.Default)) // создание обьект потока для записи файла
            {
                //System.Text.Encoding.Default
                // sw.Write("\t\n");
                sw.Write($"{tempDateTime}" + "\t\n");
                sw.Write(text);
                sw.Write("\t\n");
            }
        }


        /// <summary>
        /// рандомнон заполнение текстого файла набором цифр
        /// </summary>
        public static void RandomZapisFailaInt()
        {
            lock (loker)
            {

                DateTime now = DateTime.Now; // получение тек времени
                string text = $"Содержимое файла: \t\nДобавлено:{now} ";
                Random random = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    text += random.Next(10000);
                    text += "\t\n";
                }

                ZapisFailaText(text);

            } // обьект локер - Семофор для потоков. Блокирует достут множества потоков одновременно

            Console.WriteLine("Работа цикла по заполнению значениями закончена");
        }


        /// <summary>
        /// Чтене файла(лога) по умолчанию
        /// </summary>
        public static void ChteniefailaLoga()
        {
            string tempLog = "Log.txt";
            string tempText = "Содержимое файла \t\n";

            using (var sr = new StreamReader(tempLog, System.Text.Encoding.Default)) // обьект для чтение потока при чтении файла с жестого диска  
            {
                // var text = sr.ReadLine().ToString();           
                tempText += sr.ReadToEnd().ToString();
            }
            Console.WriteLine(tempText);
            Console.ReadKey(true);
        }

        /// <summary>
        /// чтение файла с указанием пути.
        /// </summary>
        /// <param name="Path"></param>
        public static void ChteniefailaFailaPath(string Path)
        {
            string tempLog = "Log.txt";
            string tempText = "Содержимое файла \t\n";

            using (var sr = new StreamReader(tempLog, Encoding.Unicode)) // обьект для чтение потока при чтении файла с жестого диска  
            {
                //Encoding.Default
                // var text = sr.ReadLine().ToString();  // чтение построчно      
                tempText += sr.ReadToEnd().ToString(); // чтение файла полностью
            }
            Console.WriteLine(tempText); //проверочный вывод
            Console.ReadKey(true);
        }

    }

    /// <summary>
    /// Класс для работы с архиваторами..
    /// </summary>
    public class ZipFilesByMask
    {
        private static string pathArhivHille = @"c:\1\HiddenFolder";
        private static string pathArhiv = @"c:\1";

        /// <summary>
        /// запуск архивирования с настройками по умолчанию
        /// </summary>
        public static void Checet()
        {
            // Create an instance of the ZipForge class
            ZipForge archiver = new ZipForge();

            try
            {
                archiver.FileName = @"C:\test.zip"; //Куда сохранить файл результата сжатия
                archiver.OpenArchive(System.IO.FileMode.Create); //Настраиваем дллку на работу с новым архивом
                archiver.BaseDir = @"C:\123"; //Папка где лежат все файлы для взятия
                                              // archiver.AddFiles("*.exe"); //Берём все файлы с расширением exe
                                              // archiver.AddFiles("*.dll");
                                              //archiver.AddFiles("*.jpg");
                archiver.AddFiles("*.*");

                // archiver.BaseDir = @"D:\";
                // archiver.AddFiles(@"d:\file.txt"); //Добавим один файл
                // archiver.AddFiles(@"d:\Test"); //Запакуем ещё и папку
                archiver.CloseArchive(); //Закрываем архив
            }
            //Ловим ошибки
            catch (ArchiverException ae)
            {
                Console.WriteLine("Message: {0}\t Произошла ошибка: {1}",
                                  ae.Message, ae.ErrorCode);
                Console.ReadLine();
            }
            Console.WriteLine("Процес архивирования завершен....\t\n");
        }

        /// <summary>
        /// Архивация файла с праметром места хранения. Тамже архив и создается.
        /// </summary>
        /// <param name="pathFail"></param>
        public static void ArhivPathFail(string pathFail, string endPathFail)
        {
            // Create an instance of the ZipForge class
            ZipForge archiver = new ZipForge();

            try
            {
                //archiver.FileName = @"C:\test.zip"; //Куда сохранить файл результата сжатия
                archiver.FileName = endPathFail; //Куда сохранить файл результата сжатия

                archiver.OpenArchive(System.IO.FileMode.Create); //Настраиваем дллку на работу с новым архивом

                // archiver.BaseDir = @"C:\123"; //Папка где лежат все файлы для взятия
                // archiver.IsValidArchiveFile();

                archiver.AddFiles(pathFail); // Добавляем в архив нужный файл
                archiver.Password = "123"; // пароль 

                // archiver.BaseDir = pathFail; //Папка где лежат все файлы для взятия

                // archiver.AddFiles("*.exe"); //Берём все файлы с расширением exe
                // archiver.AddFiles("*.dll");
                //archiver.AddFiles("*.jpg");
                //archiver.AddFiles("*.*");

                // archiver.BaseDir = @"D:\";
                // archiver.AddFiles(@"d:\file.txt"); //Добавим один файл
                // archiver.AddFiles(@"d:\Test"); //Запакуем ещё и папку
                archiver.CloseArchive(); //Закрываем архив
            }
            //Ловим ошибки
            catch (ArchiverException ae)
            {
                Console.WriteLine("Message: {0}\t Произошла ошибка: {1}",
                                  ae.Message, ae.ErrorCode);
                Console.ReadLine();
            }
            Console.WriteLine("Процес архивирования завершен....\t\n");
        }

        /// <summary>
        /// Распаковка архива в папку по умолчанию
        /// </summary>
        public static void RaspakovkaArhiva()
        {
            ZipForge archiver = new ZipForge();
            NoVisiblePapka();
            try
            {
                archiver.FileName = @"C:\test.zip"; //Необходимый файл
                archiver.OpenArchive(System.IO.FileMode.Open); //Указываем что хотим сделать
                                                               // archiver.BaseDir = pathArhiv; //Папка куда распаковать
                archiver.BaseDir = pathArhivHille; //Папка куда распаковать
                //archiver.BaseDir = @"C:\1"; //Папка куда распаковать
                // archiver.ExtractFiles("*.exe"); //Распаковываем *.exe
                // archiver.ExtractFiles("*.dll"); //Распаковываем *.dll
                archiver.ExtractFiles("*.*");

                //archiver.BaseDir = @"D:\";
                // archiver.ExtractFiles("*.txt"); //Распаковываем *.txt но можем и просто файл
                archiver.ExtractFiles("test"); //Распаковываем папку
                archiver.CloseArchive();

                Console.WriteLine("Архив разохвивирован");
            }
            // Catch all exceptions of the ArchiverException type
            catch (ArchiverException ae)
            {
                Console.WriteLine("Message: {0}\t Ошибка при архивации: {1}", ae.Message, ae.ErrorCode);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Сделать скрытую папку 
        /// </summary>
        public static void NoVisiblePapka()
        {
            // string path = @"c:\1\HiddenFolder"; // путь где будет находится наша папка
            string path = pathArhivHille; // путь где будет находится наша папка

            //Проверка. Если данный путь существует. Если нет то создается
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; //создание скрытой папка
            }
        }

    }

    /// <summary>
    /// Пример кода Drag_and_Drop
    /// </summary>
    public class Method_Drag_and_Drop
    {

        /// <summary>
        /// Пример пунктирной линии на панели Drag_and_Drop
        /// </summary>
        //public void ShoyGrafifDrod()
        //{
        //    float[] dashes = { 2, 2, 2, 2 }; // указываем размер пунктира, растояние и так далее
        //    Pen pen = new Pen(Color.Black); // цвет пунктира
        //    pen.DashPattern = dashes; // патерн шаблон, маска
        //    Graphics graphics = panel1.CreateGraphics(); //Обьект для раблты с графикой
        //                                                 //  graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // зглаживание пунктиров

        //    //while (true)
        //    //{
        //    //    graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1); //Рисуем нужную фигуру. В данном случаее пряугольник

        //    //}
        //    graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1); //Рисуем нужную фигуру. В данном случаее пряугольник
        //}

        //async public void ShoyGrafifDrod2()
        //{
        //    {
        //        await Task.Run(async () =>
        //        {
        //            Pen pen = new Pen(Color.Black, 2);
        //            for (int i = 30; i > 2; i--, await Task.Delay(10)) // настройки скорости пунктиров
        //            {
        //                panel1.CreateGraphics().Clear(SystemColors.Control);
        //                pen.DashPattern = new float[] { 2, i };
        //                panel1.CreateGraphics().DrawRectangle(pen, 1, 1, panel1.Width - 2, panel1.Height - 2);


        //            }
        //        });

        //    }
        //}

        /// <summary>
        /// Открытие окна выбора файла для Drag_and_Drop
        /// </summary>
        public void Clik_Drag_and_Drop()
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); //создание обьекта для работы с открытием файла
            fileDialog.Title = "Укажите файл для загрузки на FTP"; // заголовок окна

            fileDialog.InitialDirectory = @"C:\Users\Dmytriy"; // указываем откуда из какого источника открывать окно выбора 

            //Запуск формы выбора файла 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
               // materialLabel1.Text += fileDialog.FileName;
               //materialLabel1.Text += "\t\n";
            }
        }

        #region Пример кода 
        //public void ReceptionFile()
        //{

        //    string[] files = new string[];
        //    int count;
        //    List<string> myList;
        //    // materialLabel2.Text = "Файл перетащен. Еще перетащить?";

        //    files = (string[])e.Data.GetData(DataFormats.FileDrop); // Передаем файл в с отрибутом дроп. Записываем в мссив строк с преодрозованием к массиву

        //    myList = new List<string>();
        //    count = 0;

        //    foreach (var item in files)
        //    {
        //        if (Directory.Exists(item)) // проверяем. Кинули нам папку или отдельный файл
        //        {
        //            // materialLabel1.Text += "Является директорией, брошенной папкой )))\t\n";

        //            myList.AddRange(Directory.GetFiles(item, "*.*", SearchOption.AllDirectories)); // добавляем в лист пути к файлам из папки.Будуд добавленны все файлы по маске bp dct gjlrfnjkjujd 
        //            materialLabel1.Text += "\t\n";
        //        }

        //        else if ((Directory.Exists(item)) != true) // проверяем. Кинули нам папку или отдельный файл
        //        {
        //            // materialLabel1.Text += "Является файлом  )))\t\n";

        //            myList.Add(item); //  добавление в лист

        //            //   materialLabel1.Text += string.Join("\t\n",myList); // конвертация листа в стринг и вывод 
        //            materialLabel1.Text += "\t\n";
        //        }

        //        materialLabel1.Text += string.Join("\t\n", myList); // конвертация листа в стринг и вывод 

        //        #region Старые методы
        //        ////Вывод в лог
        //        //else if (count != 3 ) 
        //        //{
        //        //    // materialLabel1.Text += "Показаны только 3 первых файла";
        //        //    //break;
        //        //    materialLabel1.Text += item;
        //        //    count++;
        //        //    materialLabel1.Text += "\t\n";
        //        //}

        //        //else
        //        //{
        //        //    //materialLabel1.Text += item;
        //        //    //materialLabel1.Text += "\t\n";

        //        //    materialLabel1.Text += "Показаны только 3 первых файла";
        //        //    break;
        //        //}

        //        #endregion
        //    }
        //}
        #endregion
    }


    #region Разные примеры кода

    //Получаем имя файла. 
    // string[] splitpath = patchFile.Split('/'); //разбиваем по сплиту по слешам
    // string name = splitpath[splitpath.Length - 1];// берем последниие данные из массива 

    //Место куда сохранить  загруженный файл
   // string filaSave = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}";

    #endregion

}
