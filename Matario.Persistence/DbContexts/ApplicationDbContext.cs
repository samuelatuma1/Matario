using System;
using System.Reflection;
using Matario.Domain.Entities.AuthenticationModule;
using Microsoft.EntityFrameworkCore;

namespace Matario.Persistence.DbContexts
{
	public partial class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Register all entity type configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

