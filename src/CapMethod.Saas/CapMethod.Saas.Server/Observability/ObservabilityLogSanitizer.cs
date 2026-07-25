namespace CapMethod.Saas.Server.Observability;

public static class ObservabilityLogSanitizer
{
    private const string RedactedValue = "[REDACTED]";

    private static readonly string[] SensitiveNames =
    [
        "authorization",
        "password",
        "token",
        "secret",
        "accesscode",
        "connectionstring",
        "cookie"
    ];

    public static string Sanitize(string name, string? value)
    {
        if (SensitiveNames.Any(sensitive => name.Contains(sensitive, StringComparison.OrdinalIgnoreCase)))
        {
            return RedactedValue;
        }

        string text = value?.Trim() ?? string.Empty;
        return text.Length <= 256 ? text : string.Concat(text.AsSpan(0, 256), "…");
    }
}