using Microsoft.EntityFrameworkCore;
using PerfumeManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeManagement.DAL.Repositories
{
    public class PsaccountRepository
    {
        private PerfumeStoreDbContext _context;

        public async Task<Psaccount> Login (string email, string password)
        {
            _context = new();
            return await _context.Psaccounts.FirstOrDefaultAsync(x => x.Password == password && x.EmailAddress == email);
        }
    }
}
