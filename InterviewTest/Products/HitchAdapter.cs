namespace InterviewTest.Products
{
    public class HitchAdapter : IProduct
    {
         public int ProductId { get; set; } = 2;
        public float GetSellingPrice()
        {
            return 70;
        }

        public string GetProductNumber()
        {
            return "DrawTite 5504";
        }
    }
}
