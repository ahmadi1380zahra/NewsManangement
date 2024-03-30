using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.NewspaperCategories
{
    public class EFNewspaperCategoryRepository : NewspaperCategoryRepository
    {
        private readonly DbSet<NewspaperCategory> _newspaperCategories;
        public EFNewspaperCategoryRepository(EFDataContext context)
        {
            _newspaperCategories = context.Set<NewspaperCategory>();
        }

        public void Add(NewspaperCategory newspaperCategory)
        {
            _newspaperCategories.Add(newspaperCategory);
        }

        public void Delete(NewspaperCategory newspaperCategory)
        {
            _newspaperCategories.Remove(newspaperCategory);
        }

        public void DeleteCategoryForThisNewspaper(int id)
        {
            var categoriesOfnewspaper =  _newspaperCategories
                .Where(_ => _.NewspaperId == id);
            _newspaperCategories.RemoveRange(categoriesOfnewspaper);
        }

        public async Task<NewspaperCategory?> Find(int id)
        {
            return await _newspaperCategories.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<GetNewspaperCategoryDto?> GetHighestNewsCount()
        {
            var highestNewsCount =await _newspaperCategories
                 .Select(_ => _.TheNews.Count).MaxAsync();
            var highestNewsCountCategory = await _newspaperCategories.Include(_ => _.Newspaper)
                 .Include(_ => _.Category)
                 .Where(_=>_.TheNews.Count==highestNewsCount)
                 .Select(newspaperCategory=>new GetNewspaperCategoryDto
                 {
                     Id=newspaperCategory.Id,
                     CategoryName=newspaperCategory.Category.Title,
                     NewspaperName=newspaperCategory.Newspaper.Title,
                 }).ToListAsync();
            return highestNewsCountCategory.FirstOrDefault();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _newspaperCategories.AnyAsync(_ => _.Id == id);
        }

        public async Task<bool> IsExistCategoryForThisNewspaper(int id)
        {
            return await _newspaperCategories.AnyAsync(_ => _.NewspaperId == id);
        }

        public async Task<bool> IsReduplicateCategoryIdForThisNewspaper(int newspaperId, int categoryId)
        {
            return await _newspaperCategories.AnyAsync(_ => _.NewspaperId == newspaperId
            && _.CategoryId == categoryId);
        }
    }
}
