using PerfumeManagement.DAL.Entities;
using PerfumeManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeManagement.BLL.Services
{
    public class ProductionComService
    {
        private ProductionComRepository _repo = new();

        public List<ProductionCompany> GetAllCompanies()
        {
            return _repo.GetAllCompanies();
        }
    }
}
