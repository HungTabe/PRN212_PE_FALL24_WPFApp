using Microsoft.IdentityModel.Tokens;
using PerfumeManagement.BLL.Services;
using PerfumeManagement.DAL.Entities;
using PerfumeManagement.DAL.Repositories;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PerfumeManagement_QE123456
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PerfumeInforService _service = new();

        // Delate Temp data for Role Authen
        public Psaccount Account { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Fill DataTable
            FillDataTable();
            // Authen User Role
            WelcomeLabel.Content = "WELCOME, " + Account.PsaccountNote;
            if (Account.Role != 2)
            {
                DisbaledButton();
            }
        }

        // Disable CUD function for Staff role
        private void DisbaledButton()
        {
            SearchButton.IsEnabled = false;
            CreateButton.IsEnabled = false;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }


        private void FillDataTable()
        {
            // Delete previous data
            PerfumeDataGrid.ItemsSource = null;
            // Call service to fill new data
            PerfumeDataGrid.ItemsSource = _service.GetAllPerfumes();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get data Search terms from user
            string ingre = IngredientsTxt.Text;
            string concen = ConcentrationTxt.Text;

            // Case 4 : both null them GUI will show MessageBox need to fill search terms
            if (ingre.IsNullOrEmpty() && concen.IsNullOrEmpty())
            {
                // Show message
                MessageBox.Show("You have to fill search terms to search product!");
                return;
            }


            // Give Search data to BLL to handle Search use case
            var list = _service.GetProductBySearchTerms(ingre, concen);

            if (!list.IsNullOrEmpty())
            {
                // fill data to table
                // Delete previous data
                PerfumeDataGrid.ItemsSource = null;
                // Fill new data to table
                PerfumeDataGrid.ItemsSource = list;

            } else
            {
                // Show message
                MessageBox.Show("There is not data according to your search terms in databse");
                return;
            }

        }
    }
}