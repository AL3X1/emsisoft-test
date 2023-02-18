using System.Security.Cryptography;
using System.Text;

namespace EmsisoftTest.Infrastructure.Utils;

public static class EncryptionManager
{
    public static string GetRandomHash()
    {
        var randomPayload = Encoding.UTF8.GetBytes(DateTime.UtcNow.Millisecond.ToString());
        
        using var sha = SHA1.Create();
        var hashBytes = sha.ComputeHash(randomPayload); 
        return Convert.ToHexString(hashBytes);
    }
}