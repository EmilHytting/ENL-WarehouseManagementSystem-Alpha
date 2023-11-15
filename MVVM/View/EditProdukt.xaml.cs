using ENL___WarehouseManagementSystem.Data.DataModels;
using ENL___WarehouseManagementSystem.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    public partial class EditProdukt : Window
    {
        private DAL dbContext;
        private List<Produkt> productList;
        private int currentIndex;
        private DateTime produktDato;

        public event EventHandler DataSaved;

        public EditProdukt()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"); // Erstat med din forbindelsesstreng

            productList = dbContext.GetProducts().ToList();
            currentIndex = 0;

            DisplayProduct(currentIndex);

            produktDato = DateTime.Now;
            TbxDato.Text = produktDato.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public int ProduktId { get; set; }

        private void BtnSaveProdukt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int prodId = int.Parse(TbxProdId.Text);
                string prodNavn = TbxProdNavn.Text;
                int prodAntal = int.Parse(TbxProdAntal.Text);
                string prodBeskrivelse = TbxProdDesc.Text;
                string prodPlacering = TbxProdPlacering.Text;

                Produkt selectedProduct = dbContext.GetProductById(prodId);

                if (selectedProduct != null)
                {
                    selectedProduct.ProdNavn = prodNavn;
                    selectedProduct.ProdAntal = prodAntal;
                    selectedProduct.ProdBeskrivelse = prodBeskrivelse;
                    selectedProduct.ProdPlacering = prodPlacering;

                    selectedProduct.Oprettelse = produktDato;

                    dbContext.UpdateProduct(selectedProduct);

                    this.Close();

                    MessageBox.Show("Produktet blev gemt med succes.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    DataSaved?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Produktet blev ikke fundet i databasen.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved gemning af produkt: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelProdukt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnNæsteProdukt_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            if (currentIndex >= productList.Count)
            {
                currentIndex = 0;
            }
            DisplayProduct(currentIndex);
        }

        private void BtnTilbageProdukt_Click(object sender, RoutedEventArgs e)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = productList.Count - 1;
            }
            DisplayProduct(currentIndex);
        }

        private void DisplayProduct(int index)
        {
            if (index >= 0 && index < productList.Count)
            {
                Produkt product = productList[index];
                TbxProdId.Text = product.ProdID.ToString();
                TbxProdNavn.Text = product.ProdNavn;
                TbxProdAntal.Text = product.ProdAntal.ToString();
                TbxProdDesc.Text = product.ProdBeskrivelse;
                TbxProdPlacering.Text = product.ProdPlacering;

                // Opdater TbxProdDato med datoen fra produktet
                produktDato = product.Oprettelse;
                TbxDato.Text = produktDato.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
