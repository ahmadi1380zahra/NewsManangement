using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNewTags.Contracts
{
    public interface TheNewTagService
    {
        Task Add(AddTheNewTagDto dto);
        Task Delete(int id);
        Task<List<GetTheNewTagDto>?> GetTags(int theNewId);
    }
}
