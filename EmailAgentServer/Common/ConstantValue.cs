namespace EmailAgentServer.Common;

public class ConstantValue
{
    public const string UserPasswordMd5Key = "53EXvnHPkXFyYUns";

    public static readonly string TokenGenerateKey = "LgpCsrbRaHZHzQcy" + DateTimeOffset.Now.ToUnixTimeMilliseconds();
}