using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Exceptions;
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
        private readonly UnitOfWork _unitOfWork;
        public NewspaperAppService(NewspaperRepository repository
                                  , UnitOfWork unitOfWork
                                  , CategoryRepository categoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task Add(AddNewsPaperDto dto)
        {
            var newspaper = new Newspaper
            {
                Title = dto.Title,
            };
            foreach (var categoryId in dto.CategoriesId)
            {
                if (! await _categoryRepository.IsExist(categoryId))
                {
                    throw new CategoryIsNotExistException();
                }
                if (newspaper.NewspaperCategories.Any(_=>_.CategoryId==categoryId))
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
    }
}
