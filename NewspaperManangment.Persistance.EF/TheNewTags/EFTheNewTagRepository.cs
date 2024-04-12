using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNewTags.Contracts;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.TheNewTags
{
    public class EFTheNewTagRepository : TheNewTagRepository
    {
        private readonly DbSet<TheNewTag> _theNewTags;
        public EFTheNewTagRepository(EFDataContext context)
        {
            _theNewTags = context.Set<TheNewTag>();
        }

        public void Add(TheNewTag theNewTag)
        {
            _theNewTags.Add(theNewTag);
        }

        public void Delete(TheNewTag theNewTag)
        {
            _theNewTags.Remove(theNewTag);
        }

        public async Task<TheNewTag?> Find(int id)
        {
            return await _theNewTags.FirstOrDefaultAsync(_ => _.Id == id);
        }

      

        public async Task<List<GetTheNewTagDto>?> GetTags(int theNewId)
        {
            var theNewTags = _theNewTags.Include(_ => _.Tag).Where(_ => _.TheNewId == theNewId)
                .Select(theNewTag => new GetTheNewTagDto
                {
                    Id = theNewTag.Id,
                    TagTitle = theNewTag.Tag.Title
                });
            return await theNewTags.ToListAsync();
        }
    }
}
