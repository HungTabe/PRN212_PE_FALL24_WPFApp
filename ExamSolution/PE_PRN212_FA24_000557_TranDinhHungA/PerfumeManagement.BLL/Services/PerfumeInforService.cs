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
        private PerfumeInforRepository _repo = new();

        public List<PerfumeInformation> GetAllPerfumes()
        {
            return _repo.GetAll();
        }

        public List<PerfumeInformation> GetPerfumesByConditions(string ingre, string concen)
        {
            return _repo.GetProductsByIngredientsOrConcentration(ingre, concen);
        }

        public bool DeletePerfume(PerfumeInformation perfumeInformation)
        {
            _repo.Delete(perfumeInformation);
            return true;
        }

        public void AddPerfume(PerfumeInformation perfume)
        {
            _repo.AddPerfume(perfume);
        }

        public void UpdatePerfume(PerfumeInformation perfume)
        {
            _repo.UpdatePerfume(perfume);
        }
    }
}
