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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using ENL___WarehouseManagementSystem.Data;
using MaterialDesignThemes.Wpf;

namespace ENL___WarehouseManagementSystem
{

    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            DAL dal = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;");

            if (dal.ValidateLogin(username, password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Hide();  
            }
            else
            {
                MessageBox.Show("Forkerte loginoplysninger. Prøv igen.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void signupBtnOpen_Click(object sender, RoutedEventArgs e)
        {

            CreateLogin createLoginWindow = new CreateLogin();
            
            Hide();

            createLoginWindow.ShowDialog();

            Show();
        }
    }
}
