using Entities;
using Microsoft.EntityFrameworkCore;
using PaymentContracts;
using PaymentContracts.DTOs;
using Services;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
namespace BookingAppXunit
{
    public class PaymentsUnitTests
    {
        ReviewDbContext dbContext { get; set; }
        private readonly IPay _PayService;

        public PaymentsUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReviewDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER2;Initial Catalog=ReviewsDbContext;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;");

            dbContext = new ReviewDbContext(optionsBuilder.Options);
            _PayService = new PayService(dbContext);
        }
        #region "GetAllPayments"
        [Fact]
        public async Task GetAllPayments_EmptyList()
        {
            // Arrange - use a fresh in-memory database
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh DB
                .Options;

            var dbContext = new ReviewDbContext(options);
            var payService = new PayService(dbContext); // inject whatever your real service needs

            // Act
            List<PaymentDTO> Payments = await payService.GetAllPayments();

            // Assert
            Assert.Empty(Payments);
        }
        [Fact]
        public async void GetAllPayments_ProperList()
        {
            //arrange
            List<PaymentDTO> payments = await _PayService.GetAllPayments();

            Assert.NotEmpty(payments);
           Assert.NotNull(payments);

        }
        #endregion

        #region "GetInternationalPayments"
        [Fact]
        public async Task GetInternationalPayments_EmptyList()
        {
            // Arrange - use a fresh in-memory database
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh DB
                .Options;

            var dbContext = new ReviewDbContext(options);
            var payService = new PayService(dbContext); // inject whatever your real service needs

            // Act
            List<PaymentDTO> Payments = await payService.GetInternationalPayments();

            // Assert
            Assert.Empty(Payments);
        }
        [Fact]
        public async void GetInternationalPayments_ProperList()
        {
            //arrange
            List<PaymentDTO> payments = await _PayService.GetInternationalPayments();
            Assert.NotEmpty(payments);
            Assert.NotNull(payments);

        }
        #endregion

        #region "GetLocalPayments"
        [Fact]
        public async Task GetLocalPayments_EmptyList()
        {
            // Arrange - use a fresh in-memory database
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh DB
                .Options;

            var dbContext = new ReviewDbContext(options);
            var payService = new PayService(dbContext); // inject whatever your real service needs

            // Act
            List<PaymentDTO> Payments = await payService.GetLocalPayments();

            // Assert
            Assert.Empty(Payments);
        }
        [Fact]
        public async void GetLocalPayments_ProperList()
        {
            //arrange
            List<PaymentDTO> payments = await _PayService.GetLocalPayments();

            Assert.NotEmpty(payments);
            Assert.NotNull(payments);

        }
        #endregion


    }
}