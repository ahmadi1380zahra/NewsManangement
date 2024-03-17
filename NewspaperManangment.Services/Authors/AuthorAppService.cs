using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using NewspaperManangment.Services.Authors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors
{
    public class AuthorAppService : AuthorService
    {
        private readonly AuthorRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public AuthorAppService(AuthorRepository repository
                                 ,UnitOfWork unitOfWork   )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Add(AddAuthorDto dto)
        {
            var author = new Author
            {
                FullName = dto.FullName,
            };
            _repository.Add(author);
          await  _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var author = await _repository.Find(id);
            if (author == null)
            {
                throw new AuthorIsNotExistException();
            }
            _repository.Delete(author);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetAuthorsDto>?> GetAll(GetAuthorsFilterDto? dto)
        {
            return await _repository.GetAll(dto);
        }

        public async Task<List<GetAuthorsDto>?> GetMostViewed()
        {
            return await _repository.GetMostViewed();
        }

        public async Task Update(int id, UpdateAuthorDto dto)
        {
            var author=await _repository.Find(id);
            if(author == null)
            {
                throw new AuthorIsNotExistException();
            }
            author.FullName = dto.FullName;
            _repository.Update(author);
            await _unitOfWork.Complete();
        }
    }
}
