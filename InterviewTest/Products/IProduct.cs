namespace InterviewTest.Products
{
    public interface IProduct
    {



        int ProductId { get; set; }
        string GetProductNumber();
        float GetSellingPrice();
    }
}
