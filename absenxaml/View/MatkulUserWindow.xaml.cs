using AbsenMVC.Model;
using absenxaml.Manager;
using absenxaml.Model;
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
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace absenxaml.View
{
    /// <summary>
    /// Interaction logic for DataMatkulUserWindow.xaml
    /// </summary>
    public partial class DataMatkulUserWindow : Window
    {
        private UserManager userManager = new UserManager();
        private MatkulUserManager matkulUserManager;
        private dynamic listMatkulUser;
        private MatkulManager matkulManager = new MatkulManager();
        private User userData;

        public DataMatkulUserWindow(ObjectId selectedUserId)
        {
            InitializeComponent();
            matkulUserManager = new MatkulUserManager();
            SetUpView(selectedUserId);
            refreshDataGrid();
        }

        private void SetUpView(ObjectId selectedUserId)
        {
            cbMatkul.ItemsSource = matkulManager.getMatkul().AsQueryable().ToList();
            userData = userManager.GetUserById(selectedUserId);
            this.Title = "Data Matkul untuk " + userData.Nama;
            cbHari.ItemsSource = Utils.GetListHari();
        }

        private void refreshDataGrid()
        {
            dgMatkulUser.ItemsSource = null;
            List<dynamic> listMatkulUser = new List<dynamic>();
            List<BsonDocument> listDataRaw = matkulUserManager.getMatkulUserByUserId(userData.Id);
            foreach (dynamic dataRaw in listDataRaw) {
                var dataMatkul = dataRaw["DataMatkul"];
                dataRaw.Remove("DataMatkul");
                var matkulUser = BsonSerializer.Deserialize<MatkulUser>(dataRaw);
                var matkul = BsonSerializer.Deserialize<Matkul>(dataMatkul);
                listMatkulUser.Add(new
                {
                    MatkulId = matkul.Id.ToString(),
                    MatkulUserId = matkulUser.Id.ToString(),
                    Nama = matkul.Nama,
                    JamMulai = matkulUser.JamMulai,
                    JamSelesai = matkulUser.JamSelesai,
                    Hari = matkulUser.Hari
                });


            }
            dgMatkulUser.ItemsSource = listMatkulUser;
        }

        private void dgMatkulUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = dgMatkulUser.SelectedItem as dynamic;
            Debug.WriteLine(dgMatkulUser.SelectedItem);

            if (selectedItem == null)
            {
                return;
            }

            cbHari.SelectedItem = selectedItem.Hari;
            tpJamMulai.Text = selectedItem.JamMulai;
            tpJamSelesai.Text = selectedItem.JamSelesai;
            cbMatkul.SelectedValue = ObjectId.Parse( selectedItem.MatkulId);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var mkId = cbMatkul.SelectedValue.ToString();
            var hari = cbHari.SelectedItem.ToString();
            var jamMulai = tpJamMulai.Text;
            var jamSelesai = tpJamSelesai.Text;
            MatkulUser matkulUser = new MatkulUser(ObjectId.Parse(mkId), userData.Id,hari, jamMulai, jamSelesai);
            matkulUserManager.InsertNewMatkulUser(matkulUser);
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedOption = dgMatkulUser.SelectedItem as dynamic;
            if (selectedOption == null)
            {
                Utils.ShowMBWarning("Harap pilih salah satu item");
            }
            var mkId = cbMatkul.SelectedValue;
            var hari = cbHari.SelectedItem.ToString();
            var jamMulai = tpJamMulai.Text;
            var jamSelesai = tpJamSelesai.Text;
            MatkulUser newMatkuluser = new MatkulUser(
                ObjectId.Parse(mkId.ToString()),
                userData.Id,
                hari,
                jamMulai,
                jamSelesai
            );
            matkulUserManager.UpdateById(ObjectId.Parse(selectedOption.MatkulUserId), newMatkuluser );
            Utils.ShowMBInfo("Berhasil update data !");
            refreshDataGrid();

        }

        private void btnHapus_Click(object sender, RoutedEventArgs e)
        {
            var selectedOption = dgMatkulUser.SelectedItem as dynamic;
            var mb = MessageBox.Show("Anda yakin menghapus data ini?", "Kofirmasi", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mb == MessageBoxResult.OK) { 
                matkulUserManager.DeleteById(ObjectId.Parse(selectedOption.MatkulUserId));
                refreshDataGrid();
            }
            btnClear_Click(sender, e);  
        }
    }
}
