using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNewTags.Contracts
{
    public interface TheNewTagRepository
    {
        void Add(TheNewTag theNewTag);
        void Delete(TheNewTag theNewTag);
        Task<TheNewTag?> Find(int id);
    }
}
