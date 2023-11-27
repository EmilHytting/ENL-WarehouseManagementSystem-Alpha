using ENL___WarehouseManagementSystem.Data;
using ENL___WarehouseManagementSystem.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddOrdre.xaml
    /// </summary>
    public partial class AddOrdre : Window
    {

        private ObservableCollection<Ordre> OrdreList;
        private DAL dbContext;

        
        public delegate void UpdateDataDelegate();
        public UpdateDataDelegate UpdateDataHandler;

        public AddOrdre()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;");
            LoadProductData();
            LoadEmployeeData();
        }

        private void LoadProductData()
        {
            var products = dbContext.GetProducts();
            cmbProducts.ItemsSource = products;
        }

        private void LoadEmployeeData()
        {
            var employees = dbContext.GetAllEmployees();
            cmbEmployees.ItemsSource = employees;
        }

        private void BtnCancelOrdre_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSaveOrdre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProduct = cmbProducts.SelectedItem as Produkt;
                var antal = int.Parse(txtAntal.Text);
                var status = (cbxStatus.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Oprettet";
                var selectedEmployee = cmbEmployees.SelectedItem as Employee;

                var newOrdre = new Ordre
                {
                    ProdNavn = selectedProduct.ProdNavn,
                    OrdreAntal = antal,
                    OrdreStatus = status,
                    EmpName = selectedEmployee?.EmpName,
                    CreatedDate = DateTime.Now
                };

                
                dbContext.AddOrdre(newOrdre);

                
                UpdateDataHandler?.Invoke();

                
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under gemning af ordre: {ex.Message}");
            }
        }

        private void ClearInputFields()
        {
            
            cmbProducts.SelectedItem = null;
            txtAntal.Text = string.Empty;
            cbxStatus.SelectedItem = null;
            cmbEmployees.SelectedItem = null; 
        }

    }
}
