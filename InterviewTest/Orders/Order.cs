﻿using System.Collections.Generic;
using InterviewTest.Customers;
using InterviewTest.Products;

namespace InterviewTest.Orders
{
    public class Order : IOrder
    {
        public Order(string orderNumber, ICustomer customer)
        {
            OrderNumber = orderNumber;
            Customer = customer;
            Products = new List<OrderedProduct>();
        }

        public string OrderNumber { get; }
        public ICustomer Customer { get; }
        public List<OrderedProduct> Products { get; }

        public void AddProduct(IProduct product)
        {
            Products.Add(new OrderedProduct(product));
        }
    }
}
