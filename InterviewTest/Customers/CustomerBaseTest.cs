using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public class ConcreteCustomer : CustomerBase
    {
        public ConcreteCustomer(OrderRepository orderRepo, ReturnRepository returnRepo)
            : base(orderRepo, returnRepo)
        {
        }

        public override int CustomerId { get; set; }

        public override string GetName()
        {
            return "Concrete Customer";
        }
    }
}
