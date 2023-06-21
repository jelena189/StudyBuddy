using Microsoft.EntityFrameworkCore;
using StudyBuddy.Repositories.Entities;

namespace StudyBuddy.Repositories
{
    public class StudyBuddyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Class> Classes { get; set; }


        public StudyBuddyContext(DbContextOptions<StudyBuddyContext> options)
        : base(options)
        {
        }

        public StudyBuddyContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<User>()
                .HasIndex(i => i.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(i => i.Username).IsUnique();

            modelBuilder.Entity<User>()
               .HasMany(u => u.Comments)
               .WithOne(c => c.Author)
               .HasForeignKey(x => x.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasIndex(i => i.RoleName).IsUnique();

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Users)
                .WithOne(u => u.School)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<School>()
               .HasMany(s => s.Users)
               .WithOne(u => u.School)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Class>()
              .HasMany(c => c.Posts)
              .WithOne(p => p.Class)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
             .HasMany(p => p.Comments)
             .WithOne(c => c.Post)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
