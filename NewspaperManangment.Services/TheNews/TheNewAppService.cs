using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Exceptions;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Exceptions;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Exceptions;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews
{
    public class TheNewAppService : TheNewService
    {
        private readonly TheNewRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly AuthorRepository _authorRepository;
        private readonly TagRepository _tagRepository;
        private readonly NewspaperCategoryRepository _newspaperCategoryRepository;
        public TheNewAppService(TheNewRepository repository
            , UnitOfWork unitOfWork
            , AuthorRepository authorRepository
            , NewspaperCategoryRepository newspaperCategoryRepository
            , TagRepository tagRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _newspaperCategoryRepository = newspaperCategoryRepository;
            _tagRepository = tagRepository;
        }

        public async Task Add(AddTheNewDto dto)
        {
            if (!await _authorRepository.IsExist(dto.AuthorId))
            {
                throw new AuthorIsNotExistException();
            }
            if (dto.Rate <=0)
            {
                throw new NewsRateShouldBeMoreThanZeroException();
            }
            var newsPaperCategory = await _newspaperCategoryRepository.Find(dto.NewsPaperCategoryId);
            if (newsPaperCategory == null)
            {
                throw new NewspaperCategoryIsNotExistException();
            }
            var allowedCategoryId = newsPaperCategory.CategoryId;
            var theNew = new TheNew
            {
                Title = dto.Title,
                Description = dto.Description,
                Rate = dto.Rate,
                NewspaperCategoryId = dto.NewsPaperCategoryId,
                AuthorId = dto.AuthorId,
                View = 0,

            };


            foreach (var tagId in dto.TagsId)
            {
                var tag = await _tagRepository.Find(tagId);
                if (tag is null)
                {
                    throw new TagIsNotExistException();
                }

                if (tag.CategoryId != allowedCategoryId)
                {
                    throw new ThisTagIsNotAllowedForThisNewspaperCategoryException();

                }
                theNew.TheNewTags.Add(new TheNewTag
                {
                    TagId = tagId
                });
            }
            _repository.Add(theNew);
            await _unitOfWork.Complete();

        }
    }
}
