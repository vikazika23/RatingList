using Media.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Media.DataAccess.Context
{
    public partial class RatingContext : DbContext
    {
        public RatingContext()
        {
        }

        public RatingContext(DbContextOptions<RatingContext> options) : base(options)
        {
        }

        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Viewer> Viewer { get; set; }
        public virtual DbSet<Critic> Critic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>(entity =>
                {
                    entity.Property(a => a.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                    entity.Property(a => a.Name).IsRequired();
                    entity.Property(a => a.Address).IsRequired();
                });

            modelBuilder.Entity<Viewer>(entity =>
                {
                    entity.Property(t => t.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                    entity.Property(t => t.Title).IsRequired();
                    entity.Property(t => t.Author).IsRequired();
                    entity.Property((t => t.Duration)).IsRequired();
                    entity.HasOne(t => t.Rating)
                        .WithMany(a => a.Viewer)
                        .HasForeignKey(t => t.RatingId)
                        .HasConstraintName("FK_Viewer_Rating");
                });

            modelBuilder.Entity<Critic>(entity =>
                {
                    entity.Property(p => p.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                    entity.Property(p => p.Title).IsRequired();
                    entity.Property(p => p.Author).IsRequired();
                    entity.Property((p => p.Duration)).IsRequired();
                    entity.HasOne(p => p.Rating)
                        .WithMany(a => a.Critic)
                        .HasForeignKey(p => p.RatingId)
                        .HasConstraintName("FK_Critic_Rating");
                });
            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}