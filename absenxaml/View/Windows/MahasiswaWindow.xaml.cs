using absenxaml.Manager;
using absenxaml.Model;
using absenxaml.View.Pages.Dosen;
using absenxaml.View.Pages;
using MongoDB.Bson;
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

namespace absenxaml.View.Windows
{
    /// <summary>
    /// Interaction logic for MahasiswaWindow.xaml
    /// </summary>
    public partial class MahasiswaWindow : Window
    {
        private MatkulUserManager matkulUserManager = new MatkulUserManager();
        private ObjectId userId;
        private MatkulUser matkulUser;
        private bool isOnMatkulTime = false;
        public MahasiswaWindow(ObjectId mhsId)
        {
            InitializeComponent();
            userId = mhsId;
            matkulUser = matkulUserManager.GetCurrentMatkulUser(userId);

            if (matkulUser.Hari != null)
            {
                MainFrame.Navigate(new AbsenMhsPage(matkulUser));
                isOnMatkulTime = true;
            }
            else
            {
                MainFrame.Navigate(new JadwalPage(userId));
                isOnMatkulTime = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (isOnMatkulTime)
            {
                MainFrame.Navigate(new AbsenMhsPage(matkulUser));
            }
            else
            {
                MessageBox.Show("Saat ini tidak sedang dalam waktu belajar !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new JadwalPage(userId));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HistoryPage(userId));
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var mb = MessageBox.Show("Are you sure to logout ?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mb == MessageBoxResult.OK)
            {
                new LoginPage().Show();
                this.Close();
            }
        }
    }
}
