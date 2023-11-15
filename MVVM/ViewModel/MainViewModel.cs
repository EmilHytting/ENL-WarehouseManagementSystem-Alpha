using ENL___WarehouseManagementSystem.Core;
using ENL___WarehouseManagementSystem.MVVM.View;

namespace ENL___WarehouseManagementSystem.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        // Relay Commands

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProduktViewCommand { get; set; }
        public RelayCommand MedarbejderViewCommand { get; set; }
        public RelayCommand OrdreViewCommand { get; set; }




        public HomeViewModel HomeVM { get; set; }

        public ProduktViewModel ProduktVM { get; set; }

        public MedarbejderViewModel MedarbejderVM { get; set; }

        public OrdreViewModel OrdreVM { get; set; }



        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            ProduktVM = new ProduktViewModel();     
            MedarbejderVM = new MedarbejderViewModel();
            OrdreVM = new OrdreViewModel();

            CurrentView = HomeVM;


            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ProduktViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProduktVM;
            });

            MedarbejderViewCommand = new RelayCommand(o =>
            {
                CurrentView = MedarbejderVM;
            });

            OrdreViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrdreVM;
            });
        }
    }
}
