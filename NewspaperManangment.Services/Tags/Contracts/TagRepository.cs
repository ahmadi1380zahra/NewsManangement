using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Tags.Contracts
{
    public interface TagRepository:Repository
    {
        void Add(Tag tag);
        void Delete(Tag tag);
        Task<Tag?> Find(int id);
        Task<List<GetTagDto>?> GetAll(GetTagFilterDto? dto);
        Task<bool> IsExist(int id);
        void Update(Tag tag);
    }
}
