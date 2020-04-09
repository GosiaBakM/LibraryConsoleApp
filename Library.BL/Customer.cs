

namespace Library.BL
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(string customerName, string customerSurname)
        {
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            CustomerId = 0;
        }

        public int CustomerId { get; private set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        //public List<Book> BorrowedBooksList { get; set; }

        public override string ToString()
        {
            return $"Imię: {CustomerName} nazwisko: {CustomerSurname}";
        }
    }
}
