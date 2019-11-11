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
    public class ContractBizDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void UpdateContract(Contract contract)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", contract.contractID);
                myCommand.Parameters.AddWithValue("@CategoryId", contract.initiateBy);
                myCommand.Parameters.AddWithValue("@UnitPrice", contract.startDate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", contract.endDate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", contract.completeStatus);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertContract(Contract contract)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", contract.contractID);
                myCommand.Parameters.AddWithValue("@CategoryId", contract.initiateBy);
                myCommand.Parameters.AddWithValue("@UnitPrice", contract.startDate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", contract.endDate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", contract.completeStatus);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteContract(Contract contract)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", contract.contractID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Contract> GetContracts(string searchItem)
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

                var contracts = DataTableToContractList(dataTable);

                return contracts;
            }
        }

        private List<Contract> DataTableToContractList(DataTable table)
        {
            var orders = new List<Contract>();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new Contract
                {
                    contractID = row["contractID"].ToString(),
                    initiateBy = row["initiateBy"].ToString(),
                    startDate = row["startDate"].ToString(),
                    endDate = row["endDate"].ToString(),
                    completeStatus = row["completeStatus"].ToString()
            });
            }

            return orders;
        }
    }
}
