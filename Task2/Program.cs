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
                OrderDetails.createandInsertToDatabaseOrderDetail(2, 1, 50, 5, 4);
                OrderDetails.createandInsertToDatabaseOrderDetail(2, 2, 50, 5, 4);
                OrderDetails.createandInsertToDatabaseOrderDetail(3, 3, 58, 5, 4);
                IEnumerable<Order> orders = Order.getListOfOrdersSortedByDate();
                Console.WriteLine("List of orders sorted by date");
                foreach (Order order in orders)
                {
                    Console.WriteLine(order.orderID + " " + order.customerID + " " + order.orderDate + " " + order.shipCity);
                }
                Console.WriteLine("Products sorted by most sold products");
                IEnumerable<Product> products = Product.GetProductsSortedByMostSold();
                foreach (Product product in products)
                {
                    Console.WriteLine(product.productID + " " + product.productName + " " + product.unitPrice + " " + product.unitInStock);
                }
                List<CategoryWithSales> categoryWithSales =  Category.GetCategoriesSortedByMostSoldProducts();
                Console.WriteLine("Categories sorted by most sold products");
              
                foreach (CategoryWithSales cat in categoryWithSales)
                {
                    Console.WriteLine(String.Format("ID: {0},Name: {1},Total sales: {2}", cat.categoryID,cat.categoryName,cat.totalSales));
                   // Console.WriteLine(cat.categoryID + " " + cat.categoryName + " " + cat.totalSales);
                }
                OrderDetails.dropOrdersDetailsTable();
                Order.dropOrdersTable();
                Product.dropProductsTable();
                Category.dropCategoriesTable();
            }
        }
    }
}
