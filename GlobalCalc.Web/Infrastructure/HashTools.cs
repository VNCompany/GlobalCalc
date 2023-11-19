using System.Security.Cryptography;
using System.Text;

namespace GlobalCalc.Web.Infrastructure;

public static class HashTools
{
    public static string GenerateToken(string user, string password)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(user, password)));
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hash)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }
}
