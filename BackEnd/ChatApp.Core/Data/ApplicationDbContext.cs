using ChatApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");
                entity.HasKey(e => e.Id);
                entity.HasOne(m => m.Sender)
                        .WithMany()
                        .HasForeignKey(m => m.SenderId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(m => m.Receiver)
                        .WithMany()
                        .HasForeignKey(m => m.ReceiverId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }
    }
}