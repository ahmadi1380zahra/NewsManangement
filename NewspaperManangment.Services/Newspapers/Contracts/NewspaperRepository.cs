using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts
{
    public interface NewspaperRepository
    {
        void Add(Newspaper newspaper);
        void Delete(Newspaper newspaper);
        Task<Newspaper?> Find(int id);
        Task<List<GetNewspaperDto>?> GetAll(GetNewspaperFilterDto? dto);
        Task<bool> IsDuplicateTitle(string title);
        Task<bool> IsDuplicateTitleExceptiItSelf(int id, string title);
        Task<bool> IsExist(int id);
        Task<bool> IsPublish(int id);
        void Update(Newspaper newspaper);
    }
}
