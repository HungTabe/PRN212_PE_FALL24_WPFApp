using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public List<PerfumeInformation> GetProductsByIngredientsOrConcentration(string ingredients, string concentration)
        {
            _context = new();

            // Case 1 : Only have Concen
            if (ingredients.IsNullOrEmpty() && !concentration.IsNullOrEmpty())
            {
                return _context.PerfumeInformations.Where(p => p.Concentration.ToLower().Contains(concentration.ToLower())).Include(p => p.ProductionCompany).ToList();
            }

            // Case 2 : Only have ingre
            if (concentration.IsNullOrEmpty() && !ingredients.IsNullOrEmpty())
            {
                return _context.PerfumeInformations.Where(p => p.Ingredients.ToLower().Contains(ingredients.ToLower())).Include(p => p.ProductionCompany).ToList();

            }

            // Case 3 : Have bold
            if (!ingredients.IsNullOrEmpty() && !concentration.IsNullOrEmpty())
            {
                return _context.PerfumeInformations.Where(p => p.Ingredients.ToLower().Contains(ingredients.ToLower()) && p.Concentration.ToLower().Contains(concentration.ToLower())).Include(p => p.ProductionCompany).ToList();

            }

            return _context.PerfumeInformations.Where(p => p.Ingredients.ToLower().Contains(ingredients.ToLower()) || p.Concentration.ToLower().Contains(concentration.ToLower())).Include(p => p.ProductionCompany).ToList();
        }
    }
}
