using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Newspapers
{
    public static class AddNewsPaperDtoFactory
    {
        public static AddNewsPaperDto Create(int categoryId1
            , int categoryId2
            , int categoryId3)
        {
            return new AddNewsPaperDto
            {
                Title = "طلوع",
                CategoriesId = new List<int>
             {
             categoryId1,
            categoryId2,
            categoryId3
              }
            };
        }

    }
}
