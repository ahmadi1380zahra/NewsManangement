using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Categories
{
    public static class UpdateCategoryDtoFactory
    {
        public static UpdateCategoryDto Create(string? title=null, int? rate = null)
        {
            return new UpdateCategoryDto
            {
                Title = title ?? "Updated-cat",
                Rate = rate ?? 10,
            };
        }
    }
}
