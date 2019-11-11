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
    public class CarrierBizDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void UpdateCarrier(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", carrier.carrierID);
                myCommand.Parameters.AddWithValue("@CategoryId", carrier.depotCity);
                myCommand.Parameters.AddWithValue("@UnitPrice", carrier.carrierName);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ftlAvail);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ltlAvail);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ftlRate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ltlRate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.feeferCharge);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertCarrier(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", carrier.carrierID);
                myCommand.Parameters.AddWithValue("@CategoryId", carrier.depotCity);
                myCommand.Parameters.AddWithValue("@UnitPrice", carrier.carrierName);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ftlAvail);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ltlAvail);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ftlRate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.ltlRate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", carrier.feeferCharge);


                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteCarrier(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", carrier.carrierID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Carrier> GetCarriers(string searchItem)
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

                var carriers = DataTableToCarrierList(dataTable);

                return carriers;
            }
        }

        private List<Carrier> DataTableToCarrierList(DataTable table)
        {
            var carriers = new List<Carrier>();

            foreach (DataRow row in table.Rows)
            {
                carriers.Add(new Carrier
                {
                    carrierID = row["contractID"].ToString(),
                    depotCity = row["initiateBy"].ToString(),
                    carrierName = row["startDate"].ToString(),
                    ftlAvail = Convert.ToDouble(row["ftlAvail"]),
                    ltlAvail = Convert.ToDouble(row["ltlAvail"]),
                    ftlRate = Convert.ToDouble(row["ftlRate"]),
                    ltlRate = Convert.ToDouble(row["ltlRate"]),
                    feeferCharge = Convert.ToDouble(row["feeferCharge"])
            });
            }

            return carriers;
        }
    }
}
