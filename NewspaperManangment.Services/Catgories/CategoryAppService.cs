using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Catgories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Catgories
{
    public class CategoryAppService : CategoryService
    {
        private readonly CategoryRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public CategoryAppService(CategoryRepository repository,
                                  UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddCategoryDto dto)
        {
            if (dto.Rate <= 0)
            {
                throw new CategoryRateShouldBeMoreThanZeroException();
            }
            var category = new Category
            {
                Title = dto.Title,
                Rate = dto.Rate,
            };
            _repository.Add(category);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var category = await _repository.Find(id);
            if (category == null)
            {
                throw new CategoryIsNotExistException();
            }
            _repository.Delete(category);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetCategoryDto>?> GetAll(GetCategoryFilterDto? dto)
        {
            return await _repository.GetAll(dto);
        }

        public async Task Update(int id, UpdateCategoryDto dto)
        {
            if (dto.Rate <= 0)
            {
                throw new CategoryRateShouldBeMoreThanZeroException();
            }
            var category = await _repository.Find(id);
            if (category == null)
            {
                throw new CategoryIsNotExistException();
            }
            category.Title = dto.Title;
            category.Rate = dto.Rate;
            _repository.Update(category);
            await _unitOfWork.Complete();
        }
    }
}
