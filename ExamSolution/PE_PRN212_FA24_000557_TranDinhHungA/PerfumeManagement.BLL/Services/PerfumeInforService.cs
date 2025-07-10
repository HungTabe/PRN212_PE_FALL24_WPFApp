using Microsoft.Extensions.Logging.Abstractions;
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

        // Function to hanlde Search use case of User at BLL
        public List<PerfumeInformation> GetProductBySearchTerms(string ingredient, string concentration)
        {
            // Call to function search on DB through repo
            return _repo.GetProductBySearchTerms(ingredient, concentration);
        }

        // Function Add new
        public void AddPerfume(PerfumeInformation newPerfume)
        {
            _repo.AddPerfume(newPerfume);
        }

        // Function Update
        public void UpdatePerfume(PerfumeInformation updatePerfume)
        {
            _repo.UpdatePerfume(updatePerfume);
        }
    }
}
