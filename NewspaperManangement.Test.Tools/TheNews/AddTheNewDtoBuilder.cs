using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNews
{
    public class AddTheNewDtoBuilder
    {
        private readonly AddTheNewDto _addTheNewDto;
        public AddTheNewDtoBuilder(int authorId, int newsPaperCategoryId)
        {
            _addTheNewDto = new AddTheNewDto
            {
                Title = "پرسپولیس در لیگ",
                Description = "پرسپولیس قهرمان لیگ شد",
                Rate = 10,
                AuthorId = authorId,
                NewsPaperCategoryId = newsPaperCategoryId,
                TagsId = new()

            };
        }
        public AddTheNewDtoBuilder WithTagId(int tagId)
        {
            _addTheNewDto.TagsId.Add(tagId);
            return this;
        }
        public AddTheNewDtoBuilder WithRate(int rate)
        {
            _addTheNewDto.Rate = rate;
            return this;
        }
        public AddTheNewDto Build()
        {
            return _addTheNewDto;
        }
    }
}
