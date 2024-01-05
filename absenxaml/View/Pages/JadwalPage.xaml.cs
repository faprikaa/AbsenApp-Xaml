using AbsenMVC.Model;
using absenxaml.Manager;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

namespace absenxaml.View.Pages.Dosen
{
    /// <summary>
    /// Interaction logic for JadwalPage.xaml
    /// </summary>
    public partial class JadwalPage : Page
    {
        private MatkulUserManager matkulUserManager = new MatkulUserManager();

        public JadwalPage(ObjectId userId)
        {
            InitializeComponent();
            SetupView(userId);
        }

        private void SetupView(ObjectId userId)
        {
            dgJadwal.ItemsSource = null;
            var listDataRaw = matkulUserManager.GetJadwalByUserId(userId);
            var listJadi = new List<dynamic>();
            foreach (var item in listDataRaw)
            {
                var jadi = BsonSerializer.Deserialize<dynamic>(item);
                Console.WriteLine(item.ToString());
                listJadi.Add(jadi);
            }
            dgJadwal.ItemsSource = listJadi;
        }
    }
}
