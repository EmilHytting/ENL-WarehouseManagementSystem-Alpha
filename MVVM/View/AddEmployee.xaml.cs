using ENL___WarehouseManagementSystem.Data;
using ENL___WarehouseManagementSystem.Data.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace ENL___WarehouseManagementSystem
{
    public partial class AddEmployee : Window
    {
        public event Action<Employee> EmployeeAdded;

        public AddEmployee()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TbxName.Text;
            string lastName = TbxLastName.Text;
            string email = TbxEmail.Text;
            string phone = TbxPhone.Text;
            string employmentStatus = TbxStatus.Text;
            int age;

            if (int.TryParse(TbxAge.Text, out age))
            {
                
                using (SqlConnection connection = new SqlConnection("Server=DESKTOP-S5PO84T;Database=ENL-Distribution-A/S;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"))
                {
                    try
                    {
                        connection.Open();

                        
                        string insertQuery = "INSERT INTO Employees (EmpName, LastName, EmpEmail, EmpPhone, EmploymentStatus, Age) VALUES (@EmpName, @LastName, @EmpEmail, @EmpPhone, @EmploymentStatus, @Age)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@EmpName", firstName);
                            cmd.Parameters.AddWithValue("@LastName", lastName);
                            cmd.Parameters.AddWithValue("@EmpEmail", email);
                            cmd.Parameters.AddWithValue("@EmpPhone", phone);
                            cmd.Parameters.AddWithValue("@EmploymentStatus", employmentStatus);
                            cmd.Parameters.AddWithValue("@Age", age);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Medarbejderen blev tilføjet med succes.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            TbxName.Text = "";
                            TbxLastName.Text = "";
                            TbxEmail.Text = "";
                            TbxPhone.Text = "";
                            TbxStatus.Text = "";
                            TbxAge.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fejl ved tilføjelse af medarbejder: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Alder skal være et heltal.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
