using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ReviewDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet <PaymentMethods> payment { get; set; }

        public DbSet<Appointments> Appointments { get; set; }

        public DbSet<RegisteredAccounts> RegisteredAccount { get; set; }

        public ReviewDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<PaymentMethods>().ToTable("Payments");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<RegisteredAccounts>().ToTable("RegisteredAccount");
            string? stReviews = System.IO.File.ReadAllText("Reviews.json");
            List<Review>? Reviews = System.Text.Json.JsonSerializer.Deserialize<List<Review>>(stReviews);
            if (Reviews != null)
            {
                foreach (Review c in Reviews)
                {
                    modelBuilder.Entity<Review>().HasData(c);
                }
            }
            string? stBookings = System.IO.File.ReadAllText("Bookings.json");
            List<Booking>? Bookings = System.Text.Json.JsonSerializer.Deserialize<List<Booking>>(stBookings);
            if (Bookings != null)
            {
                foreach (Booking B in Bookings)
                {
                    modelBuilder.Entity<Booking>().HasData(B);
                }
            }
            string? StPayments = System.IO.File.ReadAllText("Payments.json");
            List<PaymentMethods>? Payments = System.Text.Json.JsonSerializer.Deserialize<List<PaymentMethods>>(StPayments);
            if (Payments != null)
            {
                foreach(PaymentMethods Payment in Payments)
                {
                    modelBuilder.Entity<PaymentMethods>().HasData(Payment);
                }
            }

            string? stClients = System.IO.File.ReadAllText("Clients.json");
            List<Client> Clients = System.Text.Json.JsonSerializer.Deserialize<List<Client>>(stClients);
            if (Clients != null)
            {
                modelBuilder.Entity<Client>().HasData(Clients);
            }

            string? stServices = System.IO.File.ReadAllText("Services.json");
            List<Service> Services = System.Text.Json.JsonSerializer.Deserialize<List<Service>>(stServices);
           if (Services != null)
            {
                modelBuilder.Entity<Service>().HasData(Services);
            }
            modelBuilder.Entity<RegisteredAccounts>()
            .HasIndex(a => a.Email)
            .IsUnique();
            //modelBuilder.Entity<Booking>().Property(temp => temp.TIN).
            //    HasColumnName("TaxIdentificationNumber").HasColumnType("varchar(8)").HasDefaultValue("ABDD1234");
        }

        public List<Review> sp_GetAllReviews()
        {
            return Reviews.FromSqlRaw("Execute[dbo].[GetAllReviews]").ToList();

        }

        public int sp_InsertReview(Review review)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ReviewID", review.ReviewId),
                new SqlParameter("@BookingID", review.BookingID),
                new SqlParameter("@ReviewMessage", review.ReviewMessage)

            };
            return Database.ExecuteSqlRaw("EXECUTE [dbo].[SP_InsertReview] @ReviewID,@BookingID,@ReviewMessage", parameters);
        }


    }
}


