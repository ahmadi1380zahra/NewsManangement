using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts
{
    public interface AuthorService: Service
    {
        Task Add(AddAuthorDto dto);
        Task Delete(int id);
        Task<GetAuthorsDto?> Get(int id);
        Task<List<GetAuthorsDto>?> GetAll(GetAuthorsFilterDto? dto);
        Task<List<GetAuthorsDto>?> GetHighestNewsCount();
        Task<List<GetAuthorsDto>?> GetMostViewed();
        Task Update(int id, UpdateAuthorDto dto);
    }
}
