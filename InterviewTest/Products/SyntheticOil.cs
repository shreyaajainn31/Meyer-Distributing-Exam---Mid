namespace InterviewTest.Products
{
    public class SyntheticOil : IProduct
    {
         public int ProductId { get; set; } = 4;

        public float GetSellingPrice()
        {
            return 25;
        }

        public string GetProductNumber()
        {
            return "Mobil 1 5W-30";
        }
    }
}
