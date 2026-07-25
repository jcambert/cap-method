using CapMethod.Saas.Server.Observability;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class ObservabilityLogSanitizerTests
{
    [Theory]
    [InlineData("Authorization")]
    [InlineData("JwtToken")]
    [InlineData("DatabasePassword")]
    [InlineData("ConnectionString")]
    [InlineData("AccessCode")]
    public void Sensitive_values_are_redacted(string name)
    {
        Assert.Equal("[REDACTED]", ObservabilityLogSanitizer.Sanitize(name, "sensitive-value"));
    }

    [Fact]
    public void Non_sensitive_values_are_trimmed_and_bounded()
    {
        string value = string.Concat("  ", new string('a', 300), "  ");

        string result = ObservabilityLogSanitizer.Sanitize("RequestPath", value);

        Assert.Equal(257, result.Length);
        Assert.EndsWith("…", result, StringComparison.Ordinal);
    }
}