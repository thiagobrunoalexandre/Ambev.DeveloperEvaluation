using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService
    {
        private readonly SaleRepository _saleRepository;
        private readonly ILogger _logger;

        public SaleService(SaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
            _logger = Log.ForContext<SaleService>(); 
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            _logger.Information("Retrieved {SaleCount} sales from database", sales.Count());
            return sales;
        }

        public async Task<Sale?> GetSaleByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                _logger.Warning("Sale with ID {SaleId} not found", id);
            }
            else
            {
                _logger.Information("Retrieved sale with ID {SaleId}", id);
            }
            return sale;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            var createdSale = await _saleRepository.AddAsync(sale);
            _logger.Information("SaleCreated: Sale {SaleId} created with {ItemCount} items", createdSale.Id, createdSale.Items.Count);
            return createdSale;
        }

        public async Task<Sale?> UpdateSaleAsync(Guid id, Sale sale)
        {
            var updatedSale = await _saleRepository.UpdateAsync(id, sale);
            if (updatedSale == null)
            {
                _logger.Warning("Sale with ID {SaleId} not found for update", id);
            }
            else
            {
                _logger.Information("SaleModified: Sale {SaleId} updated successfully", id);
            }
            return updatedSale;
        }

        public async Task<bool> DeleteSaleAsync(Guid id)
        {
            var deleted = await _saleRepository.DeleteAsync(id);
            if (deleted)
            {
                _logger.Information("SaleCancelled: Sale {SaleId} was cancelled and removed", id);
            }
            else
            {
                _logger.Warning("Attempted to cancel sale {SaleId}, but it was not found", id);
            }
            return deleted;
        }
    }
}
