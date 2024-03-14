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
    public class EFNewspaperRepository : NewspaperRepository
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
        public async Task<Newspaper?> Find(int id)
        {
            return await _newspaper.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _newspaper.AnyAsync(_ => _.Id == id);
        }

        public void Update(Newspaper newspaper)
        {
            _newspaper.Update(newspaper);
        }
    }
}
