using System.Security.Cryptography;
using System.Text;

namespace CapMethod.Saas.Server.Security;

public sealed class PasswordHashVerifier
{
    public bool VerifySha256(string password, string expectedHexHash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(expectedHexHash))
        {
            return false;
        }

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = SHA256.HashData(passwordBytes);
        string actualHexHash = Convert.ToHexString(hashBytes).ToLowerInvariant();

        return string.Equals(actualHexHash, expectedHexHash, StringComparison.OrdinalIgnoreCase);
    }
}
