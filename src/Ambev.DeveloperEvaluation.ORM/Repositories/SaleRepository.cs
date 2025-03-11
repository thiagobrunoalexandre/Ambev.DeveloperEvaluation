using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public virtual async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public virtual async Task<Sale> AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public virtual async Task<Sale?> UpdateAsync(Guid id, Sale sale)
        {
            var existingSale = await _context.Sales.FindAsync(id);
            if (existingSale == null) return null;

            _context.Entry(existingSale).CurrentValues.SetValues(sale);
            await _context.SaveChangesAsync();
            return existingSale;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
