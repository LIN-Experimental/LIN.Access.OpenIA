namespace LIN.Access.OpenIA;

public class OpenIA
{

    internal static string ApiKey { get; set; } = string.Empty;

    public static void SetKey(string key)
    {
        ApiKey = key;
    }

}