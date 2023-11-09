
namespace Store
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public partial class order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public System.DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        StoreDataBaseEntities context = new StoreDataBaseEntities();
        public bool Insert(order o)
        {
            bool isSuccess = false;

            using (var context = new StoreDataBaseEntities())
            {
                try
                {
                    var newOrder = new order
                    {
                        Name = o.Name,
                        Price = o.Price,
                        TotalPrice = o.TotalPrice,
                        Quantity = o.Quantity,
                        OrderDate = o.OrderDate,
                    };

                    context.orders.Add(newOrder);
                    context.SaveChanges();

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Insert method: " + ex.Message);
                }
            }

            return isSuccess;
        }

        public DataTable Select()
        {
            DataTable dt = new DataTable();
            using (var context = new StoreDataBaseEntities())
            {
                try
                {
                    var orders = context.orders.ToList();

                    // Create the structure of the DataTable based on the entity's properties
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Price", typeof(decimal));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                    dt.Columns.Add("OrderDate", typeof(DateTime));

                    // Populate the DataTable with data from the entity
                    foreach (var order in orders)
                    {
                        dt.Rows.Add(order.Id, order.Name, order.Price, order.TotalPrice, order.Quantity, order.OrderDate);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Select method: " + ex.Message);
                }
            }
            return dt;
        }

        public DataTable FilterOrderDataByMonthYear(int mm, int yy)
        {
            DataTable dt = new DataTable();

            using (var context = new StoreDataBaseEntities())
            {
                try
                {
                    var filteredOrders = context.orders
                        .Where(o => o.OrderDate.Month == mm && o.OrderDate.Year == yy)
                        .ToList();

                    // Add columns to the DataTable based on the order entity properties
                    foreach (var property in typeof(order).GetProperties())
                    {
                        dt.Columns.Add(property.Name, property.PropertyType);
                    }

                    foreach (var order in filteredOrders)
                    {
                        DataRow row = dt.NewRow();

                        // Set values for each column based on the order entity properties
                        foreach (var property in typeof(order).GetProperties())
                        {
                            row[property.Name] = property.GetValue(order);
                        }

                        dt.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in FilterOrderDataByMonthYear method: " + ex.Message);
                }
            }

            return dt;
        }

        public DataTable FilterOrderDataByMonthYearOrderByName(int mm, int yy)
        {
            DataTable dt = new DataTable();

            using (var context = new StoreDataBaseEntities())
            {
                try
                {
                    var filteredOrders = context.orders
                        .Where(o => o.OrderDate.Month == mm && o.OrderDate.Year == yy)
                        .OrderBy(o => o.Name)
                        .ToList();

                    // Add columns to the DataTable based on the order entity properties
                    foreach (var property in typeof(order).GetProperties())
                    {
                        dt.Columns.Add(property.Name, property.PropertyType);
                    }

                    foreach (var order in filteredOrders)
                    {
                        DataRow row = dt.NewRow();

                        // Set values for each column based on the order entity properties
                        foreach (var property in typeof(order).GetProperties())
                        {
                            row[property.Name] = property.GetValue(order);
                        }

                        dt.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in FilterOrderDataByMonthYearOrderByName method: " + ex.Message);
                }
            }

            return dt;
        }

        public decimal CalculateTotalPriceByMonthYear(int mm, int yy)
        {
            decimal totalOrderPrice = 0;
            using (var context = new StoreDataBaseEntities()) // Replace YourDataContext with your actual DataContext class
            {
                
                try
                {
                    totalOrderPrice = context.orders
                        .Where(p => p.OrderDate.Month == mm && p.OrderDate.Year == yy)
                        .Sum(p => p.TotalPrice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in CalculateTotalPriceByMonthYear method: " + ex.Message);
                }
            }
            return totalOrderPrice;
        }




    }
}
