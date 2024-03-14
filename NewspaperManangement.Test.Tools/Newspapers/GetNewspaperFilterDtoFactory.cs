using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Newspapers
{
    public static class GetNewspaperFilterDtoFactory
    {
        public static GetNewspaperFilterDto Create(string? title=null)
        {
            return new GetNewspaperFilterDto()
            {
                Title = title?? null
            };
        }
    }
}
