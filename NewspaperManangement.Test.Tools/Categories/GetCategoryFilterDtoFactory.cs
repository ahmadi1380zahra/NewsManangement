using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Categories
{
    public static class GetCategoryFilterDtoFactory
    {
        public static GetCategoryFilterDto Create(string? title = null)
        {
            return new GetCategoryFilterDto
            {
                Title = title ?? null
            };
        }
    }
}
