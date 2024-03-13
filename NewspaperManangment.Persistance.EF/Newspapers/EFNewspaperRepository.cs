using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.Newspapers
{
    public class EFNewspaperRepository: NewspaperRepository
    {
        private readonly DbSet<Newspaper> _newspaper;
        public EFNewspaperRepository(EFDataContext context)
        {
            _newspaper = context.Set<Newspaper>();
        }

        public void Add(Newspaper newspaper)
        {
            _newspaper.Add(newspaper);
        }

      
    }
}
