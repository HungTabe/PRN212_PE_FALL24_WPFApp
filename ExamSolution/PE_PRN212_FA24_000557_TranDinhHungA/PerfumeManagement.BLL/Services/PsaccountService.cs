using PerfumeManagement.DAL.Entities;
using PerfumeManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeManagement.BLL.Services
{
    public class PsaccountService
    {
        private PsaccountRepository _repo = new();

        public async Task<Psaccount> Login (string email, string password)
        {
            var user = await _repo.Login(email, password);

            if (user !=  null)
            {
                return user;
            }
            return null;
        }
    }
}
