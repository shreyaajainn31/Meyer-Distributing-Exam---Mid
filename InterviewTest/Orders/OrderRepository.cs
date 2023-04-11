using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System;

namespace InterviewTest.Orders
{

    public class OrderRepository
    {
        string connectionString ="server=localhost;user=shreya;password=root;database=meyer_database;";
        private List<IOrder> orders;
        public OrderRepository()
        {
            orders = new List<IOrder>();
        }

        public void Add(IOrder newOrder)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                   string query = "INSERT INTO Orders (customerId, orderNumber, orderDate) VALUES (@customerId, @orderNumber, @orderDate)";


                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@customerId", newOrder.Customer.CustomerId);
                    command.Parameters.AddWithValue("@orderNumber", newOrder.OrderNumber);
                    command.Parameters.AddWithValue("@orderDate", DateTime.Now);

                   int orderId = Convert.ToInt32(command.ExecuteScalar());

                    foreach (var product in newOrder.Products)
                    {
                        query = "INSERT INTO OrderProduct (orderId, productId) VALUES (@orderId, @productId)";
                        command = new MySqlCommand(query, conn);
                        command.Parameters.AddWithValue("@orderId", orderId);
                        command.Parameters.AddWithValue("@productId", product.ProductId);
                        command.ExecuteNonQuery();
                    }

            // Add the order to the orders list
            orders.Add(newOrder);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        public void Remove(IOrder removedOrder)
        {
            orders = orders.Where(o => !string.Equals(removedOrder.OrderNumber, o.OrderNumber)).ToList();
        }

        public List<IOrder> Get()
        {
            return orders;
        }
    }
}
