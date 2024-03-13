using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Authors
{
    public class AuthorServiceGetTests:BusinessUnitTest
    {
        private readonly AuthorService _sut;
        public AuthorServiceGetTests()
        {
            _sut = AuthorServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Get_gets_all_authors_with_valid_data()
        {
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("نیلوفر حقیقت").Build();
            DbContext.Save(author2);
            var dto = GetAuthorsFilterDtoFactory.Create(null);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(2);
            var author = actual.FirstOrDefault();
            author.Id.Should().Be(author1.Id);
            author.FullName.Should().Be(author1.FullName);
        }
        [Fact]
        public async Task Get_gets_authors_by_filter_with_valid_data()
        {
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("نیلوفر حقیقت").Build();
            DbContext.Save(author2);
            var filter = "حقیق";
            var dto = GetAuthorsFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(1);
            var author = actual.FirstOrDefault();
            author.Id.Should().Be(author2.Id);
            author.FullName.Should().Be(author2.FullName);
        }
    }
}
