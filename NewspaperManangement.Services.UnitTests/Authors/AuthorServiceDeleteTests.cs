using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Authors
{
    public class AuthorServiceDeleteTests : BusinessUnitTest
    {
        private readonly AuthorService _sut;
        public AuthorServiceDeleteTests()
        {
            _sut = AuthorServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_an_author_properly()
        {
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);

            await _sut.Delete(author.Id);

            var actual = ReadContext.Authors.FirstOrDefault(_ => _.Id == author.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_AuthorIsNotExistException()
        {
            var dummyAuthorId = 13;
           
            var actual = () => _sut.Delete(dummyAuthorId);

            await actual.Should().ThrowExactlyAsync<AuthorIsNotExistException>();
        }
    }
}
