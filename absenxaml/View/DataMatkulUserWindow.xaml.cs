using AbsenMVC.Model;
using absenxaml.Manager;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for DataMatkulUserWindow.xaml
    /// </summary>
    public partial class DataMatkulUserWindow : Window
    {
        private UserManager userManager = new UserManager();
        private ObjectId userId;
        private dynamic listMatkulUser;
        private MatkulManager matkulManager = new MatkulManager();
        private User userData;

        public DataMatkulUserWindow(ObjectId selectedUserId)
        {
            InitializeComponent();
            this.userId = selectedUserId;
            refreshDataGrid();
            SetUpView();
        }

        private void SetUpView()
        {
            List<String> list = new List<String>();
            List<Matkul> listMatkul = matkulManager.getMatkul().AsQueryable().ToList();
            foreach (var item in listMatkul)
            {
                list.Add(item.Nama);
            }
            cbMatkul.ItemsSource = list;
            this.Title = "Data Matkul untuk " + userData.Nama;
            cbHari.ItemsSource = Utils.GetListHari();
        }

        private void refreshDataGrid()
        {
            dgMatkulUser.ItemsSource = null;

            List<BsonDocument> x = userManager.GetMatkulByUserId(userId);
            var dataMatkul = x.First()["dataMatkul"];
            x.First().Remove("dataMatkul");

            userData = BsonSerializer.Deserialize<User>(x.First());
            listMatkulUser = new List<dynamic>();
            var i = 0;
            foreach (var item in dataMatkul.AsBsonArray)
            {
                var y = BsonSerializer.Deserialize<Matkul>(item.ToBsonDocument());

                listMatkulUser.Add(new
                {
                    Id = y.Id.ToString(),
                    Nama = y.Nama,
                    JamMulai = userData.Matkul[i].JamMulai,
                    JamSelesai = userData.Matkul[i].JamSelesai,
                    Hari = userData.Matkul[i].Hari
                });
                i++;
            }


            dgMatkulUser.ItemsSource = listMatkulUser;
        }

        private void dgMatkulUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = dgMatkulUser.SelectedItem as dynamic;
            if (selectedItem == null)
            {
                return;
            }
            cbHari.SelectedItem = selectedItem.Hari;
            tpJamMulai.Text = selectedItem.JamMulai;
            tpJamSelesai.Text = selectedItem.JamSelesai;
            cbMatkul.SelectedItem = selectedItem.Nama;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var namaMk = cbMatkul.SelectedItem.ToString();
            var hari = cbHari.SelectedItem.ToString();
            var jamMulai = tpJamMulai.Text;
            var jamSelesai = tpJamSelesai.Text;
            Matkul matkul = matkulManager.GetMatkulByName(namaMk);
            MatkulItem matkulItem = new MatkulItem(matkul.Id, hari, jamMulai, jamSelesai);
            userManager.InsertOneMatkulToUser(userData.Id, matkulItem);
            MessageBox.Show("Sukses menambah data matkul!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Hand);
            refreshDataGrid();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dgMatkulUser.SelectedItem = null;
            cbHari.SelectedItem = null;
            cbMatkul.SelectedItem = null;
            tpJamMulai.Text = null; 
            tpJamSelesai.Text = null;
        }
    }
}
