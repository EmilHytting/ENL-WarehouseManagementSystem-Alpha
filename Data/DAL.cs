using ENL___WarehouseManagementSystem.Data.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ENL___WarehouseManagementSystem.Data
{
    class DAL
    {
        private readonly string connectionString;

        public DAL(string connectionString)
        {
            this.connectionString = connectionString;
        }




        //USERLOGIN//
        public bool ValidateLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public void AddUser(string firstName, string lastName, string email, string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO [User] (FirstName, LastName, Email, Username, Password) VALUES (@FirstName, @LastName, @Email, @Username, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //EMPLOYEES//


        public List<Employee> GetAllEmployees()
        {
            List<Employee> empList = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        empList.Add(new Employee
                        {
                            EmpID = Convert.ToInt32(reader["EmpID"]),
                            EmpName = reader["EmpName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            EmpEmail = reader["EmpEmail"].ToString(),
                            EmpPhone = reader["EmpPhone"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            EmploymentStatus = reader["EmploymentStatus"].ToString()
                        });
                    }
                }
            }

            return empList;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Employees (EmpName, LastName, EmpEmail, EmpPhone, EmploymentStatus) VALUES (@EmpName, @LastName, @EmpEmail, @EmpPhone, @EmploymentStatus)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpName", employee.EmpName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@EmpEmail", employee.EmpEmail);
                    cmd.Parameters.AddWithValue("@EmpPhone", employee.EmpPhone);
                    cmd.Parameters.AddWithValue("@EmploymentStatus", employee.EmploymentStatus);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE EmpID = @EmpID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpID", employee.EmpID);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public List<Employee> SearchEmployee(int empId, string empName)
        {
            List<Employee> searchResults = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees WHERE (EmpID = @EmpId OR @EmpId IS NULL) AND (EmpName LIKE @EmpName OR @EmpName IS NULL)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId == 0 ? (object)DBNull.Value : empId);
                    cmd.Parameters.AddWithValue("@EmpName", string.IsNullOrEmpty(empName) ? (object)DBNull.Value : "%" + empName + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            searchResults.Add(new Employee
                            {
                                EmpID = Convert.ToInt32(reader["EmpID"]),
                                EmpName = reader["EmpName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                EmpEmail = reader["EmpEmail"].ToString(),
                                EmpPhone = reader["EmpPhone"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                EmploymentStatus = reader["EmploymentStatus"].ToString()
                            });
                        }
                    }
                }
            }

            return searchResults;
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Employees SET EmpName = @EmpName, LastName = @LastName, EmpEmail = @EmpEmail, EmpPhone = @EmpPhone, EmploymentStatus = @EmploymentStatus WHERE EmpID = @EmpID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpID", employee.EmpID);
                    cmd.Parameters.AddWithValue("@EmpName", employee.EmpName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@EmpEmail", employee.EmpEmail);
                    cmd.Parameters.AddWithValue("@EmpPhone", employee.EmpPhone);
                    cmd.Parameters.AddWithValue("@EmploymentStatus", employee.EmploymentStatus);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        //PRODUKTER//

        public List<Produkt> GetProducts()
        {
            List<Produkt> productList = new List<Produkt>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Produkt";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productList.Add(new Produkt
                        {
                            ProdID = Convert.ToInt32(reader["ProdID"]),
                            ProdNavn = reader["ProdNavn"].ToString(),
                            ProdAntal = Convert.ToInt32(reader["ProdAntal"]),
                            ProdBeskrivelse = reader["ProdBeskrivelse"].ToString(),
                            ProdPlacering = reader["ProdPlacering"].ToString(),
                            Oprettelse = Convert.ToDateTime(reader["Oprettelse"])
                        });
                    }
                }
            }

            return productList;
        }

        public Produkt GetProductById(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Produkt WHERE ProdID = @ProdId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdID", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Produkt
                            {
                                ProdID = Convert.ToInt32(reader["ProdID"]),
                                ProdNavn = reader["ProdNavn"].ToString(),
                                ProdAntal = Convert.ToInt32(reader["ProdAntal"]),
                                ProdBeskrivelse = reader["ProdBeskrivelse"].ToString(),
                                ProdPlacering = reader["ProdPlacering"].ToString(),
                                Oprettelse = Convert.ToDateTime(reader["Oprettelse"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void AddProduct(Produkt product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Produkt (ProdNavn, ProdAntal, ProdBeskrivelse, ProdPlacering, Oprettelse) VALUES (@ProdNavn, @ProdAntal, @ProdBeskrivelse, @ProdPlacering, @Oprettelse)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdNavn", product.ProdNavn);
                    cmd.Parameters.AddWithValue("@ProdAntal", product.ProdAntal);
                    cmd.Parameters.AddWithValue("@ProdBeskrivelse", product.ProdBeskrivelse);
                    cmd.Parameters.AddWithValue("@ProdPlacering", product.ProdPlacering);
                    cmd.Parameters.AddWithValue("@Oprettelse", product.Oprettelse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Produkt product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Produkt SET ProdNavn = @ProdNavn, ProdAntal = @ProdAntal, ProdBeskrivelse = @ProdBeskrivelse, ProdPlacering = @ProdPlacering, Oprettelse = @Oprettelse WHERE ProdID = @ProdID"; // Erstat med dit tabelnavn

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdID", product.ProdID);
                    cmd.Parameters.AddWithValue("@ProdNavn", product.ProdNavn);
                    cmd.Parameters.AddWithValue("@ProdAntal", product.ProdAntal);
                    cmd.Parameters.AddWithValue("@ProdBeskrivelse", product.ProdBeskrivelse);
                    cmd.Parameters.AddWithValue("@ProdPlacering", product.ProdPlacering);
                    cmd.Parameters.AddWithValue("@Oprettelse", product.Oprettelse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveProduct(Produkt product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Produkt WHERE ProdID = @ProdID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdID", product.ProdID);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AddOrdre(Ordre ordre)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Ordre (ProdNavn, OrdreAntal, OrdreStatus, EmpName, CreatedDate) VALUES (@ProdNavn, @OrdreAntal, @OrdreStatus, @EmpName, @CreatedDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdNavn", ordre.ProdNavn);
                    cmd.Parameters.AddWithValue("@OrdreAntal", ordre.OrdreAntal);
                    cmd.Parameters.AddWithValue("@OrdreStatus", ordre.OrdreStatus);
                    cmd.Parameters.AddWithValue("@EmpName", ordre.EmpName);
                    cmd.Parameters.AddWithValue("@CreatedDate", ordre.CreatedDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Ordre//
        public List<Ordre> GetOrdre()
        {
            List<Ordre> ordreList = new List<Ordre>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Ordre"; // Brug "Ordre" i stedet for "Produkt"

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ordreList.Add(new Ordre
                        {
                            OrdreID = Convert.ToInt32(reader["OrdreID"]),
                            ProdNavn = reader["ProdNavn"].ToString(),
                            OrdreAntal = Convert.ToInt32(reader["OrdreAntal"]),
                            OrdreStatus = reader["OrdreStatus"].ToString(),
                            EmpName = reader["EmpName"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        });
                    }
                }
            }

            return ordreList;
        }
    }
}