using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Kavehnegar.External.Infrastructure
{
    public class KavehnegarDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<User> Users { get; set; }
        public KavehnegarDbContext(DbContextOptions<KavehnegarDbContext> options) : base(options) 
        { 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogPostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
        {
            public void Configure(EntityTypeBuilder<BlogPost> builder)
            {
                //Shadow property to implement optimistic concurrency control
                builder.Property<byte[]>("RowVersion")
                    .IsRowVersion()
                    .IsConcurrencyToken();
                builder.HasKey(x => x.Id);
                builder.Property(p => p.Id).HasConversion(informationId => informationId.Value, dbId => new BlogPostId(dbId));
                builder.OwnsOne(x => x.Title)
                    .Property(x => x.Value).HasColumnName("Title");
                builder.OwnsOne(x => x.Description)
                    .Property(x => x.Value).HasColumnName("Description");
                builder.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.authorId);

            }
        }

        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                //Shadow property to implement optimistic concurrency control
                builder.Property<byte[]>("RowVersion")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                builder.HasKey(x => x.Id);
                builder.Property(p => p.Id).HasConversion(informationId => informationId.Value, dbId => new UserId(dbId));
                builder.OwnsOne(x => x.username)
                    .Property(x => x.Value).HasColumnName("Username");
            }
        }
    }
    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<KavehnegarDbContext>();

                if (!context.Database.EnsureCreated())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
