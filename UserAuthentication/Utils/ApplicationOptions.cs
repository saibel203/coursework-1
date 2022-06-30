using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserAuthentication.Utils;

public class ApplicationOptions
{
    public const string Audience = "Audience";
    public const string Issue = "Issue";
    private const string Key = "My new big security Key12345";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.UTF8.GetBytes(Key));
}
