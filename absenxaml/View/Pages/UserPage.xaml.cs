using AbsenMVC.Model;
using absenxaml.Manager;
using absenxaml.View.Windows;
using MongoDB.Driver;
using System;
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

namespace absenxaml.View.Pages
{
    /// <summary>
    /// Interaction logic for DataUser.xaml
    /// </summary>
    public partial class DataUser : Page
    {
        private UserManager userManager;
        private bool isBtnAdd = true;
        private bool isBtnEdit = true;
        public DataUser()
        {
            InitializeComponent();
            userManager = new UserManager();
            refreshDataGrid();
        }

        public void refreshDataGrid()
        {
            dgUser.ItemsSource = null;
            dgUser.ItemsSource = userManager.getUser().AsQueryable().ToList<User>();
            cbRole.ItemsSource = new List<String> { "admin", "mahasiswa", "dosen" };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            String nama = tbNama.Text;
            String role = cbRole.SelectedItem as String;
            if(nama == String.Empty || cbRole.SelectedItem == null)
            {
                MessageBox.Show("Harap lengkapi input diatas", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            User newUser = new User(nama, nama, nama, role);
            userManager.InsertNewUser(newUser);
            MessageBox.Show("Sukses menambah user baru !", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            clear();
            refreshDataGrid();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgUser.SelectedItem as User;
            if (selectedItem == null)
            {
                MessageBox.Show("Pilih salah satu user", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            String newName = tbNama.Text;
            String newRole = cbRole.SelectedItem as String;
            var filter = Builders<User>.Filter.Eq(u => u.Id, selectedItem.Id);
            var query = Builders<User>.Update.Set(u => u.Nama, newName).Set(u => u.Role, newRole);
            MessageBox.Show("Berhasil update data user", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            userManager.UpdateUser(filter, query);
            refreshDataGrid();
            clear();
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dgUser.SelectedItem as User;
            if (selectedItem == null)
            {
                return;
            }
            tbNama.Text = selectedItem.Nama;
            cbRole.SelectedItem = selectedItem.Role;
        }

        private void clear()
        {
            tbNama.Text = string.Empty;
            cbRole.SelectedItem = null;
            dgUser.SelectedItem = null;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgUser.SelectedItem as User;
            if (selectedItem == null)
            {
                MessageBox.Show("Pilih salah satu user", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var d = MessageBox.Show("hapus user " + selectedItem.Nama + " ?", "Konfirmasi", MessageBoxButton.OKCancel);
            if (d == MessageBoxResult.OK)
            {
                userManager.DeleteUser(selectedItem.Id);
                clear();
                refreshDataGrid();
                MessageBox.Show("Berhasil hapus data user", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSeeMatkul_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgUser.SelectedItem as User;
            var wind = new DataMatkulUserWindow(selectedItem.Id);
            wind.Show();
        }
    }
}
