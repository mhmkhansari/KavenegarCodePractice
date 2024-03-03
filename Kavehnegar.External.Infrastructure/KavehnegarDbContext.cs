using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Kavehnegar.External.Infrastructure
{
    public class KavehnegarDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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

            }
        }
    }
}
