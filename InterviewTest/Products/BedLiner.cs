namespace InterviewTest.Products
{
    public class BedLiner : IProduct
    {
        public int ProductId { get; set; } = 1;

        public string GetProductNumber()
        {
            return "Rugged Liner F55U15";
        }

        public float GetSellingPrice()
        {
            return 150;
        }
    }
}
