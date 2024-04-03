using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Exceptions;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Exceptions;
using NewspaperManangment.Services.TheNewTags.Contracts;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using NewspaperManangment.Services.TheNewTags.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNewTags
{
    public class TheNewTagAppService : TheNewTagService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TheNewTagRepository _repository;
        private readonly TagRepository _tagRepository;
        private readonly TheNewRepository _theNewRepository;

        private readonly NewspaperCategoryRepository _newspaperCategoryRepository;
        public TheNewTagAppService(TheNewTagRepository repository
            , UnitOfWork unitOfWork
            , TagRepository tagRepository
            , TheNewRepository theNewRepository

            , NewspaperCategoryRepository newspaperCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _tagRepository = tagRepository;
            _theNewRepository = theNewRepository;

            _newspaperCategoryRepository = newspaperCategoryRepository;
        }

        public async Task Add(AddTheNewTagDto dto)
        {
            if (!await _tagRepository.IsExist(dto.TagId))
            {
                throw new TagIsNotExistException();
            }
            if (!await _theNewRepository.IsExist(dto.TheNewId))
            {
                throw new TheNewIsNotExistedException();
            }
            var theNew = await _theNewRepository.Find(dto.TheNewId);
            var newsPaperCategory = await _newspaperCategoryRepository.Find(theNew!.NewspaperCategoryId);
            var tag = await _tagRepository.Find(dto.TagId);
            if (tag!.CategoryId != newsPaperCategory!.CategoryId)
            {
                throw new ThisTagIsNotAllowedForThisNewspaperCategoryException();
            }
            var theNewTag = new TheNewTag
            {
                TagId = dto.TagId,
                TheNewId = dto.TheNewId,
            };
            _repository.Add(theNewTag);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var theNewTag = await _repository.Find(id);
            if (theNewTag == null)
            {
                throw new TheNewTagIsNotExistException();
            }
            _repository.Delete(theNewTag);
            await _unitOfWork.Complete();
        }
    }
}
