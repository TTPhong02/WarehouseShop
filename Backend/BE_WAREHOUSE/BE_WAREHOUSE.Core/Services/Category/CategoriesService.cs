using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Category;
using BE_WAREHOUSE.Core.Resources;
using BE_WAREHOUSE.Core.Services.Base;
using MISA.AMISDemo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Category
{
    public class CategoriesService : BaseService<Categories>,ICategoriesService
    {
        public CategoriesService(IBaseRepository<Categories> repository) : base(repository)
        {
        }
        public override async Task ValidateBeforeInsert(Categories categories)
        {
            if (categories.CategoriesCode == "" || categories.CategoriesCode == null)
            {
                throw new MISAValidateException(CategoriesResource.CategoriesCodeNotEmpty);
            }
            if (categories.CategoriesName == "" || categories.CategoriesName == null)
            {
                throw new MISAValidateException(CategoriesResource.CategoriesNameNotEmpty);
            }

            await Task.CompletedTask;
        }
    }
}
