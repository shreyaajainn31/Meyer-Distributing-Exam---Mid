﻿using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using InterviewTest.Customers;
using InterviewTest.Orders;
using InterviewTest.Products;
using InterviewTest.Returns;

namespace InterviewTest
{
    public class Program
    {
        private static readonly OrderRepository orderRepo = new OrderRepository();
        private static readonly ReturnRepository returnRepo = new ReturnRepository();

        static void Main(string[] args)
        {
            // ------------------------
            // Coding Challenge Requirements
            // ------------------------

            // 1: Create a database, contained locally within this project, and refactor all repositories (Order, Return, and Product) to utilize it.
            // 2: Implement get total sales, returns, and profit in the CustomerBase class.
            // 3: Record when an item was purchased.
            // 4: Ensure all output results, when running this console app, are correct. 

            // ------------------------
            // Bonus
            // ------------------------

            // 1: Refactor the customer classes to be repository/database based
            // 2: Create unit tests


            // Connecting mysql database
            string connectionString = "server=localhost;user=shreya;password=root;database=meyer_database;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            // Checking if the connection is open
            try{
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    //  AddOrderRepositoryTest();
                    // RemoveOrderRepositoryTest();
                    GetTotalSales();

                }
                else
                {
                    Console.WriteLine("Connection is not open");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                connection.Close();
}


            // ProcessTruckAccessoriesExample();

            // ProcessCarDealershipExample();

            Console.ReadKey();
        }

        private static void GetTotalSales(){
            CustomerBase customerBase = new ConcreteCustomer(orderRepo, returnRepo);
            Console.WriteLine(customerBase.GetTotalSales());
        }

        private static void RemoveOrderRepositoryTest(){
            ICustomer customer = new CarDealershipCustomer(orderRepo, returnRepo);
            IOrder order = new Order("45678", customer);
            orderRepo.Remove(order);
        }
        private static void AddOrderRepositoryTest(){
            ICustomer customer = new CarDealershipCustomer(orderRepo, returnRepo);
            IOrder order = new Order("1234563", customer);
            order.AddProduct(new BedLiner());
            order.AddProduct(new SyntheticOil());
            orderRepo.Add(order);

            ICustomer customer1 = new TruckAccessoriesCustomer(orderRepo, returnRepo);
            IOrder order1 = new Order("45678", customer1);

            order1.AddProduct(new BedLiner());
            order1.AddProduct(new HitchAdapter());
            order1.AddProduct(new ReplacementBumper());
            orderRepo.Add(order1);

        }
        private static void ProcessTruckAccessoriesExample()
        {
            var customer = GetTruckAccessoriesCustomer();

            IOrder order = new Order("TruckAccessoriesOrder123", customer);
            customer.CreateOrder(order);

            IReturn rga = new Return("TruckAccessoriesReturn123", order);
            rga.AddProduct(order.Products.First());

            ConsoleWriteLineResults(customer);
        }

        private static void ProcessCarDealershipExample()
        { 
            var customer = GetCarDealershipCustomer();

            IOrder order = new Order("CarDealerShipOrder123", customer);
            order.AddProduct(new ReplacementBumper());
            order.AddProduct(new SyntheticOil());
            customer.CreateOrder(order);

            IReturn rga = new Return("CarDealerShipReturn123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);

            ConsoleWriteLineResults(customer);
        }

        private static ICustomer GetTruckAccessoriesCustomer()
        {
            return new TruckAccessoriesCustomer(orderRepo, returnRepo);
        }

        private static ICustomer GetCarDealershipCustomer()
        {
            return new CarDealershipCustomer(orderRepo, returnRepo);
        }

        private static void ConsoleWriteLineResults(ICustomer customer)
        {
            Console.WriteLine(customer.GetName());

            Console.WriteLine($"Total Sales: {customer.GetTotalSales().ToString("c")}");

            Console.WriteLine($"Total Returns: {customer.GetTotalReturns().ToString("c")}");

            Console.WriteLine($"Total Profit: {customer.GetTotalProfit().ToString("c")}");

            Console.WriteLine();
        }
    }
}
