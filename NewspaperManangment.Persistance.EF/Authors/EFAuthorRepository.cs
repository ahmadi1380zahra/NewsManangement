using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Author?> Find(int id)
        {
            return await _authors.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public void Update(Author author)
        {
            _authors.Update(author);
        }
    }
}
