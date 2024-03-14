using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Newspapers
{
    public static class UpdateNewsPaperDtoFactory
    {
        public static UpdateNewsPaperDto Create(string? title=null)
        {
            return new UpdateNewsPaperDto
            {
                Title = title ?? "dummy-title"
            };
        }
    }
}
