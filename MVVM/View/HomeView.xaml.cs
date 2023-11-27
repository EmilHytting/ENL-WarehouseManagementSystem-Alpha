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
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"); 

            
            int antalMedarbejdere = dbContext.GetAllEmployees().Count;

            
            UserCountText.Text = antalMedarbejdere.ToString();


            
            int antalProdukter = dbContext.GetProducts().Count;

            
            ProduktCountTextBlock.Text = antalProdukter.ToString();

            
            int antalOrdre = dbContext.GetOrdre().Count;

            
            OrdreCountTextBlock.Text = antalOrdre.ToString();
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