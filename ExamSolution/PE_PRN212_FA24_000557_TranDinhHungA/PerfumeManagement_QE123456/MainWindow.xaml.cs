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

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            // Show Login Screen
            LoginWindow login = new();
            login.Show();
            // Close Main Screen
            this.Close();
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredients = IngredientsTextBox.Text.IsNullOrEmpty() ? "empty" : IngredientsTextBox.Text;
            string concentration = ConcentrationTextBox.Text.IsNullOrEmpty() ? "empty" : ConcentrationTextBox.Text ;

            var list = _service.GetPerfumesByConditions(ingredients, concentration);

            if (!list.IsNullOrEmpty())
            {
                // Delete previous data
                PerfumeDataGrid.ItemsSource = null;
                PerfumeDataGrid.ItemsSource = list;
            }
            else
            {
                MessageBox.Show($"Not found any product with ingredients: {ingredients} and concentration: {concentration}");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var perfume = PerfumeDataGrid.SelectedItem as PerfumeInformation;
            if (perfume == null)
            {
                MessageBox.Show("Please select item!", "Warning", MessageBoxButton.OK);
                return;
            }
            var result = MessageBox.Show("Do you want to delete this perfume?", "Confirm deletion", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool checkDelete = _service.DeletePerfume(perfume);
                if (checkDelete)
                {
                    FillDataTable();
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Because DetailWindow() have InitializeComponent(); so when write this
            // is will initialize window detail
            var detailWindow = new DetailWindow();
            // MainWindow At BackGround still check If 
            if (detailWindow.ShowDialog() == true && detailWindow.IsSaved)
            {
                // Reload data for MainWindow
                FillDataTable();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Check objet select if not Inform user please select to update
            if (PerfumeDataGrid.SelectedItem is PerfumeInformation selectedPerfume)
            {
                // Run detailWindow constructor
                var detailWindow = new DetailWindow(selectedPerfume);
                // MainWindow At BackGround still check If 
                if (detailWindow.ShowDialog() == true && detailWindow.IsSaved)
                {
                    FillDataTable();
                }
            }
            else
            {
                MessageBox.Show("Please select a perfume to update.");
            }
        }
    }
}