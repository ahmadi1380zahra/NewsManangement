using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Authors.Contracts
{
    public interface AuthorRepository
    {
        void Add(Author author);
    }
}
