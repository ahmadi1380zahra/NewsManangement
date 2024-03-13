using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Authors
{
    public class AuthorServiceAddTests: BusinessUnitTest
    {
        private readonly AuthorService _sut;
        public AuthorServiceAddTests()
        {
            _sut = AuthorServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task Add_adds_a_new_author_properly()
        {
            var dto = AddAuthorDtoFactory.Create();

            await _sut.Add(dto);

            var actual = ReadContext.Authors.Single();
            actual.FullName.Should().Be(dto.FullName);
        }
    }
}
