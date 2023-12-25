using AbsenMVC.Model;
using MongoDB.Driver;
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

namespace absenxaml.View
{
    /// <summary>
    /// Interaction logic for DataMatkul.xaml
    /// </summary>
    public partial class DataMatkul : Window
    {
        private string oldValue = "";
        public List<Matkul> matkuls { get; set; }
        private MatkulManager matkulManager;
        public DataMatkul()
        {
            InitializeComponent();
            matkulManager = new MatkulManager();
            matkuls = matkulManager.getMatkul().AsQueryable().ToList<Matkul>();
            dgMatkul.ItemsSource = matkuls;

        }
    }
}
