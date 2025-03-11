using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Should_Calculate_Total_Correctly()
        {
            // Arrange
            var product1 = new Product(Guid.NewGuid(), "Cerveja Pilsen", 10.00m);
            var product2 = new Product(Guid.NewGuid(), "Cerveja Lager", 5.00m);

            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), new List<SaleItem>
    {
        new SaleItem(product1, 2), // 2 unidades de R$10,00 cada
        new SaleItem(product2, 3)  // 3 unidades de R$5,00 cada
    });

            // Act
            var totalAmount = sale.TotalAmount;

            // Assert
            totalAmount.Should().Be(35.00m); // 20 + 15 = 35
        }

    }
}
