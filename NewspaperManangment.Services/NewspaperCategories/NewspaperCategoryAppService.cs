using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Exceptions;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;
using NewspaperManangment.Services.NewspaperCategories.Exceptions;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.NewspaperCategories
{
    public class NewspaperCategoryAppService: NewspaperCategoryService
    {
        private readonly NewspaperCategoryRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private readonly NewspaperRepository _newspaperRepository;
        public NewspaperCategoryAppService(NewspaperCategoryRepository repository
            , UnitOfWork unitOfWork
            , CategoryRepository categoryRepository
            , NewspaperRepository newspaperRepository)
        {
            _repository = repository;
            _unitOfWork= unitOfWork;
            _categoryRepository = categoryRepository;
            _newspaperRepository = newspaperRepository;
        }
        public async Task Add(AddNewspaperCategoryDto dto)
        {
            if (!await _categoryRepository.IsExist(dto.categoryId))
            {
                throw new CategoryIsNotExistException();
            }
            if (!await _newspaperRepository.IsExist(dto.newspaperId))
            {
                throw new NewspaperIsNotExistException();
            }
            if (await _newspaperRepository.IsPublish(dto.newspaperId))
            {
                throw new NewspaperHasBeenPublishedYouCantUpdateIt();
            }
            if (await _repository.IsReduplicateCategoryIdForThisNewspaper(dto.newspaperId,dto.categoryId))
            {
                throw new CategoryIsReduplicateForThisNewspaperException();
            }
            var newspaperCategory = new NewspaperCategory
            {
                CategoryId = dto.categoryId,
                NewspaperId = dto.newspaperId,
            };
            _repository.Add(newspaperCategory);
            await _unitOfWork.Complete();

        }

        public async Task Delete(int id)
        {
            var newspaperCategory =await _repository.Find(id);
            if (newspaperCategory==null)
            {
                throw new NewspaperCategoryIsNotExistException();
            }
            _repository.Delete(newspaperCategory);
          await  _unitOfWork.Complete();
        }
    }
}
