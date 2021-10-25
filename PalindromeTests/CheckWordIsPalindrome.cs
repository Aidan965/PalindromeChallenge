using System.Collections.Generic;
using Xunit;

namespace PalindromeChallenge
{
    public class PalindromeTests
    {
        [Theory]
        [MemberData(nameof(CheckWordIsPalindromeTestData))]
        public static void CheckWordIsPalindrome(string user, string word, List<string> cache, bool expected)
        {
            var result = Program.IsPalindrome(user, word, cache);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> CheckWordIsPalindromeTestData()
        {
            return new List<object[]>
            {
                new object[] { "aidan", "kayak", new List<string> { "anna" , "civic" , "radar" }, true },
                new object[] { "aidan", "e", new List<string> { "anna" , "civic", "radar" }, true },
                new object[] { "aidan", "123", new List<string> { "anna" , "civic" , "radar" }, false },
                new object[] { "aidan", " ", new List<string> { "anna" , "civic", "radar" }, false },
                new object[] { "aidan", "=!`", new List<string> { "anna" , "civic", "radar" }, false },
                new object[] { "aidan", " + ", new List<string> { "anna" , "civic", "radar" }, false }
            };
        }

    }
}

