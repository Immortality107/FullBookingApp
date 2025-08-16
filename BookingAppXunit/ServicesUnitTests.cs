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
    public class ServicesUnitTests
    {
        ReviewDbContext dbContext { get; set; }
        private readonly IService _Services;

        public ServicesUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReviewDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER2;Initial Catalog=ReviewsDbContext;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;");

            dbContext = new ReviewDbContext(optionsBuilder.Options);
            _Services = new ServiceServices(dbContext);
        }
        #region "GetServices"
        //[Fact]

        //public async void GetServices_EmptyList()
        //{
        //    //arrange
        //    dbContext.services.RemoveRange(dbContext.services);
        //    dbContext.SaveChanges();
        //    List<Service> Services = await _Services.GetServices();
        //    //Act
        //    //Assert
        //    Assert.Empty(Services);
        //}
        [Fact]
        public async Task GetServices_EmptyList()
        {
            // Arrange - use a fresh in-memory database
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh DB
                .Options;

            var dbContext = new ReviewDbContext(options);
            var servicesService = new ServiceServices(dbContext); // inject whatever your real service needs

            // Act
            List<Service> services = await servicesService.GetServices();

            // Assert
            Assert.Empty(services);
        }
        [Fact]
        public async void GetServices_ProperList()
        {
            //arrange
            List<Service> services = await _Services.GetServices();

            Assert.NotEmpty(services);
           Assert.NotNull(services);

        }
        #endregion


    }
}