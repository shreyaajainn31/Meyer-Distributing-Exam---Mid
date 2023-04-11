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
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        
                        // Checking if order already exists.. 

                        string checkQuery = "SELECT COUNT(*) FROM Orders WHERE orderNumber = @orderNumber";
                        MySqlCommand checkCommand = new MySqlCommand(checkQuery, conn);
                        checkCommand.Parameters.AddWithValue("@orderNumber", newOrder.OrderNumber);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());


                        // Insert the order into the Orders table
                        string orderQuery = "INSERT INTO Orders (customerId, orderNumber, orderDate) VALUES (@customerId, @orderNumber, @orderDate)";
                        MySqlCommand orderCommand = new MySqlCommand(orderQuery, conn);
                        orderCommand.Parameters.AddWithValue("@customerId", newOrder.Customer.CustomerId);
                        orderCommand.Parameters.AddWithValue("@orderNumber", newOrder.OrderNumber);
                        orderCommand.Parameters.AddWithValue("@orderDate", DateTime.Now);
                        
                        // We will only add if we do not find that order in the Orders table
                        if(count == 0){
                            orderCommand.ExecuteNonQuery();
                        }

                        // Insert the order products into the OrderProduct table
                        foreach (var product in newOrder.Products)
                        {
                            string productQuery = "INSERT INTO OrderProduct (orderNumber, productId) VALUES (@orderNumber, @productId)";
                            MySqlCommand productCommand = new MySqlCommand(productQuery, conn);
                            productCommand.Parameters.AddWithValue("@orderNumber", newOrder.OrderNumber);
                            productCommand.Parameters.AddWithValue("@productId", product.Product.ProductId);
                            productCommand.ExecuteNonQuery();
                        }

                        // Add the order to the orders list
                        orders.Add(newOrder);

                        transaction.Commit();
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        public void Remove(IOrder removedOrder)
        {
          
           String orderNumber = removedOrder.OrderNumber;

           try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {

                         // Delete the order from orderProduct table
                        string orderProductQuery = "DELETE from OrderProduct where orderNumber = @orderNumber";
                        MySqlCommand orderProductCommand = new MySqlCommand(orderProductQuery, conn);
                        orderProductCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
                        orderProductCommand.ExecuteNonQuery();

                        // Delete the order from the Orders table
                        string orderQuery = "DELETE from Orders where orderNumber = @orderNumber";
                        MySqlCommand orderCommand = new MySqlCommand(orderQuery, conn);
                        orderCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
                        orderCommand.ExecuteNonQuery();

                       
                       
                        transaction.Commit();
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        public List<IOrder> Get()
        {
            return orders;
        }
    }
}
