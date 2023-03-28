using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models
{
    class CategoryWithSales
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public int totalSales { get; set; }

  

        public CategoryWithSales(System.Int32 ID, System.String CategoryName, System.Int32 TotalSales)
        {
            this.categoryID = ID;
            this.categoryName = CategoryName;
            this.totalSales = TotalSales;
        }
    }
}
