using System;
using Microsoft.EntityFrameworkCore;
using RazorTutorial.Models;

namespace RazorTutorial.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
    }
}
