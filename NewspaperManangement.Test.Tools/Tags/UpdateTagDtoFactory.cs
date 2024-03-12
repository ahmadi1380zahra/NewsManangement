using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Tags
{
    public static class UpdateTagDtoFactory
    {
        public static UpdateTagDto Create(int categoryId,string? title=null)
        {
            return new UpdateTagDto
            {
                Title=title ?? "update-title",
                CategoryId=categoryId,
            };
        }
    }
}
