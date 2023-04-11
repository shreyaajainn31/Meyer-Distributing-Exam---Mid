using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;
using System.Data;
using MySql.Data.MySqlClient;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        string connectionString ="server=localhost;user=shreya;password=root;database=meyer_database;";

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }
        public abstract int CustomerId { get; set; }

        public abstract string GetName();
        
        public void CreateOrder(IOrder order)
        {
            _orderRepository.Add(order);
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }

        public float GetTotalSales()
        {   
            float answer = 0f;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string getSalesQuery = @"SELECT SUM(p.productSellingPrice) 
                                from
                                 OrderProduct op, Product p 
                                 where op.productId = p.productId;   
                    ";

                    MySqlCommand checkCommand = new MySqlCommand(getSalesQuery, conn);
                    var result = checkCommand.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        answer = Convert.ToSingle(result);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            return answer;
        }

        public float GetTotalReturns()
        {
            throw new NotImplementedException();
        }

        public float GetTotalProfit()
        {
            throw new NotImplementedException();
        }
    }
}
