using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.DTOs.Account;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
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
                .HasOne<ApplicationUser>(b => b.Requester)
                .WithMany()
                .HasForeignKey(b => b.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookBorrowingRequest>()
                .HasOne<ApplicationUser>(b => b.Approver)
                .WithMany()
                .HasForeignKey(b => b.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            var userId = "332ffd7b-2305-49a5-a479-24307421df4a";

            var adminId = "843ef537-0078-496e-8fbd-ac24d3caca8d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userId,
                    Name = LibraryRoles.User,
                    NormalizedName = LibraryRoles.User.ToUpper(),
                    ConcurrencyStamp = userId
                },
                new IdentityRole
                {
                    Id = adminId,
                    Name = LibraryRoles.Admin,
                    NormalizedName = LibraryRoles.Admin.ToUpper(),
                    ConcurrencyStamp = adminId
                },
                new IdentityRole
                {
                    Name = LibraryRoles.SuperUser,
                    NormalizedName = LibraryRoles.SuperUser.ToUpper(),
                    ConcurrencyStamp = adminId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}