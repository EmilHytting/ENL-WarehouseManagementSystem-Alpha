using ENL___WarehouseManagementSystem.Data;
using ENL___WarehouseManagementSystem.Data.DataModels;
using System;
using System.Windows;

namespace ENL___WarehouseManagementSystem
{
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        private DAL dbContext;

        public event Action<EditEmployee> NextEmployee;
        public event Action<EditEmployee> PrevEmployee;
        public event Action<Employee> EmployeeUpdated;

        public EditEmployee()
        {
            InitializeComponent();
            dbContext = new DAL("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;");
        }

        public Employee Employee { get; set; }

        public void ShowEmployee(Employee emp)
        {
            Employee = emp;
            TbxEmpId.Text = $"{Employee.EmpID}";
            TbxName.Text = $"{Employee.EmpName}";
            TbxLastName.Text = $"{Employee.LastName}";
            TbxEmail.Text = $"{Employee.EmpEmail}";
            TbxPhone.Text = $"{Employee.EmpPhone}";
            TbxStilling.Text = $"{Employee.EmploymentStatus}";
            Show();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            PrevEmployee.Invoke(this);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NextEmployee.Invoke(this);
        }

        private void BtnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Employee.EmpID != 0)
                {
                    Employee.EmpName = TbxName.Text;
                    Employee.LastName = TbxLastName.Text;
                    Employee.EmpEmail = TbxEmail.Text;
                    Employee.EmpPhone = TbxPhone.Text;
                    Employee.EmploymentStatus = TbxStilling.Text;

                    dbContext.UpdateEmployee(Employee);

                    MessageBox.Show("Medarbejderdata blev opdateret med succes.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Kan ikke opdatere medarbejderen. Vær venlig at tilføje medarbejderen i stedet for.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
