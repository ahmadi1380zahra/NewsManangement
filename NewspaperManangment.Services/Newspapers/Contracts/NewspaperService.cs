using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts
{
    public interface NewspaperService
    {
        Task Add(AddNewsPaperDto dto);
        Task Delete(int id);
       Task<List<GetNewspaperDto>?> GetAll(GetNewspaperFilterDto? dto);
        Task Update(int id, UpdateNewsPaperDto dto);
    }
}
