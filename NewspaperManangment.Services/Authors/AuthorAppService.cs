using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
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
    }
}
