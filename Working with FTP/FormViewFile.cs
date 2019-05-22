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

        /// <summary>
        /// Elfkbnm dct 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaterialFlatButton2_Click(object sender, EventArgs e)
        {
            Form1.DeleteFailSorse();
            materialLabel2.Text = "Очищенно.";
        }

        //Просmотреть файлы на сервере.
        private void MaterialFlatButton3_Click(object sender, EventArgs e)
        {
            GeFilFtp(); // получение списка файлов
        }


        //МЕТОДЫ

        /// <summary>
        /// Получение списка файлов с фтп
        /// </summary>
        List<string> GeFilFtp()
        {
            util.Util MyUtil = new util.Util(); // обьект для работы с фтп

            string tempList = "Содержимое фтр \t\n";

            var listFileData = MyUtil.Getting_List_Files(); //получаем список файлов с фтп

            foreach (var itemElementFileFtp in listFileData)
            {
                tempList += itemElementFileFtp;
                tempList += "\t\n";
            }
            materialLabel2.Text = tempList;
            materialLabel4.Text = listFileData.Count().ToString();

            return listFileData;
        }

        void DownloadFile()
            {
         util.Util MyUtil = new util.Util(); // обьект для работы с фтп

        var temp = GeFilFtp(); // получение списка файлов с фтп
        string tenmListFail = "Cjlth;bvjt \t\n";

        string temFailList = "";
            // MyUtil.ArvihBDSParamsMassif(temp);

            foreach (var tempNameFiles in temp)
            {
                tenmListFail += temFailList;
                tenmListFail += "\t\n";

                temFailList = MyUtil.GetNameIsFail(tempNameFiles);

                MyUtil.DownloadFileFtpS_fTP(temFailList);
            }
    materialLabel2.Text = tenmListFail;
            }


        //Кнопка Скачивание файлов 
        private void MaterialFlatButton4_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }
    }
}
