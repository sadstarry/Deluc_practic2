using Deluc_practic2.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Логика взаимодействия для Kalendar.xaml
    /// </summary>
    public partial class Kalendar
    {
        public Kalendar()
        {
            InitializeComponent();
            ListProduct.ItemsSource = Dbconnect.db.Napominanie.Where(x => x.IsDelete != true && x.UserID == GlobalPerements.nameuser.ID).ToList();
            NameUserHello.Text = GlobalPerements.nameuser.Name;
            sort();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try {
                DateTime DateZad = (DateTime)calensar.SelectedDate;
                string Zad = Zadacha.Text.Trim();

                DateTime today = DateTime.Today;
                if (DateZad < today)
                {
                    MessageBox.Show("Нельзя ставить задачу на прошедшее время");
                }
                else
                {
                    if(Zad.Length > 0)
                    {
                        Dbconnect.db.Napominanie.Add(new Napominanie
                        {
                            Date = DateZad,
                            text = Zad,
                            UserID = GlobalPerements.nameuser.ID,
                            IsDelete = false
                        });
                        Dbconnect.db.SaveChanges();
                        MessageBox.Show("Сохранено");
                        Zadacha.Text = "";
                        var nap = Dbconnect.db.Napominanie.Where(x => x.IsDelete != true && x.UserID == GlobalPerements.nameuser.ID).ToList();
                        var update = new List<Napominanie>();
                        for (int i = 0; i < nap.Count; i++)
                        {
                            if (nap[i].IsDelete != true)
                            {
                                update.Add(nap[i]);
                            }
                        }
                        ListProduct.ItemsSource = update.ToList();
                    //list = Dbconnect.db.Napominanie.Where(x => x.IsDelete != true && x.UserID == GlobalPerements.nameuser.ID).ToList();
                }
                    else
                    {
                        MessageBox.Show("Заполните поля!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Заполните поля!");
            }
        }

        private void sort()
        {
            var nap = Dbconnect.db.Napominanie.Where(x => x.IsDelete != true && x.UserID == GlobalPerements.nameuser.ID).ToList();
            var update = new List<Napominanie>();
            if (ListProduct != null)
            {
                for (int i = 0; i < nap.Count; i++)
                {
                    DateTime today = DateTime.Today;
                    if (nap[i].Date < today)
                    {
                        nap[i].IsDelete = true;
                        Dbconnect.db.SaveChanges();

                    }
                    if (nap[i].IsDelete != true)
                    {
                        update.Add(nap[i]);
                    }
                }
                ListProduct.ItemsSource = update.ToList();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var BtnProd = (sender as Button).DataContext as Napominanie;
            GlobalPerements.delete = Dbconnect.db.Napominanie.Where(x => x.ID == BtnProd.ID).FirstOrDefault();

            Napominanie Nap = Dbconnect.db.Napominanie.Where(x => x.ID == GlobalPerements.delete.ID).FirstOrDefault();

            try
            {
                var nap = Dbconnect.db.Napominanie.Where(x => x.IsDelete != true && x.UserID == GlobalPerements.nameuser.ID).ToList();
                var update = new List<Napominanie>();
                if (MessageBox.Show("Вы точно хотите удалить данную задачу?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                
                }
                else
                {
;
                    Nap.IsDelete = true;
                    Dbconnect.db.SaveChanges();
                    MessageBox.Show("Задача удалена");
                    for (int i = 0; i < nap.Count; i++)
                    {
                        if (nap[i].IsDelete != true)
                        {
                            update.Add(nap[i]);
                        }
                    }
                }
                ListProduct.ItemsSource = update.ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
