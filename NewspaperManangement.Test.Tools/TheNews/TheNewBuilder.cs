using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNews
{
    public class TheNewBuilder
    {
        private readonly TheNew _theNew;
        public TheNewBuilder(int authorId,int newspaperCategoryId)
        {
            _theNew = new TheNew
            {
                Title = "پرسپولیس در لیگ",
                Description = "پرسپولیس قهرمان لیگ شد",
                Rate = 10,
                AuthorId = authorId,
                NewspaperCategoryId = newspaperCategoryId,
            };
        }
        public TheNewBuilder WithTitle(string title)
        {
            _theNew.Title = title;
            return this;
        }
        public TheNewBuilder WithDesciption(string description)
        {
            _theNew.Description = description;
            return this;
        }
        public TheNewBuilder WithRate(int rate)
        {
            _theNew.Rate = rate;
            return this;
        }
        public TheNewBuilder WithView(int view)
        {
            _theNew.View = view;
            return this;
        }
        public TheNewBuilder WithTheNewTags(int tagId)
        {
            _theNew.TheNewTags.Add(new TheNewTag
            {
                TagId = tagId
            });
            return this;
        }
        public TheNew Build()
        {
            return _theNew;      
        }
    }
}
