using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews
{
    public class TheNewAppService:TheNewService
    {
        private readonly TheNewRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public TheNewAppService(TheNewRepository repository
            , UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork=unitOfWork;
        }

        public async Task Add(AddTheNewDto dto)
        {
            var theNew = new TheNew
            {
                Title=dto.Title,
                Description=dto.Description,
                Rate=dto.Rate,
                NewspaperCategoryId=dto.NewsPaperCategoryId,
                AuthorId=dto.AuthorId,
                View=0,

            };
            foreach (var tagId in dto.TagsId)
            {
                theNew.TheNewTags.Add(new TheNewTag
                {
                    TagId=tagId
                });
            }
            _repository.Add(theNew);
            await _unitOfWork.Complete();

        }
    }
}
