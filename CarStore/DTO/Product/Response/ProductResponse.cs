namespace CarStore.DTO.Product.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int ProductionYear { get; set; }

        public int Price { get; set; }

        public int Km { get; set; }

        public string Description { get; set; }

        public ProductCategoryResponse Category { get; set; }

    }
}
