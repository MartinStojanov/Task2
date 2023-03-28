using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    class Category
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public string picture { get; set; }


        public Category(int catehoryID, string categoryName, string description, string picture)
        {
            this.categoryID = categoryID;
            this.categoryName = categoryName;
            this.description = description;
            this.picture = picture;
        }
        public Category(string categoryName, string description, string picture)
        {
            this.categoryName = categoryName;
            this.description = description;
            this.picture = picture;
        }
        
        public static void createCategoryandInsertInDatabase(string categoryName, string description, string picture)
        {
            Category tmp = new Category(categoryName,description,picture);
            string connectionString = Settings.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Categories (CategoryName,Description,Picture) VALUES (@categoryName,@description,@picture)", tmp);
                connection.Close();
            }
        }
        public static void createCategoriesTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"
                       CREATE TABLE Categories (
                          ID int identity(1,1) primary key,
                          CategoryName char(50),
                          Description char(50),
                          Picture char(50)
                          );
                              ");
                connection.Close();
            }
        }
        public static void dropCategoriesTable()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("DROP TABLE Categories");
                connection.Close();
            }
        }
        public static List<CategoryWithSales> GetCategoriesSortedByMostSoldProducts()
        {
            string connectionString = Settings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var categories = connection.Query<CategoryWithSales>(
               "SELECT Categories.ID, Categories.CategoryName, SUM(OrdersDetails.Quantity) as TotalSales " +
               "FROM Categories " +
               "INNER JOIN Products ON Categories.ID = Products.CategoryId " +
               "INNER JOIN OrdersDetails ON Products.ID = OrdersDetails.ProductId " +
               "GROUP BY Categories.ID, Categories.CategoryName " +
               "ORDER BY TotalSales DESC")
           .ToList();

                
                connection.Close();
                return categories;

            }

        }
    }
}
