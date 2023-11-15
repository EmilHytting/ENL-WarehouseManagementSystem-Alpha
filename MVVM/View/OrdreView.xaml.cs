using System.Windows.Controls;
using System.Collections.ObjectModel;
using ENL___WarehouseManagementSystem.Data.DataModels;
using System.Windows;
using System.Collections.Generic;
using ENL___WarehouseManagementSystem.Data;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    public partial class OrdreView : UserControl
    {
        private List<Ordre> employees = new List<Ordre>();
        private DAL dbContext; 
        private ObservableCollection<Ordre> OrdreList;

        public OrdreView()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"); 
            OrdreList = new ObservableCollection<Ordre>();
            LoadOrdreData();
        }

        private void LoadOrdreData()
        {
            var ordreData = dbContext.GetOrdre(); 
            UpdateOrdreList(ordreData);
        }

        private void UpdateOrdreList(List<Ordre> data)
        {
            foreach (var ordre in data)
            {
                OrdreList.Add(ordre);
            }
            dgOrdre.ItemsSource = OrdreList; 
        }

        private void BtnSearchOrdre_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAddOrdre_Click(object sender, RoutedEventArgs e)
        {
            AddOrdre addOrdreWindow = new AddOrdre();
            addOrdreWindow.UpdateDataHandler += LoadOrdreData;
            addOrdreWindow.ShowDialog();
        }
    }
}
