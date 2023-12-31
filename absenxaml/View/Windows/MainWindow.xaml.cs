﻿using absenxaml.View;
using absenxaml.View.Pages;
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

namespace absenxaml.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new DataMatkul());
        }

        public void userHeader_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DataUser());
        }
        
        public void matkulHeader_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DataMatkul());
        }

        private void absenHeader_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AbsenPage());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
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
