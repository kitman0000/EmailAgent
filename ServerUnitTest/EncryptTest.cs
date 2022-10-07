using EmailAgentServer.Common;

namespace ServerUnitTest;

public class EncryptTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AesTest()
    {
        var key = EncryptHelper.CreateRandomKey(32);
        Console.WriteLine("Using Key:" + key);
        var encryptStringResult = EncryptHelper.AesEncryptString("hello", key);
        Console.WriteLine(encryptStringResult);
        var decryptStringResult = EncryptHelper.AesDecryptString(encryptStringResult, key);
        Console.WriteLine(decryptStringResult);
        Assert.AreEqual(decryptStringResult,"hello");
        Assert.Pass();
    }

    [Test]
    public void RandomKeyTest()
    {
        var key = EncryptHelper.CreateRandomKey(16);
        Console.WriteLine(key);
        Assert.AreEqual(key.Length, 16);
    }
}