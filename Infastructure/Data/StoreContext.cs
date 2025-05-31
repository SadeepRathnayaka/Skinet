using System;
using Core.Entities;
using Infastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data;

public class StoreContext : DbContext
{

    // constructor
    public StoreContext(DbContextOptions options) : base(options)    // input param options : SQL server connection string 
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }


}
