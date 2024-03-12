using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Tags
{
    public static class GetTagFilterDtoFactory
    {
        public static GetTagFilterDto Create(string? title = null)
        {
            return new GetTagFilterDto
            {
                Title = title ?? null
            };
        }
    }
}
