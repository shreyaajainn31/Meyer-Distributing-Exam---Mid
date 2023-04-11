namespace InterviewTest.Products
{
    public class ReplacementBumper : IProduct
    {
         public int ProductId { get; set; } = 3;
        public float GetSellingPrice()
        {
            return 155;
        }

        public string GetProductNumber()
        {
            return "Sherman 036-87-1";
        }
    }
}
