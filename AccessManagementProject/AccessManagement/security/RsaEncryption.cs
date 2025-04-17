using Microsoft.Extensions.Configuration;
using System.IO;

public class RsaEncryption
{
    private static readonly IConfiguration Configuration;

    static RsaEncryption()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string GetPublicKey()
    {
        return Configuration["RSA:PublicKey"];
    }

    public static string GetPrivateKey()
    {
        return Configuration["RSA:PrivateKey"];
    }
}
