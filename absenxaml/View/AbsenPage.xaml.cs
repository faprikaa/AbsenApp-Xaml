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
        }

        private void RefreshDataGrid()
        {
            dgAbsen.ItemsSource = null;
            List<dynamic> listDataJadi = new List<dynamic>();
            List<BsonDocument> listDataRaw =  absensiManager.getAbsensiWithAggregate();
            listDataRaw.ForEach(data =>
            {
                listDataJadi.Add(BsonSerializer.Deserialize<dynamic>(data) );
            });


            Debug.WriteLine(listDataJadi.Count);
            dgAbsen.ItemsSource = listDataJadi;
        }
    }
}
