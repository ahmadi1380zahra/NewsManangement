using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Exceptions;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using NewspaperManangment.Services.Newspapers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Newspapers
{
    public class NewspaperAppService : NewspaperService
    {
        private readonly NewspaperRepository _repository;
        private readonly CategoryRepository _categoryRepository;
        private readonly NewspaperCategoryRepository _newspaperCategoryRepository;
        private readonly UnitOfWork _unitOfWork;
        public NewspaperAppService(NewspaperRepository repository
                                  , UnitOfWork unitOfWork
                                  , CategoryRepository categoryRepository
                                  , NewspaperCategoryRepository newspaperUategoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _newspaperCategoryRepository = newspaperUategoryRepository;
        }

        public async Task Add(AddNewsPaperDto dto)
        {
            if (await _repository.IsDuplicateTitle(dto.Title))
            {
                throw new NewspaperShouldHaveUniqueNameException();
            }
            var newspaper = new Newspaper
            {
                Title = dto.Title,
            };
            foreach (var categoryId in dto.CategoriesId)
            {
                if (!await _categoryRepository.IsExist(categoryId))
                {
                    throw new CategoryIsNotExistException();
                }
                if (newspaper.NewspaperCategories.Any(_ => _.CategoryId == categoryId))
                {
                    throw new CategoryIsReduplicateForThisNewspaperException();
                }
                newspaper.NewspaperCategories.Add(new NewspaperCategory
                {
                    CategoryId = categoryId,
                });
            }
            _repository.Add(newspaper);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var newspaper = await _repository.Find(id);
            if (newspaper == null)
            {
                throw new NewspaperIsNotExistException();
            }
            if (newspaper.PublishDate!=null)
            {
                throw new NewspaperHasBeenPublishedYouCantDeleteIt();
            }
            if (await _newspaperCategoryRepository.IsExistCategoryForThisNewspaper( id))
            {
                _newspaperCategoryRepository.DeleteCategoryForThisNewspaper(id);
            }
            _repository.Delete(newspaper);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetNewspaperDto>?> GetAll(GetNewspaperFilterDto? dto)
        {
            return await _repository.GetAll(dto);
        }

        public async Task Update(int id, UpdateNewsPaperDto dto)
        {
            var newspaper = await _repository.Find(id);
            if (newspaper == null)
            {
                throw new NewspaperIsNotExistException();
            }
            if (await _repository.IsDuplicateTitleExceptiItSelf(newspaper.Id, dto.Title))
            {
                throw new NewspaperShouldHaveUniqueNameException();
            }

            if (newspaper.PublishDate != null)
            {
                throw new NewspaperHasBeenPublishedYouCantUpdateIt();
            }
            newspaper.Title = dto.Title;
            _repository.Update(newspaper);
            await _unitOfWork.Complete();
        }
    }
}
