using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Authors
{
    public class UpdateAuthorDtoFactory
    {
        public static UpdateAuthorDto Create(string? fullName=null)
        {
            return new UpdateAuthorDto
            {
                FullName = fullName?? "dummy-name",
            };
        }
    }
}
