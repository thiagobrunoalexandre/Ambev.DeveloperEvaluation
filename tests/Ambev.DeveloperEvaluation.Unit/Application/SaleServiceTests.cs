using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Unit.Fakes;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class SaleServiceTests
    {
        private readonly SaleService _saleService;
        private readonly FakeSaleRepository _fakeSaleRepository;

        public SaleServiceTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") 
                .Options;

            _fakeSaleRepository = new FakeSaleRepository(options);
            _saleService = new SaleService(_fakeSaleRepository);
        }

        [Fact]
        public async Task Should_Create_Sale_Successfully()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "Cerveja", 10.00m);
            var saleItem = new SaleItem(product, 2);
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), new List<SaleItem> { saleItem });

            // Act
            var result = await _saleService.CreateSaleAsync(sale);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
        }
    }
}
