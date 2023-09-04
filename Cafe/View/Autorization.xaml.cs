using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Pariks.View
{
    /// <summary>
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        int cntTry = 0;
        public Autorization()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (cntTry == 3)
            {
                Close();
            }

            string login = tbLogin.Text, pass = tbPassword.Text;
            if (login == App.login && pass == App.password)
            {
                View.Catalog catalog = new View.Catalog();
                Close();
                catalog.ShowDialog();
            }
            else
            {
                if (login != App.login) MessageBox.Show($"Такого пользователя не существует!\nОсталось попыток: {3 - cntTry}");
                else MessageBox.Show($"Неверный пароль!\nОсталось попыток: {3 - cntTry}");
                cntTry++;
            }
        }
    }
}
