using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.Tags
{
    public class EFTagRepository : TagRepository
    {
        private readonly DbSet<Tag> _tags;
        public EFTagRepository(EFDataContext dataContext)
        {
            _tags = dataContext.Tags;
        }

        public void Add(Tag tag)
        {
            _tags.Add(tag);
        }

        public void Delete(Tag tag)
        {
            _tags.Remove(tag);
        }

        public async Task<Tag?> Find(int id)
        {
            return await _tags.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetTagDto>?> GetAll(GetTagFilterDto? dto)
        {
            IQueryable<Tag> query = _tags;
            if (dto.Title != null)
            {
                query = query.Where(_ => _.Title.Replace(" ", string.Empty).Contains(dto.Title.Replace(" ", string.Empty)));
            };
            List<GetTagDto> tags = await query.Include(_=>_.Category).Select(tag => new GetTagDto
            {
                Id = tag.Id,
                Title = tag.Title,
                Category = tag.Category.Title,
            }).ToListAsync();
            return tags;
        }

        public void Update(Tag tag)
        {
            _tags.Update(tag);
        }
    }
}
