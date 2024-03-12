using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Exceptions;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using NewspaperManangment.Services.Tags.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Tags
{
    public class TagAppService : TagService
    {
        private readonly TagRepository _repository;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        public TagAppService(TagRepository repository,
            UnitOfWork unitOfWork
            , CategoryRepository categoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task Add(AddTagDto dto)
        {
            if (!await _categoryRepository.IsExist(dto.CategoryId))
            {
                throw new CategoryIsNotExistException();
            }
            var tag = new Tag
            {
                Title = dto.Title,
                CategoryId = dto.CategoryId,
            };
            _repository.Add(tag);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var tag = await _repository.Find(id);
            if (tag == null)
            {
                throw new TagIsNotExistException();
            }
            _repository.Delete(tag);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetTagDto>?> GetAll(GetTagFilterDto? dto)
        {
            return await _repository.GetAll(dto);
        }

        public async Task Update(int id, UpdateTagDto dto)
        {
            if (!await _categoryRepository.IsExist(dto.CategoryId))
            {
                throw new CategoryIsNotExistException();
            }
            var tag = await _repository.Find(id);
            if (tag == null)
            {
                throw new TagIsNotExistException();
            }
            tag.Title = dto.Title;
            tag.CategoryId = dto.CategoryId;
            _repository.Update(tag);
            await _unitOfWork.Complete();

        }
    }
}
