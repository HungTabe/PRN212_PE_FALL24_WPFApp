using Microsoft.IdentityModel.Tokens;
using PerfumeManagement.DAL.Entities;
using PerfumeManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeManagement.BLL.Services
{
    public class PerfumeInforService
    {
        // Because not setup context at construction so we can do this 
        private PerfumeInforRepository _repo = new();

        public List<PerfumeInformation> GetAllPerfumes()
        {
            return _repo.GetAll();
        }

        public List<PerfumeInformation> GetPerfumesByConditions(string ingre, string concen)
        {
            return _repo.GetProductsByIngredientsOrConcentration(ingre, concen);
        }
    }
}
