using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RetailerStores.Application.Interfaces
{
    public interface IRetailerStoresDbContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
