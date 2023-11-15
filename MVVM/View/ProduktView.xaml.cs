using ENL___WarehouseManagementSystem.Data;
using ENL___WarehouseManagementSystem.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    public partial class ProduktView : UserControl
    {
        private DAL dbContext;
        private ObservableCollection<Produkt> Produkter;
        private EditProdukt editWindow;

        public ProduktView()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;");
            Produkter = new ObservableCollection<Produkt>();
            LoadProdukterFromDatabase();

            editWindow = new EditProdukt();
            editWindow.DataSaved += (sender, args) => GetProductData();
        }

        private void LoadProdukterFromDatabase()
        {
            var produkterFraDatabase = dbContext.GetProducts();
            foreach (var produkt in produkterFraDatabase)
            {
                Produkter.Add(produkt);
            }

            dgProdukt.ItemsSource = Produkter;
        }

        public void GetProductData()
        {
            try
            {
                var products = dbContext.GetProducts();

                Produkter.Clear();
                foreach (var produkt in products)
                {
                    Produkter.Add(produkt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indhentning af data: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpdater_Click(object sender, RoutedEventArgs e)
        {
            GetProductData();
        }

        private void btnTilføj_Click(object sender, RoutedEventArgs e)
        {
            AddProdukt addProduktWindow = new AddProdukt();
            addProduktWindow.ShowDialog();
        }

        private void BtnFjern_Click(object sender, RoutedEventArgs e)
        {
            if (dgProdukt.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på, at du vil fjerne valgte produkter?", "Bekræft fjernelse", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    List<Produkt> produkterAtFjerne = new List<Produkt>();

                    foreach (var selectedItem in dgProdukt.SelectedItems)
                    {
                        if (selectedItem is Produkt produkt)
                        {
                            produkterAtFjerne.Add(produkt);
                        }
                    }

                    foreach (var produkt in produkterAtFjerne)
                    {
                        Produkter.Remove(produkt);
                        dbContext.RemoveProduct(produkt);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vælg mindst én række, der skal fjernes.", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSearchProdukt_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbxSearchProduct.Text, out int selectedProductId))
            {
                Produkt selectedProduct = dbContext.GetProductById(selectedProductId);

                if (selectedProduct != null)
                {
                    editWindow.TbxProdId.Text = selectedProduct.ProdID.ToString();
                    editWindow.TbxProdNavn.Text = selectedProduct.ProdNavn;
                    editWindow.TbxProdAntal.Text = selectedProduct.ProdAntal.ToString();
                    editWindow.TbxProdDesc.Text = selectedProduct.ProdBeskrivelse;
                    editWindow.TbxProdPlacering.Text = selectedProduct.ProdPlacering;
                    editWindow.TbxDato.Text = selectedProduct.Oprettelse.ToString();

                    editWindow.TbxProdId.IsReadOnly = true;

                    editWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Produktet blev ikke fundet i databasen.");
                }
            }
            else
            {
                MessageBox.Show("Indtast venligst en gyldig produkt-ID.");
            }
        }
    }
}
