using System.Windows.Controls;
using System.Collections.ObjectModel;
using ENL___WarehouseManagementSystem.Data.DataModels;
using System.Windows;
using System.Collections.Generic;
using ENL___WarehouseManagementSystem.Data;
using System;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    public partial class OrdreView : UserControl
    {
        private DAL dbContext;
        public ObservableCollection<Ordre> OrdreList { get; set; }

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
            OrdreList.Clear(); 
            foreach (var ordre in data)
            {
                OrdreList.Add(ordre); 
            }
            dgOrdre.ItemsSource = OrdreList;
        }

        private void BtnSearchOrdre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(tbxOrdreId.Text, out int ordreId))
                {
                    var ordre = dbContext.GetOrdreById(ordreId);

                    if (ordre != null)
                    {
                        var editOrdreWindow = new EditOrdre(ordre);
                        editOrdreWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Ingen ordre fundet med det angivne ID.");
                    }
                }
                else
                {
                    MessageBox.Show("Ugyldigt ordre-ID. Indtast venligst et heltal.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under søgning efter ordre: {ex.Message}");
            }
        }

        private void btnAddOrdre_Click(object sender, RoutedEventArgs e)
        {
            AddOrdre addOrdreWindow = new AddOrdre();
            addOrdreWindow.UpdateDataHandler += LoadOrdreData;
            addOrdreWindow.ShowDialog();
        }

        private void BtnFjernOrdre_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrdre.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på, at du vil fjerne valgte ordre?", "Bekræft fjernelse", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    List<Ordre> ordreAtFjerne = new List<Ordre>();

                    foreach (var selectedItem in dgOrdre.SelectedItems)
                    {
                        if (selectedItem is Ordre ordre)
                        {
                            ordreAtFjerne.Add(ordre);
                        }
                    }

                    foreach (var ordre in ordreAtFjerne)
                    {
                        OrdreList.Remove(ordre);
                        dbContext.RemoveOrdre(ordre);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vælg mindst én række, der skal fjernes.", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnOpdaterOrdre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var ordreList = dbContext.GetOrdre(); 
                OrdreList.Clear(); 
                foreach (var ordre in ordreList)
                {
                    OrdreList.Add(ordre); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under opdatering af ordre: {ex.Message}");
            }
        }
    }
}
