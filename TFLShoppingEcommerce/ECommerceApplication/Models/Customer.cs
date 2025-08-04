namespace ECommerceApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }

        public Customer()
        {
            CustomerId = 0;
            UserName = "";
            Password = "";
            Email = "";
            Address = "";
        }

        public Customer(int id, string username, string password,string email, string address)
        {
            CustomerId = id;
            UserName = username;
            Password = password;
            Email = email;
            Address = address;
            
        }
    }
    
}