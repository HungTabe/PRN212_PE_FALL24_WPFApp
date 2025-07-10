using PerfumeManagement.BLL.Services;
using PerfumeManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {

        // Dependencies
        private readonly PerfumeInforService _perfumeService = new();
        private readonly ProductionComService _companyService = new();

        // Var to hold data from MainWindow to DetailWindow
        private readonly PerfumeInformation _perfume;

        // Flag1 : to know update or add new mode
        private readonly bool _isUpdate;

        // Flag2 : to help MainWindow know when need to reload data grid
        public bool IsSaved { get; set; } = false;


        public DetailWindow(PerfumeInformation? perfume = null)
        {
            InitializeComponent();
            // flag to know update or add new
            // true when data selected item is passed in => update mode ON
            // false when no selected item is passed in => add new mode ON
            _isUpdate = perfume != null;
            // When perfume var have value pass in - _perfume hold data from perfume var
            // When perfume var null = _perfume in no data mode on, ONLY have constructor
            _perfume = perfume ?? new PerfumeInformation();
            Loaded += DetailWindow_Loaded;
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate fields
            if (!ValidateInputs()) return;


            // Fill data from window to object _perfume - call sevice update object _perfume (update case)
            if (_isUpdate)
            {
                _perfume.PerfumeId = PerfumeIdTextBox.Text.Trim();
                _perfume.PerfumeName = PerfumeNameTextBox.Text.Trim();
                _perfume.Ingredients = IngredientsTextBox.Text.Trim();
                _perfume.ReleaseDate = ReleaseDatePicker.SelectedDate;
                _perfume.Concentration = ConcentrationTextBox.Text.Trim();
                _perfume.Longevity = LongevityTextBox.Text.Trim();
                _perfume.Quantity = int.Parse(QuantityTextBox.Text.Trim());
                _perfume.Price = decimal.Parse(PriceTextBox.Text.Trim());
                _perfume.ProductionCompanyId = ProductionCompanyComboBox.SelectedValue?.ToString();
            }

            // call service add object _perfumr (add case)
            if (_isUpdate)
            {
                _perfumeService.UpdatePerfume(_perfume);
            } else
            {
                _perfume.PerfumeId = PerfumeIdTextBox.Text.Trim();
                _perfume.PerfumeName = PerfumeNameTextBox.Text.Trim();
                _perfume.Ingredients = IngredientsTextBox.Text.Trim();
                _perfume.ReleaseDate = ReleaseDatePicker.SelectedDate;
                _perfume.Concentration = ConcentrationTextBox.Text.Trim();
                _perfume.Longevity = LongevityTextBox.Text.Trim();
                _perfume.Quantity = int.Parse(QuantityTextBox.Text.Trim());
                _perfume.Price = decimal.Parse(PriceTextBox.Text.Trim());
                _perfume.ProductionCompanyId = ProductionCompanyComboBox.SelectedValue?.ToString();
                _perfumeService.AddPerfume(_perfume);
            }

            // Update flag
            IsSaved = true;
            // Return dialog
            this.DialogResult = true;
            this.Close();

        }

        private bool ValidateInputs()
        {
            // Check required
            if ( // PerfumeIdTextBox không required khi tạo mới
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

        private void DetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Load data for combo box Company Name
            var companies = _companyService.GetAllCompanies();
            ProductionCompanyComboBox.ItemsSource = companies;
            ProductionCompanyComboBox.DisplayMemberPath = "ProductionCompanyName";
            ProductionCompanyComboBox.SelectedValuePath = "ProductionCompanyId";


            // Rely on flag1 to Update mode or Add new mode
            // isUpdate TRUE means Update mode
            if (_isUpdate)
            {
                // fill existing item data in to window fields
                PerfumeIdTextBox.Text = _perfume.PerfumeId;
                // In update mode : Only show id not Enable ajust Item Id
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
        }
    }
}
