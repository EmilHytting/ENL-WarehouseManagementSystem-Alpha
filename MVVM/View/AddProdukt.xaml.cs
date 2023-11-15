using System;
using System.Windows;
using ENL___WarehouseManagementSystem.Data;
using ENL___WarehouseManagementSystem.Data.DataModels;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    public partial class AddProdukt : Window
    {
        private DAL dbContext;

        public AddProdukt()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"); 
        }

        private void BtnCancelProdukt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSaveProdukt_Click(object sender, RoutedEventArgs e)
        {
            string productName = TbxProdNavn.Text;
            string productDescription = TbxProdDesc.Text;
            int productQuantity;

            if (int.TryParse(TbxProdAntal.Text, out productQuantity))
            {
                int placering;

                if (int.TryParse(TbxProdPlacering.Text, out placering) && placering >= 0)
                {
                    int række = placering / 10; // Del ved 10 for at få række
                    int hylde = placering % 10; // Tag resten for at få hylde

                    // Opret format for placering (f.eks. 1.2 eller 1.1)
                    string formateretPlacering = $"{række}.{hylde}";

                    try
                    {
                        // Opret en ny produktinstans
                        Produkt newProduct = new Produkt
                        {
                            ProdNavn = productName,
                            ProdBeskrivelse = productDescription,
                            ProdAntal = productQuantity,
                            ProdPlacering = formateretPlacering,
                            Oprettelse = DateTime.Now // Gem placeringen som en streng
                        };

                        // Tilføj produktet til databasen
                        dbContext.AddProduct(newProduct);
                        MessageBox.Show("Produktet blev tilføjet med succes.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        TbxProdNavn.Text = "";
                        TbxProdDesc.Text = "";
                        TbxProdAntal.Text = "";
                        TbxProdPlacering.Text = "";
                        TbxProdDato.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fejl ved tilføjelse af produkt: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Række skal være et positivt heltal.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Antal skal være et gyldigt heltal.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
