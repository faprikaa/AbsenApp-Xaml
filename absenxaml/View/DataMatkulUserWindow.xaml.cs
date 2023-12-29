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
        private dynamic listMatkulUser = new List<dynamic>();
        private MatkulManager matkulManager = new MatkulManager();

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
            List<Matkul> listMatkul= matkulManager.getMatkul().AsQueryable().ToList();
            foreach (var item in listMatkul)
            {
                list.Add(item.Nama);
            }
            cbMatkul.ItemsSource = list;
        }

        private void refreshDataGrid()
        {
            dgMatkulUser.ItemsSource = null;

            List<BsonDocument> x = userManager.GetMatkulByUserId(userId);
            var dataMatkul = x.First()["dataMatkul"];
            x.First().Remove("dataMatkul");

            User userData = BsonSerializer.Deserialize<User>(x.First());
            this.Title = "Data Matkul untuk " + userData.Nama;

            var i = 0;
            foreach (var item in dataMatkul.AsBsonArray)
            {
                var y = BsonSerializer.Deserialize<Matkul>(item.ToBsonDocument());

                listMatkulUser.Add(new { Id = y.Id.ToString(), 
                    Nama = y.Nama, 
                    JamMulai = userData.Matkul[i].JamMulai, 
                    JamSelesai = userData.Matkul[i].JamSelesai, 
                    Hari = userData.Matkul[i].Hari });
                i++;
            }


            dgMatkulUser.ItemsSource = listMatkulUser;
        }

        private void dgMatkulUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = dgMatkulUser.SelectedItem as dynamic;
            tbHari.Text = selectedItem.Hari;
            tpJamMulai.Text = selectedItem.JamMulai;
            tpJamSelesai.Text = selectedItem.JamSelesai;
            cbMatkul.SelectedItem = selectedItem.Nama;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
