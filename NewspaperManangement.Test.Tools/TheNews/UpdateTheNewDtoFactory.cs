using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewspaperManangement.Test.Tools.TheNews
{
    public static class UpdateTheNewDtoFactory
    {
        public static UpdateTheNewDto Create(int authorId,int newsPaperCategoryId,int? rate=null)
        {
            return new UpdateTheNewDto
            {
                Title = "dummy-Title",
                Description="dimmy-Description",
                Rate= rate??1,
                AuthorId= authorId,
                NewsPaperCategoryId= newsPaperCategoryId,
            };
        }
    }
}
