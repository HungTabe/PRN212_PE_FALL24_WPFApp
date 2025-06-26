using Microsoft.EntityFrameworkCore;
using PerfumeManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeManagement.DAL.Repositories
{
    public class PerfumeInforRepository
    {
        private PerfumeStoreDbContext _context;

        public List<PerfumeInformation> GetAll()
        {
            _context = new();
            // This will get also virtual property Company to get company name
            return _context.PerfumeInformations.Include("ProductionCompany").ToList();
        }
    }
}
