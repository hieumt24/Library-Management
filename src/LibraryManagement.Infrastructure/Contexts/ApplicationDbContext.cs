using LibraryManagement.Application.Models.DTOs.Account;
using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable("User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRole");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaim");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaim");
            });

            //Configure relationships for BookBorrowingRequest
            builder.Entity<BookBorrowingRequest>()
                .HasOne<User>(b => b.Requester)
                .WithMany()
                .HasForeignKey(b => b.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookBorrowingRequest>()
                .HasOne<User>(b => b.Approver)
                .WithMany()
                .HasForeignKey(b => b.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}