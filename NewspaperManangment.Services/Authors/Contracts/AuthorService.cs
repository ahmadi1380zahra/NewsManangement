using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts
{
    public interface AuthorService
    {
        Task Add(AddAuthorDto dto);
    }
}
