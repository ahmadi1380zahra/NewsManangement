using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNews
{
    public static class GetTheNewFilterDtoFactory
    {
        public static GetTheNewFilterDto Create(string? title=null)
        {
            return new GetTheNewFilterDto
            {
                Title=title??null
            };
        }
    }
}
