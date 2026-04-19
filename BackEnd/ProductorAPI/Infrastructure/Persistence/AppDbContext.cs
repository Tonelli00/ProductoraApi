using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Audit_Log> Audit_Logs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Sector> Sectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Event
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
                entity.Property(e => e.EventDate)
                    .IsRequired();
                entity.Property(e => e.Venue)
                    .IsRequired()
                    .HasColumnType("varchar(150)");
                entity.Property(e =>e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            // Sector
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("Sector");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EventId)
                    .IsRequired();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");
                entity.Property(e => e.Capacity)
                    .IsRequired();
            });

            // Seat 
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                     .HasDefaultValueSql("NEWID()").IsRequired();
                entity.Property(e => e.SectorId)
                    .IsRequired();
                entity.Property(e => e.RowIdentifier)
                    .IsRequired()
                    .HasColumnType("varchar(10)");
                entity.Property(e => e.SeatNumber)
                    .IsRequired()
                    .HasColumnType("integer");
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnType("int");
            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasColumnType("varchar(150)");
                entity.HasIndex(u => u.Email)
                    .IsUnique();
                entity.Property(u => u.PasswordHash)
                    .IsRequired();
            });

            // Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Status)
                    .IsRequired()
                    .HasConversion<string>() // guarda "Pending" en vez de 0
                    .HasColumnType("varchar(20)");
                entity.Property(r => r.ReservedAt)
                    .IsRequired();
                entity.Property(r => r.ExpiresAt)
                    .IsRequired();
            });

            // AuditLog
            modelBuilder.Entity<Audit_Log>(entity =>
            {
                entity.ToTable("AuditLog");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Action)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasColumnType("varchar(50)");
                entity.Property(a => a.EntityType)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
                entity.Property(a => a.EntityId)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
                // JSON con metadatos del evento
                entity.Property(a => a.Details)
                    .HasColumnType("nvarchar(max)");
                entity.Property(a => a.CreatedAt)
                    .IsRequired();
            });

            // Relationships

            // uno a muchos - User Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // uno a muchos - Seat Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany()
                .HasForeignKey(r => r.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // uno a muchos - User AuditLog (nullable - puede ser sistema)
            modelBuilder.Entity<Audit_Log>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // uno a muchos - Event Sector
            modelBuilder.Entity<Sector>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Sectors)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // uno a muchos - Sector Seat
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Sector)
                .WithMany(sec => sec.Seats)
                .HasForeignKey(s => s.SectorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
