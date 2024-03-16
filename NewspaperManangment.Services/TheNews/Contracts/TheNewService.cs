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
        Task<GetTheNewDto?> GetToIncreaseView(int id);
    }
}
