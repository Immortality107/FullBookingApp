using Entities;
using PaymentContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceServices : IService
    {
        private readonly ReviewDbContext _db;
        public ServiceServices(ReviewDbContext dbContext) {
        _db = dbContext;
        }
        public async Task<List<Service>> GetServices()
        {
           return _db.services.ToList();
        }

    }
}
