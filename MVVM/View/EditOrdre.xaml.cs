using ENL___WarehouseManagementSystem.Data.DataModels;
using ENL___WarehouseManagementSystem.Data;
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
using System.Collections.ObjectModel;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for EditOrdre.xaml
    /// </summary>
    public partial class EditOrdre : Window
    {
        private DAL dbContext;
        private Ordre ordreToEdit;

        public ObservableCollection<Ordre> OrdreList { get; set; }
        public ObservableCollection<Produkt> ProductList { get; set; }

        public EditOrdre(Ordre ordre)
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;");
            ProductList = new ObservableCollection<Produkt>();
            ordreToEdit = ordre;
            Window_Loaded(null, null);
            SetOrdreData(ordre);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            LoadProductData();
            LoadEmployeeData();

            cmbProducts.SelectedItem = dbContext.GetProductByName(ordreToEdit.ProdNavn);
            cbxStatus.SelectedItem = ordreToEdit.OrdreStatus;
            txtAntal.Text = ordreToEdit.OrdreAntal.ToString();
            cmbEmployees.SelectedItem = dbContext.GetEmployeeByName(ordreToEdit.EmpName);
        }

        private void LoadProductData() // Indlæser ordre der er lavet.
        {
            ProductList.Clear();
            var products = dbContext.GetProducts();
            foreach (var product in products)
            {
                ProductList.Add(product);
            }
            cmbProducts.ItemsSource = ProductList;
        }

        private void LoadEmployeeData()
        {
            var employees = dbContext.GetAllEmployees();
            cmbEmployees.ItemsSource = employees;
        }

        private void BtnSaveOrdre1_Click(object sender, RoutedEventArgs e) // Gemmer Ordre.
        {
            try
            {
                var selectedProduct = cmbProducts.SelectedItem as Produkt;
                var antal = int.Parse(txtAntal.Text);
                var status = cbxStatus.Text;
                var selectedEmployee = cmbEmployees.SelectedItem as Employee;

                ordreToEdit.ProdNavn = selectedProduct?.ProdNavn;
                ordreToEdit.OrdreAntal = antal;
                ordreToEdit.OrdreStatus = status;

                if (selectedEmployee != null)
                {
                    ordreToEdit.EmpName = selectedEmployee.EmpName;
                }

                dbContext.UpdateOrdre(ordreToEdit);

                if (OrdreList != null)
                {
                    OrdreList.Clear();

                    var updatedOrdreList = dbContext.GetOrdre();

                    if (updatedOrdreList != null)
                    {
                        foreach (var ordre in updatedOrdreList)
                        {
                            OrdreList.Add(ordre);
                        }
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under gemning af ordre: {ex.Message}");
            }
        }

        private void SetOrdreData(Ordre ordre)
        {
            cmbProducts.SelectedItem = dbContext.GetProductByName(ordre.ProdNavn);
            txtAntal.Text = ordre.OrdreAntal.ToString();
            cbxStatus.SelectedItem = cbxStatus.Items.Cast<object>().FirstOrDefault(item =>
            (item as ComboBoxItem)?.Content.ToString() == ordre.OrdreStatus);
            SetEmployeeData(ordre.EmpName);
        }

        private void SetEmployeeData(string empName)
        {
            var selectedEmployee = dbContext.GetEmployeeByName(empName);
            cmbEmployees.SelectedItem = selectedEmployee;
        }

        private void BtnCancelOrdre1_Click(object sender, RoutedEventArgs e) // Lukker Edit Vinduet.
        {
            Close();
        }
    }
}
