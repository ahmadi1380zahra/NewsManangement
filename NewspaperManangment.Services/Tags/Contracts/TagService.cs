using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Tags.Contracts
{
    public interface TagService
    {
        Task Add(AddTagDto dto);
        Task Delete(int id);
        Task<List<GetTagDto>?> GetAll(GetTagFilterDto? dto);
        Task Update(int id,UpdateTagDto dto);
    }
}
