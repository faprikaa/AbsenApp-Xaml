using AbsenMVC.Model;
using absenxaml.Manager;
using MongoDB.Bson;
using MongoDB.Driver;
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

namespace absenxaml.View.Windows
{
    /// <summary>
    /// Interaction logic for UnPwWindow.xaml
    /// </summary>
    public partial class UnPwWindow : Window
    {
        private UserManager userManager = new UserManager();
        private User user;
        public UnPwWindow(ObjectId userId)
        {
            InitializeComponent();
            user = userManager.GetUserById(userId); 
            tbPassword.Text = user.Password;
            tbUsername.Text = user.Username;    
        }

        private void btnSimpan_Click(object sender, RoutedEventArgs e)
        {
            var newUn = tbUsername.Text;
            var newPw = tbPassword.Text;
            var filtert = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            var query = Builders<User>.Update.Set(u => u.Username, newUn)
                .Set(u => u.Password, newPw);
            userManager.UpdateUser(filtert, query);
            Utils.ShowMBInfo("Berhasil update data user !");
            
        }
    }
}
