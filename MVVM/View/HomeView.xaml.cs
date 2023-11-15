using ENL___WarehouseManagementSystem.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private DAL dbContext;

        public HomeView()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"); //Forbindelsesstreng 

            // Hent antallet af medarbejdere fra databasen
            int antalMedarbejdere = dbContext.GetAllEmployees().Count;

            // Opdater TextBlock-teksten
            UserCountText.Text = antalMedarbejdere.ToString();


            // Hent antallet af medarbejdere fra databasen
            int antalProdukter = dbContext.GetProducts().Count;

            // Opdater TextBlock-teksten
            ProduktCountTextBlock.Text = antalProdukter.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://github.com/EmilHytting/ENL-Distribution-A-S-WarehouseManagementSystem";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Håndter fejl ved at åbne webstedet
            }
        }
    }
}