using AbsenMVC.Model;
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

namespace absenxaml.View
{
    /// <summary>
    /// Interaction logic for AddMatkul.xaml
    /// </summary>
    public partial class AddMatkul : Window
    {
        private MatkulManager matkulManager;
        private DataMatkul dataMatkulForm;
        public AddMatkul(MatkulManager manager, DataMatkul dataMatkul)
        {
            InitializeComponent();
            matkulManager = manager;
            this.dataMatkulForm = dataMatkul;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            var namaMk = tbMatkul.Text;
            Matkul mhsMatkul = new Matkul(namaMk);
            if (matkulManager.InsertNewMatkul(mhsMatkul))
            {
                MessageBox.Show("Berhasil menambah data matkul!", "Sukses",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                dataMatkulForm.refreshDataGrid();

            }
            else
            {
                MessageBox.Show("Gagal menambah data matkul!", "Gagal",
                    MessageBoxButton.OKCancel, MessageBoxImage.Stop);
            }

        }
    }
}
