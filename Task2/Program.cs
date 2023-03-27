using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data.SqlClient;
using System.Linq;
using Task2.Models;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                 
              Category.createCategoriesTable();
              Product.createProductsTable();
              Order.createOrdersTable();
              OrderDetails.createOrderDetailsTable();
                Category.createCategoryandInsertInDatabase("Category1", "Description1", "Picture1");
                Category.createCategoryandInsertInDatabase("Category2", "Description2", "Picture2");
                Product.createandInsertToDatabaseProduct("Product1", 1, 1, 50, 5);
                Product.createandInsertToDatabaseProduct("Product2", 1, 2, 80, 8);
                Product.createandInsertToDatabaseProduct("Product3", 2, 1, 30, 15);

                Order.createandInsertToDatabaseOrder(4, "2012-10-22", "Skopje");
                Order.createandInsertToDatabaseOrder(5, "2012-12-22", "Skopje");
                Order.createandInsertToDatabaseOrder(5, "2012-11-22", "Skopje");

                OrderDetails.createandInsertToDatabaseOrderDetail(1, 1, 50, 5, 4);
                IEnumerable<Order> orders = Order.getListOfOrdersSortedByDate();
                foreach(Order order in orders)
                {
                    Console.WriteLine(order.orderID + " " + order.customerID + " " + order.orderDate + " " + order.shipCity);
                }
                // connection.Execute("DROP TABLE Categories");
                // connection.Execute("DROP TABLE Products");
                // connection.Execute("DROP TABLE Orders");

            }
        }
    }
}
