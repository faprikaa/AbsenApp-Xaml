using absenxaml.Manager;
using absenxaml.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace absenxaml.View.Pages
{
    /// <summary>
    /// Interaction logic for AbsenDosenPage.xaml
    /// </summary>
    public partial class AbsenDosenPage : Page
    {
        AbsensiManager absensiManager = new AbsensiManager();
        MatkulUserManager matkulUserManager = new MatkulUserManager();
        private List<string> listCb = new List<string> { "Hadir", "Izin", "Cuti", "Absen" };
        private dynamic dataMatkul;
        private dynamic dataMatkulUser;
        private ObjectId matkulId;
        private ObjectId userId;
        private MatkulUser matkulUser;
        private Absensi dosenAbsensi;
        public AbsenDosenPage(MatkulUser matkulUser)
        {
            InitializeComponent();
            absensiManager.InsertIfNotExist(matkulUser);
            this.matkulId = matkulUser.MatkulId;
            this.userId = matkulUser.UserId;
            this.matkulUser = matkulUser;
            RefreshDataGrid();
            SetupView();
        }

        private void RefreshDataGrid()
        {
            dosenAbsensi = absensiManager.GetAbsensiByMatkulUserId(matkulUser.Id);

            cbKehadiranDosen.SelectedItem = dosenAbsensi.Absen;
            this.DataContext = this;
            dgAbsenDosen.ItemsSource = null;
            var listMhs = matkulUserManager.GetMahasiswaByMatkulUser(matkulUser);
            Console.WriteLine("APSD" + listMhs.ToJson());

            var listJadi = new List<dynamic>();
            foreach (var item in listMhs)
            {
                var jadi = BsonSerializer.Deserialize<dynamic>(item);
                Console.WriteLine(jadi.ToString());
                listJadi.Add(jadi);
            }

            this.dataMatkul = listJadi.First().matkul;
            this.dataMatkulUser = listJadi.First();
            dgAbsenDosen.ItemsSource = listJadi;


        }

        private void dgAbsenDosen_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selected = dgAbsenDosen.SelectedItem as dynamic;
            if (selected !=null)
            {
                tbNamaMhs.Text = selected.user.nama;
                cbKehadiranMhs.SelectedItem = selected.absensi.absen;
            }

        }

        private void btnUpdateMhs_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgAbsenDosen.SelectedItem as dynamic;
            if (selected != null)
            {
                ObjectId absenId = selected.absensi._id;
                Console.WriteLine(absenId.ToString());
                string newAbsen = cbKehadiranMhs.SelectedValue.ToString();
                absensiManager.UpdateAbsensi(absenId, newAbsen);
                Utils.ShowMBInfo("Berhasil update kehadiran !");
                RefreshDataGrid();
            }
        }

        private void SetupView()
        {
            cbKehadiranMhs.Items.Clear();
            cbKehadiranMhs.ItemsSource = listCb;
            cbKehadiranMhs.SelectedIndex = 3;
            cbKehadiranDosen.Items.Clear();
            cbKehadiranDosen.ItemsSource = listCb;
            cbKehadiranDosen.SelectedIndex = 3;
            lbMatkul.Content = dataMatkul.nama;
            lbTanggal.Content = DateTime.Now.ToString();
            lbJam.Content = dataMatkulUser.jam_mulai + " - " + dataMatkulUser.jam_selesai;
        }

        private void btnSimpanDosen_Click(object sender, RoutedEventArgs e)
        {
            var kehadiranDosen = cbKehadiranDosen.SelectedItem as string;
            if ( kehadiranDosen != null)
            {
                absensiManager.UpdateAbsensi(dosenAbsensi.AbsenId, kehadiranDosen);
                Utils.ShowMBInfo("Berhasil update kehadiran !");
                RefreshDataGrid();
            }

        }
    }
}
