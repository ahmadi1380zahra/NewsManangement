using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.TheNews
{
    public class EFTheNewRepository : TheNewRepository
    {
        private readonly DbSet<TheNew> _theNews;
        public EFTheNewRepository(EFDataContext context)
        {
            _theNews = context.Set<TheNew>();
        }

        public void Add(TheNew theNew)
        {
            _theNews.Add(theNew);
        }

        public void Delete(TheNew theNew)
        {
            _theNews.Remove(theNew);
        }

        public async Task<TheNew?> Find(int id)
        {
            return await _theNews.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetAllTheNewDto>?> GetAll(GetTheNewFilterDto dto)
        {
            var theNews =  _theNews.Include(_=>_.Author).Select(_ =>  new GetAllTheNewDto
            {
                Id=_.Id,
                Title=_.Title,
                Description=_.Description,
                AuthorName=_.Author.FullName,
                Rate=_.Rate,
                View =_.View,
                NewspaperCategoryId=_.NewspaperCategoryId
            });
            if(dto.Title != null)
            {
                theNews = theNews.Where
                    (_ => _.Title.Replace(" ", "").Contains(dto.Title.Replace(" ", "")));
            }
               return await theNews.ToListAsync();
        }

        public async Task<List<GetAllTheNewWithTagDto>?> GetAllWithTags(GetTheNewFilterDto dto)
        {
            var theNews = _theNews.Include(_ => _.TheNewTags)
                .ThenInclude(_ => _.Tag)
                .ThenInclude(_ => _.Category)
                .Select(_ => new GetAllTheNewWithTagDto
                {
                    Id = _.Id,
                    Title = _.Title,
                    Description = _.Description,
                    TheNewWithTags = _.TheNewTags.Select(t=>new GetTagsOfTheNewDto
                    {
                        Id=t.Tag.Id,
                        Title=t.Tag.Title,
                        CategoryTitle=t.Tag.Category.Title
                    }).ToList()
                  
                });
            if (dto.Title != null)
            {
                theNews = theNews.Where
                    (_ => _.Title.Replace(" ", "").Contains(dto.Title.Replace(" ", "")));
            }
            return await theNews.ToListAsync();
        }

        public async Task<List<GetTheNewDto>?> GetMostViewd()
        {
            var maxView = await _theNews.MaxAsync(_ => _.View);
            var TheNews = await _theNews.Where(_ => _.View == maxView)
                .Include(_ => _.Author).Select(theNew => new GetTheNewDto()
                {
                    Id = theNew.Id,
                    Title = theNew.Title,
                    Description = theNew.Description,
                    Rate = theNew.Rate,
                    View = theNew.View,
                    AuthorName = theNew.Author.FullName
                }).ToListAsync();
            return TheNews;

        }

        public async Task<GetTheNewDto?> GetToIncreaseView(int id)
        {
            var TheNew = await _theNews.Include(_ => _.Author).FirstOrDefaultAsync(_ => _.Id == id);

            if (TheNew == null)
            {
                return null;
            }

            return new GetTheNewDto
            {
                Id = TheNew.Id,
                Title = TheNew.Title,
                Description = TheNew.Description,
                Rate = TheNew.Rate,
                View = TheNew.View,
                AuthorName = TheNew.Author.FullName

            };

        }

        public async Task IncreaseView(int id)
        {
            var theNew = await _theNews.FirstOrDefaultAsync(_ => _.Id == id);
            if (theNew != null)
            {
                theNew.View++;
            }

        }

        public async Task<bool> IsExist(int id)
        {
            return await _theNews.AnyAsync(x => x.Id == id); 
        }

        public async Task<int> TotalNewsRateInOneCategoryNewspaper(int newsPaperCategoryId)
        {
            return await _theNews
                .Where(_ => _.NewspaperCategoryId == newsPaperCategoryId)
               .SumAsync(_ => _.Rate);
        }

        public async Task<int> TotalNewsRateInOneCategoryNewspaperExceptItSelf(int newsPaperCategoryId, int id)
        {
            return await _theNews
                .Where(_ => _.NewspaperCategoryId == newsPaperCategoryId
                && _.Id != id)
               .SumAsync(_ => _.Rate);
        }

        public void Update(TheNew theNew)
        {
            _theNews.Update(theNew);
        }
    }
}
