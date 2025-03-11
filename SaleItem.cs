using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }

        public SaleItem(Product product, int quantity)
        {
            if (quantity > 20)
                throw new ArgumentException("Não é possível vender mais de 20 itens do mesmo produto.");

            Product = product;
            ProductId = product.Id;
            Quantity = quantity;
            UnitPrice = product.Price;
            Discount = CalculateDiscount(quantity);
            TotalPrice = (UnitPrice * Quantity) - Discount;
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10) return (UnitPrice * quantity) * 0.2m;
            if (quantity >= 4) return (UnitPrice * quantity) * 0.1m;
            return 0;
        }
    }
}
