using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Sale> Sales { get; private set; } = new List<Sale>();
        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}
