using PerfumeManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeManagement.DAL.Repositories
{
    public class ProductionComRepository
    {

        private PerfumeStoreDbContext _context;

        public List<ProductionCompany> GetAllCompanies()
        {
            _context = new();
            return _context.ProductionCompanies.ToList();
        }

    }
}
