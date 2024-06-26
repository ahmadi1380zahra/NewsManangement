﻿using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts
{
    public interface AuthorRepository: Repository
    {
        void Add(Author author);
        void Delete(Author author);
        Task<Author?> Find(int id);
        Task<GetAuthorsDto?> Get(int id);
        Task<List<GetAuthorsDto>?> GetAll(GetAuthorsFilterDto? dto);
       Task<List<GetAuthorsDto>?> GetHighestNewsCount();
        Task<List<GetAuthorsDto>?> GetMostViewed();
        Task<bool> IsExist(int id);
        void Update(Author author);
    }
}
