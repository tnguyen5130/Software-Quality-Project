using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Model;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace TMSProject.Classes.Controller
{
    public class EmployeeBizDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void UpdateEmployee(Employee employee)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", employee.employeeID);
                myCommand.Parameters.AddWithValue("@CategoryId", employee.firstName);
                myCommand.Parameters.AddWithValue("@UnitPrice", employee.lastName);
                myCommand.Parameters.AddWithValue("@UnitsInStock", employee.employeeType);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertEmployee(Employee employee)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", employee.employeeID);
                myCommand.Parameters.AddWithValue("@CategoryId", employee.firstName);
                myCommand.Parameters.AddWithValue("@UnitPrice", employee.lastName);
                myCommand.Parameters.AddWithValue("@UnitsInStock", employee.employeeType);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteEmployee(Employee employee)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", employee.employeeID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Employee> GetEmployees(string searchItem)
        {
            const string sqlStatement = @" SELECT 
                                                ProductId, 
                                                ProductName, 
                                                QuantityPerUnit, 
                                                UnitPrice, 
                                                UnitsInStock, 
                                                QuantityPerUnit,
                                                UnitsOnOrder, 
                                                ReorderLevel,
                                                categories.CategoryId,
                                                CategoryName,
                                                Description, 
                                                suppliers.SupplierId,
                                                CompanyName
                                            FROM products
		                                        INNER JOIN categories ON products.CategoryId = categories.CategoryID 
                                                INNER JOIN suppliers ON products.SupplierId = suppliers.SupplierId
                                            WHERE Discontinued <> 1 
                                                AND ( ProductId = @SearchItem 
                                                        OR ProductName = @SearchItem
		                                                OR CategoryName = @SearchItem 
                                                        OR CompanyName = @SearchItem
		                                                OR @SearchItem = '')
                                            ORDER BY ProductName; ";


            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@SearchItem", searchItem);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var employees = DataTableToEmployeeList(dataTable);

                return employees;
            }
        }

        private List<Employee> DataTableToEmployeeList(DataTable table)
        {
            var employees = new List<Employee>();

            foreach (DataRow row in table.Rows)
            {
                employees.Add(new Employee
                {
                    employeeID = row["employeeID"].ToString(),
                    firstName = row["firstName"].ToString(),
                    lastName = row["lastName"].ToString(),
                    employeeType = row["employeeType"].ToString()
                    
                });
            }

            return employees;
        }
    }
}
