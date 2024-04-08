using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers.Contracts
{
    public interface NewspaperService:Service
    {
        Task Add(AddNewsPaperDto dto);
        Task Delete(int id);
        Task<List<GetNewspaperDto>?> GetAll(GetNewspaperFilterDto? dto);
        Task Publish(int id);
        Task Update(int id, UpdateNewsPaperDto dto);
    }
}
