using absenxaml.Manager;
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

namespace absenxaml.View.Windows
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        UserManager userManager = new UserManager();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String un = tbUsername.Text;
            String pw = pbPassword.Password;
            if (un == "" || pw == "")
            {
                Utils.ShowMBWarning("Harap lengkapi input diatas !");
            } else
            {
                var loginRes = userManager.LoginAttempt(un, pw);
                if (loginRes == "unknown")
                {
                    Utils.ShowMBWarning("Username atau password salah");

                }
                else if (loginRes == "dosen")
                {
                    Utils.ShowMBInfo("Berhasil login");
                    new DosenWindow().Show();
                    this.Close();
                }
            }

        }
    }
}
