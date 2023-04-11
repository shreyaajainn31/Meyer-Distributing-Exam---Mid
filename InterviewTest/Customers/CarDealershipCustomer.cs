using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public class CarDealershipCustomer : CustomerBase
    {
        public CarDealershipCustomer(OrderRepository orderRepo, ReturnRepository returnRepo)
            : base(orderRepo, returnRepo)
        {

        }

        public override int CustomerId { get; set; } = 1;

        public override string GetName()
        {
            return "Ruxer Ford Lincoln, Inc.";
        }
    }
}
