using Cafe.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Pariks.View
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        List<Product> products;
        List<string> listCat;

        double m;

        public CreateOrder()
        {
            InitializeComponent();
            
        }

        public CreateOrder(double money)
        {
            InitializeComponent();
            txtOnCard.Text = $"На карте:\n{money} рублей";
            txtAmount.Text = $"Сумма заказа:\n{money - 20.12} рублей";
            m = money;
            listCategory.Items.Clear();
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
            if (File.Exists(App.fileMenu))          //Проверить наличие документа
            {     //Открыть книгу Excel
                App.excelBook = App.excelApp.Workbooks.Open(App.fileMenu);  //Открыть книгу
            }
            else
            { MessageBox.Show("Файл с меню отсутствует"); this.Close(); }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //Подготовить структуры данных для заполнения	//Элемент интерфейса ListBox
            listCat = new List<string>();
            //Получить все категории из коллекции всех листов книги
            foreach (Excel.Worksheet item in App.excelBook.Worksheets)
            {
                listCat.Add(item.Name);		//Поместить название листа в список
            }
            //Переместить из построенного списка в элемент интерфейса
            listCategory.ItemsSource = listCat;
        }

        private void listCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string categoryName = listCategory.SelectedItem.ToString();
            products = new List<Product>();         //Создать список продуктов 
            Product product;                        //Объявить отдельный продукт

            foreach (Excel.Worksheet item in App.excelBook.Worksheets)
            {
                if (item.Name == categoryName)
                {
                    App.excelCells = item.Cells;
                }
            }
            //Связь с листом Excel с названием выбранной категории                                     //Получить заполненные ячейки Excel на листе
                                                            //Считываем все данные из ячеек Excel название и цена, заполняем объект
            for (int row = 1; row <= App.excelCells.Rows.Count; row++)
            {
                if (App.excelCells[row, 1].value2 == null) break;
                product = new Product();			//Создать отдельный продукт
                //Заполнить поля объекта product из ячеек Excel
                product.Name = App.excelCells.Cells[row, 1].value2;	//Название блюда в объект
                product.Image = Environment.CurrentDirectory + "/../../Categories/" + categoryName + "/" + product.Name + ".jpg";
                product.Count = 1;
                product.Cost = App.excelCells.Cells[row, 2].value2;
                Debug.WriteLine(product.Image);
                products.Add(product);			//Занесение блюда в список
            }
            Debug.WriteLine("!\n!\n!");
            listProducts.ItemsSource = products;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.excelApp.Quit();            //Выйти из Excel
                                            //Уничтожить все COM-объекты
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App.excelApp);
            //Заставляет сборщик мусора провести сборку мусора
            GC.Collect();

            Close();
        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            App.amount = m - random.Next(10000, 50000) / 100.0;


            View.MakeOrder createMakeOrder = new View.MakeOrder();

            App.excelApp.Quit();            //Выйти из Excel
                                            //Уничтожить все COM-объекты
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App.excelApp);
            //Заставляет сборщик мусора провести сборку мусора
            GC.Collect();

            Close();
            createMakeOrder.ShowDialog();
        }
    }
}
