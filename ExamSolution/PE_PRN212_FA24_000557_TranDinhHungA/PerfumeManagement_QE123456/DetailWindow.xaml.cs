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
using PerfumeManagement.BLL.Services;
using PerfumeManagement.DAL.Entities;
using System.Text.RegularExpressions;

namespace PerfumeManagement_QE123456
{
    public partial class DetailWindow : Window
    {

        // Service need for add/update (Dependency Inject)
        private readonly PerfumeInforService _perfumeService = new();
        private readonly ProductionComService _companyService = new();
        
        // Var to hold data tranfer from Main to Detail
        private readonly PerfumeInformation _perfume;

        // Flag 
        private readonly bool _isUpdate;
        public bool IsSaved { get; set; } = false;

        public DetailWindow(PerfumeInformation? perfume = null)
        {
            InitializeComponent();
            _isUpdate = perfume != null;
            _perfume = perfume ?? new PerfumeInformation();
            Loaded += DetailWindow_Loaded;
        }

        private void DetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Load companies to ComboBox
            var companies = _companyService.GetAllCompanies();
            ProductionCompanyComboBox.ItemsSource = companies;
            ProductionCompanyComboBox.DisplayMemberPath = "ProductionCompanyName";
            ProductionCompanyComboBox.SelectedValuePath = "ProductionCompanyId";

            // Update case
            if (_isUpdate)
            {
                // Fill data for update
                PerfumeIdTextBox.Text = _perfume.PerfumeId;
                PerfumeIdTextBox.IsEnabled = false;
                PerfumeNameTextBox.Text = _perfume.PerfumeName;
                IngredientsTextBox.Text = _perfume.Ingredients;
                ReleaseDatePicker.SelectedDate = _perfume.ReleaseDate;
                ConcentrationTextBox.Text = _perfume.Concentration;
                LongevityTextBox.Text = _perfume.Longevity;
                QuantityTextBox.Text = _perfume.Quantity?.ToString();
                PriceTextBox.Text = _perfume.Price?.ToString();
                ProductionCompanyComboBox.SelectedValue = _perfume.ProductionCompanyId;
            }
            // Add new case
            else
            {
                // Id auto increase so no need to fill id field
                PerfumeIdTextBox.Text = "(Auto)";
                PerfumeIdTextBox.IsEnabled = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate all fields
            if (!ValidateInputs()) return;

            // Fill data to perfume fields if update mode
            if (_isUpdate)
            _perfume.PerfumeId = PerfumeIdTextBox.Text.Trim();
            _perfume.PerfumeName = PerfumeNameTextBox.Text.Trim();
            _perfume.Ingredients = IngredientsTextBox.Text.Trim();
            _perfume.ReleaseDate = ReleaseDatePicker.SelectedDate;
            _perfume.Concentration = ConcentrationTextBox.Text.Trim();
            _perfume.Longevity = LongevityTextBox.Text.Trim();
            _perfume.Quantity = int.Parse(QuantityTextBox.Text.Trim());
            _perfume.Price = decimal.Parse(PriceTextBox.Text.Trim());
            _perfume.ProductionCompanyId = ProductionCompanyComboBox.SelectedValue?.ToString();

            // Update case
            if (_isUpdate)
                _perfumeService.UpdatePerfume(_perfume);
            // Add case
            else
                _perfumeService.AddPerfume(_perfume);

            // Update flag to MainWindow know to reload data
            IsSaved = true;
            this.DialogResult = true;
            this.Close();
        }

        private bool ValidateInputs()
        {
            // Check required
            if (
                // PerfumeIdTextBox không required khi tạo mới
                string.IsNullOrWhiteSpace(PerfumeNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(IngredientsTextBox.Text) ||
                !ReleaseDatePicker.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(ConcentrationTextBox.Text) ||
                string.IsNullOrWhiteSpace(LongevityTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                ProductionCompanyComboBox.SelectedValue == null)
            {
                MessageBox.Show("All fields are required.");
                return false;
            }
            // PerfumeName: 5-90 ký tự, mỗi từ viết hoa hoặc số, không ký tự đặc biệt $,%...^
            string name = PerfumeNameTextBox.Text.Trim();
            if (name.Length < 5 || name.Length > 90)
            {
                MessageBox.Show("Perfume Name must be 5-90 characters.");
                return false;
            }
            // Regex: mỗi từ bắt đầu bằng chữ hoa hoặc số, không ký tự đặc biệt $,%...^
            if (!Regex.IsMatch(name, @"^([A-Z0-9][a-zA-Z0-9]*\s?)+$"))
            {
                MessageBox.Show("Each word in Perfume Name must start with a capital letter or digit, and not contain special characters ($,%...^@).");
                return false;
            }
            // Không chứa ký tự đặc biệt $,%...^
            if (Regex.IsMatch(name, "[$%^@]"))
            {
                MessageBox.Show("Perfume Name must not contain special characters ($,%...^@).");
                return false;
            }
            // Quantity, Price là số hợp lệ
            if (!int.TryParse(QuantityTextBox.Text.Trim(), out _))
            {
                MessageBox.Show("Quantity must be a valid integer.");
                return false;
            }
            if (!decimal.TryParse(PriceTextBox.Text.Trim(), out _))
            {
                MessageBox.Show("Price must be a valid decimal number.");
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
