using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Category
{
    public interface ICategoriesRepository :IBaseRepository<Categories>
    {
        Task<Categories> GetCategoriesBySlug(string slug);
    }
}
