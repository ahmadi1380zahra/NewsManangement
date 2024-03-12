using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Categories
{
    public static class AddCategoryDtoFactory
    {
        public static AddCategoryDto Create(int? dummyRate=null)
        {
            return new AddCategoryDto
            {
                Title="dummy-title",
                Rate=dummyRate?? 20,
            };
        }
    }
}
