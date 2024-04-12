using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNewTags.Contracts
{
    public interface TheNewTagRepository:Repository
    {
        void Add(TheNewTag theNewTag);
        void Delete(TheNewTag theNewTag);
        Task<TheNewTag?> Find(int id);
    
        Task<List<GetTheNewTagDto>?> GetTags(int id);
    }
}
