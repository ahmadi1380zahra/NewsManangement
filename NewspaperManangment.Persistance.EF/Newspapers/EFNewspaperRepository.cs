using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
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

        public void Delete(Newspaper newspaper)
        {
            _newspaper.Remove(newspaper);
        }

        public async Task<Newspaper?> Find(int id)
        {
            return await _newspaper.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<GetNewspaperDto>?> GetAll(GetNewspaperFilterDto? dto)
        {
            var newspapers = _newspaper.Include(_ => _.NewspaperCategories)
                 .Select(newspaper => new GetNewspaperDto()
                 {
                     Id= newspaper.Id,
                     Title=newspaper.Title,
                     Status=(newspaper.PublishDate == null?"منتشر نشده":"منتشر شده"+newspaper.PublishDate)
                 });
            if (dto.Title != null)
            {
                newspapers = newspapers.Where(_ => _.Title.Replace(" ", string.Empty)
                .Contains(dto.Title.Replace(" ", string.Empty)));
            }
            return await newspapers.ToListAsync();

        }

        public async Task<bool> IsDuplicateTitle(string title)
        {
            return await _newspaper.AnyAsync(_ => _.Title.Replace(" ",String.Empty)
            .Equals(title.Replace(" ", String.Empty)));
        }

        public async Task<bool> IsDuplicateTitleExceptiItSelf(int id, string title)
        {
            return await _newspaper.AnyAsync(_ => _.Title.Replace(" ", String.Empty)
            .Equals(title.Replace(" ", String.Empty))
            && _.Id !=id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _newspaper.AnyAsync(_ => _.Id == id);
        }

        public async Task<bool> IsPublish(int id)
        {
            return await _newspaper.AnyAsync(_ => _.Id == id && _.PublishDate != null);
        }

        public void Update(Newspaper newspaper)
        {
            _newspaper.Update(newspaper);
        }
    }
}
