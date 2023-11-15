using ENL___WarehouseManagementSystem.Data;
using ENL___WarehouseManagementSystem.Data.DataModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ENL___WarehouseManagementSystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for MedarbejderView.xaml
    /// </summary>
    public partial class MedarbejderView : UserControl
    {
        private List<Employee> employees = new List<Employee>();
        private DAL dbContext;
        private EditEmployee editEmployeeWindow;

        public MedarbejderView()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"); // forbindelsesstreng 
            LoadEmployeesFromDatabase();
        }

        private void LoadEmployeesFromDatabase()
        {
            var employeesFromDatabase = dbContext.GetAllEmployees();
            foreach (var employee in employeesFromDatabase)
            {
                employees.Add(employee);
            }

            dgEmps.ItemsSource = employees;
        }

        public void BtnGetEmployees_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        public void BtnSearchEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbxEmpId.Text, out int searchId) || !string.IsNullOrEmpty(TbxEmpName.Text))
            {
                List<Employee> searchResults = dbContext.SearchEmployee(searchId, TbxEmpName.Text);

                dgEmps.ItemsSource = searchResults;

                if (searchResults.Count == 1)
                {
                    
                    Employee selectedEmployee = searchResults[0];
                    OpenEditEmployeeWindow(selectedEmployee);
                }
            }
            else
            {
                MessageBox.Show("Indtast venligst en gyldig medarbejder-ID eller navn.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenEditEmployeeWindow(Employee employee)
        {
            EditEmployee editEmployeeWindow = new EditEmployee();
            editEmployeeWindow.ShowEmployee(employee);


            editEmployeeWindow.NextEmployee += EditEmp_NextEmployee;
            editEmployeeWindow.PrevEmployee += EditEmp_PrevEmployee;


            editEmployeeWindow.Show();
        }

        void GetEmployees()
        {
            employees = dbContext.GetAllEmployees();
            dgEmps.ItemsSource = employees;
            LblEmployees.Content = $"Employee(s): {employees.Count}";
        }

        private void dgEmps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddEmployees_Click(object sender, RoutedEventArgs e)
        {
            
            AddEmployee addEmployeeWindow = new AddEmployee();

            bool? result = addEmployeeWindow.ShowDialog();

            if (result == true)
            {
                
                Employee newEmployee = new Employee
                {
                    EmpID = GenerateUniqueEmployeeId(),
                    EmpName = addEmployeeWindow.TbxName.Text,
                    
                };

                dbContext.AddEmployee(newEmployee);

                GetEmployees();
            }
        }


        private int GenerateUniqueEmployeeId()
        {
        
            return -1;
        }

        private void BtnFjernEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmps.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på, at du vil fjerne valgte medarbejdere?", "Bekræft fjernelse", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    List<Employee> employeesToRemove = new List<Employee>();

                    foreach (var selectedItem in dgEmps.SelectedItems)
                    {
                        if (selectedItem is Employee employee)
                        {
                            employeesToRemove.Add(employee);
                        }
                    }

                    foreach (var employee in employeesToRemove)
                    {
                        employees.Remove(employee);
                        dbContext.RemoveEmployee(employee);
                    }
                    dbContext.GetAllEmployees();
                }
            }
            else
            {
                MessageBox.Show("Vælg mindst én række, der skal fjernes.", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditEmp_PrevEmployee(EditEmployee editEmp)
        {
           
        }

        private void EditEmp_NextEmployee(EditEmployee editEmp)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private void dgEmps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgEmps.SelectedItem is Employee selectedEmployee)
            {
                if (editEmployeeWindow == null || !editEmployeeWindow.IsLoaded)
                {
                    editEmployeeWindow = new EditEmployee();
                    editEmployeeWindow.EmployeeUpdated += (editedEmployee) =>
                    {
                        GetEmployees();
                    };
                }

                editEmployeeWindow.ShowEmployee(selectedEmployee);
                editEmployeeWindow.ShowDialog();
            }
        }
    }
}
