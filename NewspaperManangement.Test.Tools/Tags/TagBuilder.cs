using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Tags
{
    public class TagBuilder
    {
        private readonly Tag _tag;
        public TagBuilder(int categorId)
        {
            _tag = new Tag
            {
                Title = "Title",
                CategoryId = categorId,
            };

        }
        public TagBuilder WithTitle(string title)
        {
            _tag.Title = title;
            return this;
        }
        public Tag Build()
        {
            return _tag;
        }
    }
}
