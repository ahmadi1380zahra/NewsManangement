using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Categories
{
    public class CategoryBuilder
    {
        private readonly Category _category;
        public CategoryBuilder()
        {
            _category = new Category
            {
                Title = "dummy-title",
                Rate = 40,
            };
        }
        public CategoryBuilder WithTitle(string title)
        {
            _category.Title = title;
            return this;
        }
        public CategoryBuilder WithRate(int rate)
        {
            _category.Rate = rate;
            return this;
        }
        public Category Build()
        {
            return _category;
        }
    }
}
