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
    public class EFAuthorRepository:AuthorRepository
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
    }
}
