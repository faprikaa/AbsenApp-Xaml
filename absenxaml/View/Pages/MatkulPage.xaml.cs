using AbsenMVC.Model;
using absenxaml.Manager;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class DataMatkul : Page
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
            var newMk = tbNama.Text;
            if(newMk != "")
            {
                matkulManager.InsertNewMatkul(new Matkul(newMk));
                Utils.ShowMBInfo("Berhasil menambah mata kuliah !");
            }
            refreshDataGrid();
        }

        public void btnDelMk_click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgMatkul.SelectedItem as Matkul;
            var selectedItems = dgMatkul.SelectedItems as List<Matkul>;
            if (selectedItem == null)
            {
                MessageBox.Show("Please choose one matkul", "tes");
            }
            else
            {
                var d = MessageBox.Show("hapus matkul " + selectedItem.Nama + " ?", "Konfirmasi", MessageBoxButton.OKCancel);
                if (d == MessageBoxResult.OK)
                {
                    matkulManager.DeleteMatkul(selectedItem.Id);
                    refreshDataGrid();
                }

            }

        }



        public void refreshDataGrid()
        {
            dgMatkul.ItemsSource = null;
            matkuls = matkulManager.getMatkul().AsQueryable().ToList<Matkul>();
            dgMatkul.ItemsSource = matkuls;

        }

        private void dgMatkul_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = dgMatkul.SelectedItem as Matkul;
            if (selected != null)
            {
                tbNama.Text = selected.Nama;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgMatkul.SelectedItem as Matkul;
            if (selected != null)
            {
                var newVal = tbNama.Text;
                var filter = Builders<Matkul>.Filter.Eq(m => m.Nama, selected.Nama);
                var q = Builders<Matkul>.Update.Set(m => m.Nama, newVal);
                matkulManager.UpdateMatkul(filter, q);
                Utils.ShowMBInfo("Berhasil update data matkul !");
                refreshDataGrid();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbNama.Text = "";
            dgMatkul.SelectedItem = null;
        }
    }
}
