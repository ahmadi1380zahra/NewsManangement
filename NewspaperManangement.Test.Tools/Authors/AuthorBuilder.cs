using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Authors
{
    public class AuthorBuilder
    {
        private readonly Author _author;
        public AuthorBuilder()
        {
            _author = new Author
            {
                FullName = "dummy-first-last-name"
            };
        }
        public AuthorBuilder WithFullName(string fullName)
        {
            _author.FullName = fullName;
            return this;
        }
        public Author Build()
        {
            return _author;
        }
    }
}
