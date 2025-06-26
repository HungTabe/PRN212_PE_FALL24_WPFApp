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

        // Function hanle search by term on Product Database
        public List<PerfumeInformation> GetProductBySearchTerms(string ingredient, string concentration)
        {
            // Contruct context obj to work with db
            _context = new();

            //return _context.PerfumeInformations

            // Where (s => s.) : Tìm kiếm trên DB theo điều kiện từ lamda (nếu đk từ lamda trả true thì where sẽ lấy
            // bản ghi còn false thì where ko lấy)

            // Case 1 : ingre not null , concen null
            if (!ingredient.IsNullOrEmpty() && concentration.IsNullOrEmpty())
            {
                // Vidu 1 bản ghi bất kì : s.Ingredients = "Coffe Vanila" => ToLower() = "coffe vanila"
                // Contains(ingredient.ToLower()) : User nó search "coffee" ) : Contain sẽ check từ s.Ingredients ra nếu 
                // s.Ingredients có "coffee" thì nó sẽ lấy bản ghi đó ko thì thôi
                return _context.PerfumeInformations.Where(s => s.Ingredients.ToLower().Contains(ingredient.ToLower())).Include("ProductionCompany").ToList();
            }

            // Case 2 : ingre null , concen not null
            if (ingredient.IsNullOrEmpty() && !concentration.IsNullOrEmpty())
            {
                return _context.PerfumeInformations.Where(s => s.Concentration.ToLower().Contains(concentration.ToLower())).Include("ProductionCompany").ToList();
            }

            // Case 3 : both not null
            // both not null ~ both have data
            // when ingredient have value then ingredient.IsNullOrEmpty() => false
            // when concentration have value then concentration.IsNullOrEmpty() => false
            // (!flase && !false) trả về true để kích hoạt logic trong if
            if (!ingredient.IsNullOrEmpty() && !concentration.IsNullOrEmpty())
            {
                // Muốn kích hoạt if thì đk phải trả true
                return _context.PerfumeInformations.Where(s => s.Concentration.ToLower().Contains(concentration.ToLower()) && s.Ingredients.ToLower().Contains(ingredient.ToLower())).Include("ProductionCompany").ToList();
            }

            // Case 4 : both null them GUI will show MessageBox need to fill search terms
            return null;
        }
    }
}
