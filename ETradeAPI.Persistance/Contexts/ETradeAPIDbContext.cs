﻿using ETradeAPI.Domain.Entities;
using ETradeAPI.Domain.Entities.Comman;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistance.Contexts
{
    public class ETradeAPIDbContext : DbContext
    {
        public ETradeAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasıını
            //sağlayan propertydir.Update operasyonlarınd Track edilen verileri yakalayıp elde etmemeizi sağlar.

            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate=DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
