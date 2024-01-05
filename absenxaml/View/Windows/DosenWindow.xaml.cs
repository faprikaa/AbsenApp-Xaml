using AbsenMVC.Model;
using absenxaml.Manager;
using absenxaml.View.Pages;
using absenxaml.View.Pages.Dosen;
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
    /// Interaction logic for DosenWindow.xaml
    /// </summary>
    public partial class DosenWindow : Window
    {
        private MatkulUserManager matkulUserManager = new MatkulUserManager();
        private ObjectId userId = ObjectId.Parse("658fa5f7a358110c2d0d6ca7");
        public DosenWindow()
        {
            InitializeComponent();
            var matkulId = matkulUserManager.GetCurrentMatkulId(userId);
            if (matkulId != null )
            {
                MainFrame.Navigate(new AbsenDosenPage(matkulId));
            } else
            {
                MainFrame.Navigate(new JadwalPage(userId));
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AbsenDosenPage(ObjectId.Parse("657b08352e39af52b5c8db53")));

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new JadwalPage(userId));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
