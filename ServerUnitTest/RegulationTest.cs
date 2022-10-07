using System.Text.RegularExpressions;

namespace ServerUnitTest;

public class RegulationTest
{
    [Test]
    public void RegTest()
    {
        string input = "Dear ${User}, I'm happy to inform you that your apply (N0.${ApplyNo} has been accepted.)";
        string pattern = @"\${.*?}";

        foreach (Match match in Regex.Matches(input, pattern))
        {
            var value = match.Value.Substring(2, match.Length - 3);
            Console.WriteLine(value);
        }
    }
    
}