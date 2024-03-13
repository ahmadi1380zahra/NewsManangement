using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using NewspaperManangment.Services.Authors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Authors
{
    public class AuthorServiceUpdateTests : BusinessUnitTest
    {
        private readonly AuthorService _sut;
        public AuthorServiceUpdateTests()
        {
            _sut = AuthorServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_an_author_proprly()
        {
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var dto = UpdateAuthorDtoFactory.Create("نیلو حقیقت");

            await _sut.Update(author.Id, dto);

            var actual = ReadContext.Authors.Single();
            actual.FullName.Should().Be(dto.FullName);
        }
        [Fact]
        public async Task Update_throws_AuthorIsNotExistException()
        {
            var dummyAuthorId = 13;
            var dto = UpdateAuthorDtoFactory.Create("نیلو حقیقت");

            var actual = () => _sut.Update(dummyAuthorId, dto);

            await actual.Should().ThrowExactlyAsync<AuthorIsNotExistException>();
        }
    }
}
