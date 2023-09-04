using Pariks.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;

namespace Pariks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            try                 //Обработка исключения
            {
                App.excelApp = new Excel.Application();     //Создать объект Excel
                App.excelApp.Visible = false;           //Не отображать пустой Excel
            }
            catch
            {
                MessageBox.Show("Установите MS Excel");
                this.Close();
            }
           
            App.DB = new Entity.PariksDB();
            App.categoriesDB = App.DB.Categories.ToList();
            App.productsDB = App.DB.Products.ToList();
        }

        public void btnPricelist_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(App.fileMenu))          //Проверить наличие документа
            {     //Открыть книгу Excel
                App.excelBook = App.excelApp.Workbooks.Open(App.fileMenu);  //Открыть книгу
                App.excelApp.Visible = true;        //Сделать Excel видимым
            }
            else
            { MessageBox.Show("Файл с меню отсутствует"); this.Close(); }
        }

        public void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            double money = random.Next(5000000, 10000000)/100.0;

            View.CreateOrder createCreateOrder = new View.CreateOrder(money);
            Hide();
            createCreateOrder.ShowDialog();
            Show();
        }

        public void btnKatal_Click(object sender, RoutedEventArgs e)
        {
            View.Autorization createAutorization = new View.Autorization();
            Hide();
            createAutorization.ShowDialog();
            Show();
        }

        public void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.excelApp.Quit();            //Выйти из Excel
                                            //Уничтожить все COM-объекты
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App.excelApp);
                //Заставляет сборщик мусора провести сборку мусора
                GC.Collect();
            }
            catch { }

            Close();
        }
    }


}
