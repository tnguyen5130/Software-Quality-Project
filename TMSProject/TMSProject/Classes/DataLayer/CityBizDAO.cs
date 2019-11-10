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
    public class CityBizDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void UpdateCity(City city)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", city.cityID);
                myCommand.Parameters.AddWithValue("@CategoryId", city.cityName);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertCarrier(City city)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", city.cityID);
                myCommand.Parameters.AddWithValue("@CategoryId", city.cityName);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteCity(City city)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", city.cityID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<City> GetCities(string searchItem)
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

                var cities = DataTableToCityList(dataTable);

                return cities;
            }
        }

        private List<City> DataTableToCityList(DataTable table)
        {
            var cities = new List<City>();

            foreach (DataRow row in table.Rows)
            {
                cities.Add(new City
                {
                    cityID = row["cityID"].ToString(),
                    cityName = row["cityName"].ToString(),
                    
                });
            }

            return cities;
        }
    }
}
