using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Authors
{
    public static class AddAuthorDtoFactory
    {
        public static AddAuthorDto Create()
        {
            return new AddAuthorDto
            {
                FullName = "زهرااحمدی",
            };
        }
    }
}
