using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.NewspaperCategories
{
    public class NewspaperCategoryBuilder
    {
        private readonly NewspaperCategory _newspaperCategory;
        public NewspaperCategoryBuilder(int categoryId, int newspaperId)
        {
            _newspaperCategory = new NewspaperCategory
            {
                CategoryId = categoryId,
                NewspaperId = newspaperId
            };
            
        }
        public NewspaperCategory Build()
        {
            return _newspaperCategory;
        }
    }

 
}
