using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
                entity.ToTable("EVENT");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
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

                entity.HasData(new Event 
                {
                    Id=1,
                    Name="Gran evento",
                    EventDate=DateTime.Now.AddDays(20),
                    Venue="Estadio A",
                    Status="Activo"
                });              
            });

            // Sector
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("SECTOR");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
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

            entity.HasData(new Sector{Id=1,EventId=1,Name="Sector A",Price=2000,Capacity=50 });
            entity.HasData(new Sector {Id =2, EventId = 1, Name = "Sector B", Price = 2500, Capacity = 50 });
            });

            // Seat 
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("SEAT");
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
                    .HasColumnType("int")
                    .IsConcurrencyToken();
                

                var seats = new List<Seat>();
                int id = 1;

                // Sector 1 (50 butacas)
                for (int i = 1; i <= 50; i++)
                {
                    seats.Add(new Seat
                    {
                        Id = Guid.Parse($"11111111-1111-1111-1111-{i.ToString("D12")}"),
                        SectorId = 1,
                        RowIdentifier = "Sector 1",
                        SeatNumber = i,
                        Status = "Available",
                        Version = 0
                    });
                }

                // Sector 2 (50 butacas)
                for (int i = 1; i <= 50; i++)
                {
                    seats.Add(new Seat
                    {
                        Id = Guid.Parse($"22222222-2222-2222-2222-{i.ToString("D12")}"),
                        SectorId = 2,
                        RowIdentifier = "Sector 2",
                        SeatNumber = i,
                        Status = "Available",
                        Version = 0
                    });
                }

                modelBuilder.Entity<Seat>().HasData(seats);

            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");
                entity.HasKey(u => u.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
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


                entity.HasData(new User { Id = 1, Name = "Proyecto", Email = "Proyecto2026@gmail.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Proyecto123") });

            });

            // Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATION");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id)
                    .HasDefaultValueSql("NEWID()").IsRequired();
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
                entity.ToTable("AUDIT_LOG");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id)
                      .HasDefaultValueSql("NEWID()").IsRequired();
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
                .OnDelete(DeleteBehavior.Cascade);

            // uno a muchos - Seat Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany(re=>re.Reservations)
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
