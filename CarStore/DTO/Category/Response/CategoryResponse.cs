namespace CarStore.DTO.Category.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<CategoryProductResponse> Products { get; set; }
    }
}
