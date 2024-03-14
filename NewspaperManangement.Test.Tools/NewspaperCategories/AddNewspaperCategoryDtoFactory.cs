using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.NewspaperCategories
{
    public static class AddNewspaperCategoryDtoFactory
    {
        public static AddNewspaperCategoryDto Create(int categoryId,int newspaperId)
        {
            return new AddNewspaperCategoryDto
            {
                newspaperId= newspaperId,
                categoryId=categoryId
            };
        }
    }
}
