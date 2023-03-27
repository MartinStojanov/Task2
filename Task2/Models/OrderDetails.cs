using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    class OrderDetails
    {
        public int orderID { get; set; }
        public int productID { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
        public int discount { get; set; }

        public OrderDetails(int orderID,int productID,int unitPrice, int quantity, int discount)
        {
            this.orderID = orderID;
            this.productID = productID;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
            this.discount = discount;
        }
        public static void createOrderDetailsTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Execute(@"
                          CREATE TABLE OrdersDetails (
                           OrderID INTEGER NOT NULL,
                           ProductID INTEGER NOT NULL,
                           UnitPrice INTEGER,
                           Quantity INTEGER,
                           Discount INTEGER,
                           PRIMARY KEY (OrderID, ProductID),
                           FOREIGN KEY (OrderID) REFERENCES Orders(ID),
                           FOREIGN KEY (ProductID) REFERENCES Products(ID)
                               );
                              ");
            }

        }
        public static void createandInsertToDatabaseOrderDetail(int orderID, int productID, int unitPrice, int quantity,
            int discount)
        {
            OrderDetails tmp = new OrderDetails( orderID, productID,unitPrice,quantity, discount);
            string connectionString = Settings.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO OrdersDetails (OrderID,ProductID,UnitPrice, Quantity,Discount) VALUES (@orderID, @productID,@unitPrice,@quantity, @discount)", tmp);
                connection.Close();
            }
            
        }
        public static void dropOrdersDetailsTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("DROP TABLE OrdersDetails");
                connection.Close();
            }
        }
    }
}
