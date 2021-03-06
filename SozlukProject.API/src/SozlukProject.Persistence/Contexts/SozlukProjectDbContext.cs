using SozlukProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Persistence.Contexts
{
    public class SozlukProjectDbContext : DbContext
    {
        public SozlukProjectDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                    data.Entity.DateCreated = DateTime.UtcNow;

                if (data.State == EntityState.Modified)
                    data.Entity.DateUpdated = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Comment

            // modelBuilder.Entity<Comment>();


            //// Discussion

            // modelBuilder.Entity<Discussion>();

            // User

            modelBuilder.Entity<User>()
                .HasMany(user => user.Votes)
                .WithOne(votes => votes.User)
                .OnDelete(DeleteBehavior.NoAction);

            // UserActivation

            modelBuilder.Entity<UserActivation>()
                .HasOne(ua => ua.User)
                .WithOne(u => u.UserActivation)
                .HasForeignKey<UserActivation>(ua => ua.Id);

            //// Vote

            // modelBuilder.Entity<Vote>();
        }
    }
}
