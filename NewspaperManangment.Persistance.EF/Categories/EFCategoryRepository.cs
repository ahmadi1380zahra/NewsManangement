using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly DbSet<Category> _categories;
        public EFCategoryRepository(EFDataContext context)
        {
            _categories = context.Categories;
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(Category category)
        {
            _categories.Remove(category);
        }

        public async Task<Category?> Find(int id)
        {
            return await _categories.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetCategoryDto>?> GetAll(GetCategoryFilterDto? dto)
        {
            IQueryable<Category> query = _categories;
            if (dto.Title != null)
            {
                query = query.Where(_ => _.Title.Replace(" ", string.Empty).Contains(dto.Title.Replace(" ", string.Empty)));
            };
            List<GetCategoryDto> catgories = await query.Select(category => new GetCategoryDto
            {
                Id = category.Id,
                Title = category.Title,
                Rate = category.Rate,
            }).ToListAsync();
            return catgories;

        }

        public async Task<bool> IsExist(int id)
        {
         return await _categories.AnyAsync(_ => _.Id == id);
        }

        public void Update(Category category)
        {
            _categories.Update(category);
        }
    }
}
