using System;
using System.Reflection;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Entities.OrganisationModule;
using Microsoft.EntityFrameworkCore;

namespace Matario.Persistence.DbContexts
{
	public partial class ApplicationDbContext : DbContext
	{
        // Authentication Module
		public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        // organisation Module
        public DbSet<Organisation> Organisations { get; set; }

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

