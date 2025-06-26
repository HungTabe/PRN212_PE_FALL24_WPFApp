using PerfumeManagement.BLL.Services;
using PerfumeManagement.DAL.Entities;
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

namespace PerfumeManagement_QE123456
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private PsaccountService _service;
        public LoginWindow()
        {
            InitializeComponent();
            _service = new();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Get data from field name and password to check login
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Call to Service function to handle usecase
            var account = await _service.Login(email, password);
            if (account == null || (account.Role != 2 && account.Role != 3))
            {
                // Show message
                MessageBox.Show("You have no permission to access this function!");
                return;
            }
            MainWindow main = new();
            main.Account = account;
            main.Show();
            // Close login Screen
            this.Close();
        }
    }
}
