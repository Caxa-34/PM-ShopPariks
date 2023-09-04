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
using System.Data.Entity.Core.Metadata.Edm;

namespace Pariks.View
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        List<Product> products;
        List<string> listCat;

        public double inOrder = 0;
        List<ProductInOrder> listProductsInOrders = new List<ProductInOrder>();

        public CreateOrder()
        {
            InitializeComponent();
        }

        public CreateOrder(double money)
        {
            InitializeComponent();
            txtOnCard.Text = $"На карте:\n{money} рублей";
            txtAmount.Text = $"Сумма заказа:\n0 рублей";
            App.onCard = money;

            listCategory.Items.Clear();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            listCat = new List<string>();

            foreach (Entity.Categories item in App.categoriesDB)
            {
                listCat.Add(item.categoryName);
            }

            listCategory.ItemsSource = listCat;
        }

        private void listCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string categoryName = listCategory.SelectedItem.ToString();
            int categoryId = listCategory.SelectedIndex + 1;

            products = new List<Product>();      
            Product product;                       
            
            for (int i = 1; i < App.productsDB.Count; i++)
            {
                if (App.productsDB[i].CategoryId == categoryId)
                {
                    product = new Product();
                    product.Name = App.productsDB[i].ProductName;
                    product.Image = Environment.CurrentDirectory + "/../../Categories/" + categoryName + "/" + App.productsDB[i].ProductImage + ".jpg";
                    Debug.WriteLine(product.Image);
                    product.Cost = App.productsDB[i].ProductCost;

                    products.Add(product);
                }
            }
            listProducts.ItemsSource = products;       
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            App.amount = inOrder;

            MakeOrder createMakeOrder = new MakeOrder(listProductsInOrders);

            Close();
            createMakeOrder.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductInOrder productInOrder = null;
            //Объект из списка (блюдо) в строке которой нажали кнопку
            Product product = (sender as Button).DataContext as Product;
            string productName = product.Name;		//Название блюда
            double productCost = product.Cost;			//Стоимость блюда
            if (inOrder + productCost <= App.onCard)  //Проверка под сумму на карте
            {
                inOrder += productCost;         //Общая сумма в заказе
                txtAmount.Text = $"Сумма заказа:\n{inOrder} рублей";
                //Поиск этого блюда среди заказанных блюд
                int index = listProductsInOrders.FindIndex(x => x.Name == productName);
                if (index < 0)        //Такого товара еще в заказе нет
                {
                    //Создаем новый элемент списка
                    productInOrder = new ProductInOrder();
                    productInOrder.Name = productName;
                    productInOrder.Cost = productCost;
                    productInOrder.Count = 1;   //Для нового
                    productInOrder.Costing = productCost;	//Стоимость
                    listProductsInOrders.Add(productInOrder);	//добавляем в список
                }
                else         //Такой товар уже есть в заказе, поэтому увеличиваем его количество 
                {
                    listProductsInOrders[index].Count++;
                    listProductsInOrders[index].Costing =
                                                listProductsInOrders[index].Cost * listProductsInOrders[index].Count;
                }
            }
            else
            {
                MessageBox.Show("У Вас уже не хватает денег");
            }
        }

    }
}
