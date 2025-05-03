using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextoDB _context;

        public UnitOfWork(ContextoDB context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //public async Task BeginTransactionAsync()
        //{
        //    _transaction = await _context.Database.BeginTransactionAsync();
        //}

        //public async Task CommitAsync()
        //{
        //    await _transaction.CommitAsync();
        //    await _transaction.DisposeAsync();
        //}

        //public async Task RollbackAsync()
        //{
        //    await _transaction.RollbackAsync();
        //    await _transaction.DisposeAsync();
        //}

        public void Dispose() => _context.Dispose();
    }
}
