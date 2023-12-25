using AbsenMVC.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        private void onCellValueChanged(object sender, DataGridCellEditEndingEventArgs e)
        {
            var item = e.Row.Item as Matkul;
            var header = e.Column.Header.ToString();
            var element = e.EditingElement as TextBox;
            var newValue = element.Text;

            var filter = Builders<Matkul>.Filter.Eq("_id", item.Id);
            var query = Builders<Matkul>.Update.Set(header, newValue);
            matkulManager.UpdateMatkul(filter, query);
            refreshDataGrid();

        }

        public void btnOpenAddForm_click(object sender, RoutedEventArgs e)
        {
            AddMatkul addMatkul = new AddMatkul(matkulManager, this);
            addMatkul.Show();
        }

        public void refreshDataGrid()
        {
            dgMatkul.ItemsSource = null;
            matkuls = matkulManager.getMatkul().AsQueryable().ToList<Matkul>();
            dgMatkul.ItemsSource = matkuls;

        }
    }
}
