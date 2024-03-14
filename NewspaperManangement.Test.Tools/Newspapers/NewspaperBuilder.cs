using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Newspapers
{
    public class NewspaperBuilder
    {
        private readonly Newspaper _newspaper;
        public NewspaperBuilder()
        {
            _newspaper = new Newspaper
            {
                Title = "dummy-title",

            };
        
        }
        public NewspaperBuilder WithTitle(string title)
        {
            _newspaper.Title = title;
            return this;
        }
        public NewspaperBuilder WithPublishDate(DateTime date)
        {
            _newspaper.PublishDate = date;
            return this;
        }
        public NewspaperBuilder WithCategory(int categoryId)
        {
            _newspaper.NewspaperCategories.Add(new NewspaperCategory
            {
                CategoryId = categoryId,
            });
            return this;
        }
        public Newspaper Build()
        {
            return _newspaper;
        }
    }
}
