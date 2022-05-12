using Contact.Domain.Common;
using Contact.Domain.ContactPersonAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Persistence
{
    public class ContactContext : DbContext
    {
        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<ContactPersonInfo> ContactPersonInfos { get; set; }

        //Only for unit test. Comment out when publish.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Server=localhost;Port=5432;Database=ContactDb;User Id=admin;Password=admin1234;");


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "burhan"; //TODO: User Claim
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "burhan"; //TODO: User Claim
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
