namespace ECommerceApplication.Models
{
    public class ShippingAddress
    {
       public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public Customer user{ get; set; }
    }
}