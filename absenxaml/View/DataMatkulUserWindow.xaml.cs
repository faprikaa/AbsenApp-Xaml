using AbsenMVC.Model;
using absenxaml.Manager;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
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
        public DataMatkulUserWindow(ObjectId selectedUserId)
        {
            InitializeComponent();
            this.userId = selectedUserId;
            refreshDataGrid();
        }

        private void refreshDataGrid()
        {
            dgMatkulUser.ItemsSource = null;
            var list = new List<Matkul>();
            var list2 = new List<dynamic>();
            List<BsonDocument> x = userManager.GetMatkulByUserId(userId);
            var dataMatkul = x.First()["dataMatkul"];
            x.First().Remove("dataMatkul");
            User userData = BsonSerializer.Deserialize<User>(x.First());
            //var y = BsonSerializer.Deserialize<Matkul>(dataMatkul.ToBsonDocumen);
            var i = 0;
            foreach ( var item in dataMatkul.AsBsonArray)
            {
                Debug.WriteLine(item);
                var y = BsonSerializer.Deserialize<Matkul>(item.ToBsonDocument());
                list2.Add(new {id = y.Id.ToString(), nama = y.Nama, jam = userData.Matkul[i].Jam, hari = userData.Matkul[i].Hari });
                i++;
            }
            dgMatkulUser.ItemsSource = list2;


        }
    }
}
