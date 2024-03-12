using NewspaperManangment.Entities;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Tags.Contracts
{
    public interface TagRepository
    {
        void Add(Tag tag);
        void Delete(Tag tag);
        Task<Tag?> Find(int id);
        Task<List<GetTagDto>?> GetAll(GetTagFilterDto? dto);
        void Update(Tag tag);
    }
}
