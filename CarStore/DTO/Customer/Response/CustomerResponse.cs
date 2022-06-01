namespace CarStore.DTO.Customer.Response
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string AddressName { get; set; }

        public int ZipCode { get; set; }

        public string CityName { get; set; }

        public CustomerUserResponse User { get; set; }
    }
}
