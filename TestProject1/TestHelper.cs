using System.Text.RegularExpressions;

namespace TestProject1
{
    internal class TestHelper
    {
        public static GroupCollection CreateGroup(string input)
        {
            var match = Regex.Match(input, @"(\s+)");
            return match.Groups;
        }
    }

    internal class TestDem 
    {



    }
}
