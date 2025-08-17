using Entities;
using Microsoft.EntityFrameworkCore;
using PaymentContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginService : ILogin
    {
        private ReviewDbContext _db;

        public LoginService(ReviewDbContext db )
        {
            _db = db;
        }

        public async Task<bool> AccountFound(LoginDTO loginDTO)
        {
            RegisteredAccounts AccountToCheck= loginDTO.ToRegisteredAccount();
          RegisteredAccounts? Found=  _db.RegisteredAccount.FirstOrDefault(r=> r.Email==AccountToCheck.Email&& r.HashedPassword == AccountToCheck.HashedPassword);
            return  Found != null;
        }

        public async Task<Guid> AddAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty( loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
                {
                    throw new ArgumentNullException("Please Write Valid Email & Password");
                }
                List<LoginDTO> AllRegisteredAccounts =  GetAllRegisteredAccounts().Result;
                LoginDTO SameEmailAccount=   AllRegisteredAccounts.FirstOrDefault(a=> a.Email == loginDTO.Email);

                if (SameEmailAccount != null)
                {
                    throw new DuplicateWaitObjectException("Email ALready Exists, Please Add New Email");
                }
                
                RegisteredAccounts AccountAdded = _db.RegisteredAccount.Add(loginDTO.ToRegisteredAccount()).Entity;
                await _db.SaveChangesAsync();
                return  AccountAdded.AccountID;
            }

        public Task<bool> DeleteAccount(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public  async Task<List<LoginDTO>> GetAllRegisteredAccounts()
        {
            return _db.RegisteredAccount.Select( r=> r.ToLoginDTO()).ToList();
        }

        public Task<bool> UpdateAccount(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
