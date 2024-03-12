using Xunit;

namespace NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

[CollectionDefinition(nameof(ConfigurationFixture), DisableParallelization = false)]
public class ConfigurationCollectionFixture : ICollectionFixture<ConfigurationFixture>
{
}