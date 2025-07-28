using JockeyInsight.WebApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace JockeyInsight.WebApp.DbContext
{
    public class SimpleDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options) : base(options)
        {
        }

        public DbSet<RaceStat> RaceStats { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RaceStat>(entity =>
            {
                entity.ToTable("race_stats");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Race)
                    .HasColumnName("race_name")
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.RaceDate)
                    .HasColumnName("race_date")
                    .HasColumnType("date");

                entity.Property(e => e.RaceTime)
                    .HasColumnName("race_time");

                entity.Property(e => e.RaceCourse)
                    .HasColumnName("racecourse")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.RaceDistance)
                    .HasColumnName("race_distance");

                entity.Property(e => e.Jockey)
                    .HasColumnName("jockey")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Trainer)
                    .HasColumnName("trainer")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Horse)
                    .HasColumnName("horse")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.FinishingPosition)
                    .HasColumnName("finishing_position");

                entity.Property(e => e.DistanceBeaten)
                    .HasColumnName("distance_beaten")
                    .HasColumnType("decimal(6,2)");

                entity.Property(e => e.TimeBeaten)
                    .HasColumnName("time_beaten")
                    .HasColumnType("decimal(6,2)");
                
                entity.HasIndex(e => e.RaceDate);
                entity.HasIndex(e => e.RaceCourse);
                entity.HasIndex(e => e.Jockey);
                entity.HasIndex(e => e.Trainer);
                entity.HasIndex(e => e.Horse);
            });
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("notes");

                entity.HasKey(n => n.Id);

                entity.Property(n => n.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(n => n.Body)
                    .HasColumnName("body")
                    .IsRequired();

                entity.Property(n => n.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(n => n.UpdatedAt)
                    .HasColumnName("updated_at");

                entity.Property(n => n.RaceStatId)
                    .HasColumnName("race_stat_id");
            });
        }
    }
}