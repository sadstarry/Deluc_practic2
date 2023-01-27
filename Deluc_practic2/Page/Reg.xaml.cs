using Deluc_practic2.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegReg_Click(object sender, RoutedEventArgs e)
        {
            string LoginPerReg = LoginReg.Text.Trim().ToLower();
            string PasswordPerReg = PasswordReg.Text.Trim();
            string NamePerReg = NameReg.Text.Trim();

            if (Dbconnect.db.User.ToList().Find(x => x.Login == LoginPerReg) != null)
            {
                MessageBox.Show("Такой логин занят!");
            }
            if (LoginPerReg.Length > 0 && NamePerReg.Length > 0 && PasswordPerReg.Length > 0 && (!Regex.IsMatch(PasswordPerReg, @"\s")))
            {
                if (Regex.IsMatch(PasswordPerReg, @"\S{6,}") && Regex.IsMatch(PasswordPerReg, @"\d") && Regex.IsMatch(PasswordPerReg, @"[A-ZА-Я]") && Regex.IsMatch(PasswordPerReg, @"[!@#$%^]"))
                {
                    Dbconnect.db.User.Add(new User
                    {
                        Login = LoginPerReg,
                        Password = PasswordPerReg,
                        Name = NamePerReg,
                    });
                    Dbconnect.db.SaveChanges();
                    MessageBox.Show("Регистрация прошла успешно");
                    NavigationService.Navigate(new Auth());
                }
                else
                {
                    MessageBox.Show("Пароль должен соответствовать условиям \r\nМинимум 6 символов.\r\nМинимум 1 прописная буква.\r\nМинимум 1 цифра.\r\nМинимум один символ из набора: ! @ # $ % ^.\r\n");
                }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
    }
}
