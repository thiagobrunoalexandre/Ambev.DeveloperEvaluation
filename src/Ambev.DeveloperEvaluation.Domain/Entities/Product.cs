﻿using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public ICollection<SaleItem> SaleItems { get; private set; } = new List<SaleItem>();
        private Product() { }

    
        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
