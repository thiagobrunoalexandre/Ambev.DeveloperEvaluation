using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Fakes
{
    public class FakeSaleRepository : SaleRepository
    {
        private readonly List<Sale> _sales = new List<Sale>();

        public FakeSaleRepository(DbContextOptions<DefaultContext> options)
            : base(new DefaultContext(options)) { }

        public override Task<IEnumerable<Sale>> GetAllAsync() => Task.FromResult<IEnumerable<Sale>>(_sales);

        public override Task<Sale?> GetByIdAsync(Guid id) => Task.FromResult(_sales.FirstOrDefault(s => s.Id == id));

        public override Task<Sale> AddAsync(Sale sale)
        {
            _sales.Add(sale);
            return Task.FromResult(sale);
        }

        public override Task<Sale?> UpdateAsync(Guid id, Sale sale)
        {
            var existingSale = _sales.FirstOrDefault(s => s.Id == id);
            if (existingSale == null) return Task.FromResult<Sale?>(null);

            _sales.Remove(existingSale);
            _sales.Add(sale);
            return Task.FromResult<Sale?>(sale);
        }

        public override Task<bool> DeleteAsync(Guid id)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            if (sale == null) return Task.FromResult(false);

            _sales.Remove(sale);
            return Task.FromResult(true);
        }
    }
}
