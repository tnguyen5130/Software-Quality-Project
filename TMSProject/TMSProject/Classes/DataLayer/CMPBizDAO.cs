using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.BusinessLayer;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace TMSProject.Classes.DataLayer
{
    public class CMPBizDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void UpdateCMP(ContractMarketPlace cmp)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", cmp.customerID);
                myCommand.Parameters.AddWithValue("@CategoryId", cmp.contractID);
                myCommand.Parameters.AddWithValue("@UnitPrice", cmp.jobType);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.quantity);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.origin);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.destination);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.vanType);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertCMP(ContractMarketPlace cmp)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", cmp.customerID);
                myCommand.Parameters.AddWithValue("@CategoryId", cmp.contractID);
                myCommand.Parameters.AddWithValue("@UnitPrice", cmp.jobType);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.quantity);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.origin);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.destination);
                myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.vanType);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteCMP(ContractMarketPlace cmp)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", cmp.customerID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<ContractMarketPlace> GetCMPs(string searchItem)
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

                var cmps = DataTableToCMPList(dataTable);

                return cmps;
            }
        }

        private List<ContractMarketPlace> DataTableToCMPList(DataTable table)
        {
            var orders = new List<ContractMarketPlace>();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new ContractMarketPlace
                {
                    customerID = row["customerID"].ToString(),
                    contractID = row["contractID"].ToString(),
                    jobType = row["jobType"].ToString(),
                    quantity = Convert.ToInt32(row["quantity"]),
                    origin = row["origin"].ToString(),
                    destination = row["destination"].ToString(),
                    vanType = row["vanType"].ToString()
                });
            }

            return orders;
        }
    }
}
