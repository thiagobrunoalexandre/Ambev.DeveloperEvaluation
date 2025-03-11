using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class SaleSpecificationTests
    {
        [Fact]
        public void Should_Apply_10_Percent_Discount_When_Buying_4_Items()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "Cerveja Pilsen", 10.00m);
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), new List<SaleItem>
            {
                new SaleItem(product, 4) // 4 unidades de R$10,00 cada
            });

            // Act
            var totalAmount = sale.TotalAmount;

            // Assert
            var expectedTotal = (10.00m * 4) * 0.9m; // 10% de desconto
            totalAmount.Should().Be(expectedTotal);
        }

        [Fact]
        public void Should_Apply_20_Percent_Discount_When_Buying_10_Items()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "Cerveja Lager", 10.00m);
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), new List<SaleItem>
            {
                new SaleItem(product, 10) // 10 unidades de R$10,00 cada
            });

            // Act
            var totalAmount = sale.TotalAmount;

            // Assert
            var expectedTotal = (10.00m * 10) * 0.8m; // 20% de desconto
            totalAmount.Should().Be(expectedTotal);
        }

        [Fact]
        public void Should_Reject_Sale_With_More_Than_20_Items_Of_Same_Product()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "Cerveja Premium", 10.00m);

            // Act
            Action act = () => new Sale(Guid.NewGuid(), Guid.NewGuid(), new List<SaleItem>
            {
                new SaleItem(product, 21) // 21 unidades, acima do limite
            });

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Não é possível vender mais de 20 itens do mesmo produto.");
        }
        [Fact]
        public void Should_Not_Apply_Discount_When_Buying_Less_Than_4_Items()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "Cerveja Antarctica", 10.00m);
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), new List<SaleItem>
            {
                new SaleItem(product, 3) // 3 unidades de R$10,00 cada
            });

            // Act
            var totalAmount = sale.TotalAmount;

            // Assert
            var expectedTotal = 10.00m * 3; // Sem desconto
            totalAmount.Should().Be(expectedTotal);
        }
    }
}
