using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NewspaperManangment.Persistance.EF.Authors
{
    public class EFAuthorRepository : AuthorRepository
    {
        private readonly DbSet<Author> _authors;
        public EFAuthorRepository(EFDataContext context)
        {
            _authors = context.Set<Author>();
        }

        public void Add(Author author)
        {
            _authors.Add(author);
        }

        public void Delete(Author author)
        {
            _authors.Remove(author);
        }

        public async Task<Author?> Find(int id)
        {
            return await _authors.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetAuthorsDto>?> GetAll(GetAuthorsFilterDto? dto)
        {
            var authors = _authors.Select(author => new GetAuthorsDto
            {
                Id = author.Id,
                FullName = author.FullName,
            });
            if (dto.FullName != null)
            {
                authors = authors.Where(_ => _.FullName.Replace(" ", string.Empty)
                .Contains(dto.FullName.Replace(" ", string.Empty)));
            }
            return await authors.ToListAsync();
        }

        public async Task<List<GetAuthorsDto>?> GetHighestNewsCount()
        {
            var maxCount = await _authors.Include(_ => _.TheNews).MaxAsync(_ => _.TheNews.Count);
            var authors=await _authors.Include(_ => _.TheNews).Where(_ => _.TheNews.Count == maxCount)
                .Select(author => new GetAuthorsDto
                {
                    Id = author.Id,
                    FullName = author.FullName
                }).ToListAsync();
            return authors;
         }

        public async Task<List<GetAuthorsDto>?> GetMostViewed()
        {
            var maxView = await _authors.Include(_ => _.TheNews)
                              .SelectMany(_ => _.TheNews)
                              .MaxAsync(_ => _.View);
           return await _authors.Include(_ => _.TheNews)
                           .Where(_ => _.TheNews.Any(_ => _.View == maxView))
                           .Select(author => new GetAuthorsDto
                           {
                               Id = author.Id,
                               FullName = author.FullName
                           }).ToListAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _authors.AnyAsync(_ => _.Id == id);
        }

        public void Update(Author author)
        {
            _authors.Update(author);
        }
    }
}
