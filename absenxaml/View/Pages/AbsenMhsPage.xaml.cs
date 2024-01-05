using AbsenMVC.Model;
using absenxaml.Manager;
using absenxaml.Model;
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

namespace absenxaml.View.Pages
{
    /// <summary>
    /// Interaction logic for AbsenMhsPage.xaml
    /// </summary>
    public partial class AbsenMhsPage : Page
    {
        AbsensiManager absensiManager = new AbsensiManager();
        MatkulUserManager matkulUserManager = new MatkulUserManager();
        MatkulManager matkulManager = new MatkulManager();  
        private List<string> listCb = new List<string> { "Hadir", "Izin", "Cuti", "Absen" };
        private MatkulUser matkulUser;
        private Matkul matkul;
        private Absensi absensi;
        public AbsenMhsPage(MatkulUser matkulUser )
        {
            InitializeComponent();
            this.matkulUser = matkulUser;
            this.matkul = matkulManager.GetMatkulById(matkulUser.MatkulId);
            absensiManager.InsertIfNotExist(matkulUser);
            var currentTime = DateTime.Now;
            lbJam.Content = currentTime.Hour + " " + currentTime.Minute;
            lbMatkul.Content = matkul.Nama;
            lbTanggal.Content = currentTime.Date;
            cbKehadiran.Items.Clear();

            SetupView();
        }

        private void SetupView()
        {
            cbKehadiran.ItemsSource = listCb;
            absensiManager.InsertIfNotExist(matkulUser);
            absensi = absensiManager.GetAbsensiByMatkulUserId(matkulUser.Id);
            cbKehadiran.SelectedItem = absensi.Absen;
        }

        private void btnSimpan_Click(object sender, RoutedEventArgs e)
        {
            var newAbsen = cbKehadiran.SelectedItem as string;
            absensiManager.UpdateAbsensi(absensi.AbsenId, newAbsen);
            Utils.ShowMBInfo("Berhasil update kehadiran !");
            SetupView();
        }
    }
}
