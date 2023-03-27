﻿using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    
    class Product
    {
       
        public int productID { get; set; }
        public string productName { get; set; }
        public int supplierID { get; set; }
        public int categoryID { get; set; }
        public int unitPrice { get; set; }
        public int unitInStock { get; set; }
        public int unitsOnOrder;
       
        public Product(string productName, int categoryID, int supplierID, int unitPrice, int unitInStock)
        {
            this.productName = productName;
            this.categoryID = categoryID;
            this.unitPrice = unitPrice;
            this.supplierID = supplierID;
            this.unitInStock = unitInStock;
        }
        public static void createandInsertToDatabaseProduct(string productname,int categoryID,int supplierID,
            int unitPrice,int unitInStock)
        {
            Product tmp = new Product(productname,categoryID, supplierID,
             unitPrice,  unitInStock);
            string connectionString = Settings.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Products (ProductName,CategoryID,SupplierID,UnitPrice,UnitInStock) VALUES (@productName,@categoryID, @supplierID, @unitPrice, @unitInStock)", tmp);
                connection.Close();
            }
            
        }
        public static void createProductsTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"
                       CREATE TABLE Products (
                          ID int identity(1,1) primary key,
                          ProductName char(50),
                          CategoryID int references Categories(ID),
                          SupplierID int not null,
                          UnitPrice int,
                          UnitInStock int
                             );
                              ");
                connection.Close();
            }
        }
        public static void dropProductsTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("DROP TABLE Products");
                connection.Close();
            }
        }
    }
}
