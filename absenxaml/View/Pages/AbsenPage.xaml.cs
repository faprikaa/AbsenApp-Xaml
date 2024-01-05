using AbsenMVC.Model;
using absenxaml.Manager;
using absenxaml.Model;
using MongoDB.Bson.Serialization;
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
using MongoDB.Driver;
using System.Diagnostics;

namespace absenxaml.View
{
    /// <summary>
    /// Interaction logic for AbsenWindow.xaml
    /// </summary>
    public partial class AbsenPage : Page
    {
        private AbsensiManager absensiManager = new AbsensiManager();

        public AbsenPage()
        {
            InitializeComponent();
            RefreshDataGrid();
            cbUser.Items.Clear();
            cbUser.ItemsSource = new List<string> { "Hadir", "Izin", "Cuti", "Absen" };
            tbUser.Text = "";
        }

        private void RefreshDataGrid()
        {
            dgAbsen.ItemsSource = null;
            List<dynamic> listDataJadi = new List<dynamic>();
            List<BsonDocument> listDataRaw = absensiManager.getAbsensiWithAggregate();
            listDataRaw.ForEach(data =>
            {
                listDataJadi.Add(BsonSerializer.Deserialize<dynamic>(data));
            });
            dgAbsen.ItemsSource = listDataJadi;
        }

        private void btnSimpan_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgAbsen.SelectedItem as dynamic;
            if (selected != null)
            {
                var newHadir = cbUser.SelectedItem as string;
                absensiManager.UpdateAbsensi(selected._id, newHadir);
                Utils.ShowMBInfo("Berhasil update data kehadiran");
                RefreshDataGrid();
            }
        }
            private void dgAbsen_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                var selected = dgAbsen.SelectedItem as dynamic;
                if (selected != null)
                {
                    tbUser.Text = selected.user.nama;
                    cbUser.SelectedItem = selected.absen;
                }
            }
        }
    }
