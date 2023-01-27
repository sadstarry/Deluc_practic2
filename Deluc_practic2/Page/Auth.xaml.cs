using Deluc_practic2.Components;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Deluc_practic2.Page
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth 
    {
        string LoginAuth = "";
        string PasswordAuth = "";


        public Auth()
        {
            InitializeComponent();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            LoginAuth = Login.Text.Trim().ToLower();
            PasswordAuth = Password.Text.Trim();
            if (LoginAuth.Length == 0 && PasswordAuth.Length == 0)
            {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                var AuthUser = Dbconnect.db.User.ToList().Find(x => x.Login == LoginAuth && x.Password == PasswordAuth);
                if(AuthUser == null)
                {
                    MessageBox.Show("Не правильный логин или пароль!");
                }
                else
                {
                    GlobalPerements.nameuser = AuthUser;
                    NavigationService.Navigate(new Kalendar());

                }
            }
        }

        private void RegAuth_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Reg());
        }
    }
}
