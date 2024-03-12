using NewspaperManangment.Entities;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Tags
{
    public static class AddTagDtoFactory
    {
        public static AddTagDto Create(int categoryId)
        {
            return new AddTagDto
            {
                Title = "dummy-title",
                CategoryId = categoryId
            };
        }
    }
}
