using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews.Contracts
{
    public interface TheNewService
    {
        Task Add(AddTheNewDto dto);
        Task Delete(int id);
        Task<List<GetTheNewDto>?> GetMostViewd();
        Task<GetTheNewDto?> GetToIncreaseView(int id);
    }
}
