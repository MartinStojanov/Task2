using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    class Order
    {
        public int orderID { get; set; }
        public int customerID { get; set; }
        public int employeeID { get; set; }
        public string orderDate { get; set; }
        public string shipName { get; set; }
        public string shipCity { get; set; }
        public string shipRegion { get; set; }


        public Order(int customerID, string orderDate, string shipCity)
        {
            this.customerID = customerID;
            this.orderDate = orderDate;
            this.shipCity = shipCity;
        }
      
        public Order(System.Int32 ID, System.Int32 CustomerID, System.DateTime OrderDate, System.String ShipCity)
        {
            this.orderID = ID;
            this.customerID = CustomerID;
            this.orderDate = OrderDate.ToString();
            this.shipCity = ShipCity;
        }

        public static void createandInsertToDatabaseOrder(int CustomerID, string OrderDate, string ShipCity)
        {
            Order tmp = new Order(CustomerID, OrderDate, ShipCity);

            string connectionString = Settings.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Orders (CustomerID, OrderDate,ShipCity) VALUES (@customerID,@orderDate, @shipCity)", tmp);
                connection.Close();
            }
            
        }
        public static void createOrdersTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                  connection.Execute(@"
                       CREATE TABLE Orders (
                          ID int identity(1,1) primary key,
                          CustomerID int,
                          OrderDate DATE,
                          ShipCity char(20)
                           );
                              ");
                connection.Close();
            }
        }
        public static IEnumerable<Order> getListOfOrdersSortedByDate()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                connection.Open();
                var customers = connection.Query<Order>("SELECT * FROM Orders ORDER BY OrderDate");
                connection.Close();
                return customers.ToList();
                
            }
            

        }
        public static void dropOrdersTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("DROP TABLE Orders");
                connection.Close();
            }
        }
    }
}
