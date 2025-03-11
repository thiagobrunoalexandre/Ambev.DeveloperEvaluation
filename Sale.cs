using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Guid BranchId { get; private set; }
        public Branch Branch { get; private set; }
        public decimal TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }
        public List<SaleItem> Items { get; private set; }

        public Sale(Guid customerId, Guid branchId, List<SaleItem> items)
        {
            Id = Guid.NewGuid();
            SaleNumber = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            SaleDate = DateTime.UtcNow;
            CustomerId = customerId;
            BranchId = branchId;
            Items = items;
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.TotalPrice;
            }
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Guid BranchId { get; private set; }
        public Branch Branch { get; private set; }
        public decimal TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }
        public List<SaleItem> Items { get; private set; }

        public Sale(Guid customerId, Guid branchId, List<SaleItem> items)
        {
            Id = Guid.NewGuid();
            SaleNumber = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            SaleDate = DateTime.UtcNow;
            CustomerId = customerId;
            BranchId = branchId;
            Items = items;
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.TotalPrice;
            }
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }
    }
}
